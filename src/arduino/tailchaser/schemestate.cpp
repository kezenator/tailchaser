/**
 * @file
 * 
 * Implementation file for the SchemeState class.
 */

#include "matrix.h"
#include "schemestate.h"
#include "signalstate.h"
#include "terminal.h"

static constexpr uint16_t PALETTE_SIZE = 27;
static uint16_t PALETTE[PALETTE_SIZE] =
{
          // R G B
    0000, // 0 0 0
    0001, // 0 0 1
    0007, // 0 0 2
    0010, // 0 1 0
    0011, // 0 1 1
    0013, // 0 1 2
    0070, // 0 2 0
    0071, // 0 2 1
    0077, // 0 2 2
    0100, // 1 0 0
    0101, // 1 0 1
    0107, // 1 0 2
    0110, // 1 1 0
    0111, // 1 1 1
    0117, // 1 1 2
    0170, // 1 2 0
    0171, // 1 2 1
    0177, // 1 2 2
    0700, // 2 0 0
    0701, // 2 0 1
    0707, // 2 0 2
    0710, // 2 1 0
    0711, // 2 1 1
    0717, // 2 1 2
    0770, // 2 2 0
    0771, // 2 2 1
    0777, // 2 2 2
};

SchemeState::SchemeState()
    : m_forceRedrawRequired(true)
    , m_memBase(nullptr)
    , m_numLayers(0)
{
}

void SchemeState::clearScheme()
{
    if (m_numLayers != 0)
    {
        m_forceRedrawRequired = true;
        m_memBase = nullptr;
        m_numLayers = 0;
    }
}

bool SchemeState::loadSchemeFromRam(const uint8_t *data, size_t max_length)
{
    // Reset to no schemes and mark that a forced
    // redraw is required
    
    clearScheme();

    // Work out the length - from the first two
    // bytes

    if (max_length < 2)
        return false;

    uint16_t length = data[0] | (data[1] << 8);

    Serial.print("LENGTH=");
    Serial.println(length);

    if (max_length < (2 + length))
        return false;

    data += 2;
    
    // Save original base

    const uint8_t *orig_base = data;

    // Name and description
    
    if (!skipString(data, length))
        return false;
    if (!skipString(data, length))
        return false;

    // Number of layers

    if (length < 1)
        return false;
    
    uint8_t layers = *data;

    Serial.print("LAYERS=");
    Serial.println(layers);

    if (layers > MAX_LAYERS)
        return false;

    data += 1;
    length -= 1;

    // Layer data
    
    for (int i = 0; i < layers; ++i)
    {
        if (!skipString(data, length))
            return false;

        if (length < (2 + BITMAP_BYTES))
            return false;

        m_layers[i].m_currentlyDisplayed = false;
        m_layers[i].m_conditionMask = data[0];
        m_layers[i].m_conditionValue = data[1];
        m_layers[i].m_bitmapBase = data + 2;

        data += (2 + BITMAP_BYTES);
        length -= (2 + BITMAP_BYTES);

        Serial.print("READ ");
        Serial.print(i);
        Serial.print(" LENGTH=");
        Serial.println(length);
    }

    if (length != 0)
        return false;

    m_forceRedrawRequired = true;
    m_memBase = orig_base;
    m_numLayers = layers;
    return true;
}

bool SchemeState::skipString(const uint8_t *&data, size_t &length)
{
    if (length < 1)
        return false;

    uint8_t str_len = data[0];

    if (length <= str_len)
        return false;

    data += 1 + str_len;
    length -= 1 + str_len;
    return true;
}

bool SchemeState::update(const SignalState &signal_state)
{
    bool updated = m_forceRedrawRequired;
    m_forceRedrawRequired = false;

    for (uint8_t i = 0; i < m_numLayers; ++i)
    {
        bool needs_display = signal_state.matchesLayerMask(m_layers[i].m_conditionMask, m_layers[i].m_conditionValue);

        if (needs_display != m_layers[i].m_currentlyDisplayed)
        {
            m_layers[i].m_currentlyDisplayed = needs_display;
            updated = true;
        }
    }

    return updated;
}

void SchemeState::draw(Matrix &matrix)
{
    static_assert((BITMAP_BYTES * 8 / 5) == (Matrix::WIDTH * Matrix::HEIGHT), "Matrix and Bitmap sizes don't match");
    static_assert((Matrix::WIDTH % 8) == 0, "Matrix width must be a multiple of 8");
        
    for (uint8_t i = 0; i < m_numLayers; ++i)
    {
        if (m_layers[i].m_currentlyDisplayed)
        {
            // We need to read this layer's bitmap data from memory
            // and overlay it onto the current matrix state.

            const uint8_t *data = m_layers[i].m_bitmapBase;

            for (uint8_t row = 0; row < Matrix::HEIGHT; ++row)
            {
                // We have 5-bit values packet into 8-bit bytes -
                // so we read 5 bytes, 8 pixels at a time

                for (uint8_t column = 0; column < Matrix::WIDTH; column += 8, data += 5)
                {
                    uint8_t b1 = data[0];
                    uint8_t b2 = data[1];
                    uint8_t b3 = data[2];
                    uint8_t b4 = data[3];
                    uint8_t b5 = data[4];

                    uint8_t color;
                    
                    color = b1 >> 3;
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 0, row, PALETTE[color]);
                    color = ((b1 & 0x07) << 2) | (b2 >> 6);
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 1, row, PALETTE[color]);
                    color = (b2 >> 1) & 0x1F;
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 2, row, PALETTE[color]);
                    color = ((b2 << 4) & 0x10) | ((b3 >> 4) & 0x0F);
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 3, row, PALETTE[color]);
                    color = ((b3 << 1) & 0x1E) | ((b4 >> 7) & 0x01);
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 4, row, PALETTE[color]);
                    color = (b4 >> 2) & 0x1F;
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 5, row, PALETTE[color]);
                    color = ((b4 << 3) & 0x18) | ((b5 >> 5) & 0x07);
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 6, row, PALETTE[color]);
                    color = b5 & 0x1F;
                    if (color < PALETTE_SIZE) matrix.setPixel(column + 7, row, PALETTE[color]);
                }
            }
        }
    }
}

void SchemeState::printState(Terminal &terminal) const
{
    terminal.print("Num layers = ");
    terminal.printInt(m_numLayers);
    terminal.newLine();
}

