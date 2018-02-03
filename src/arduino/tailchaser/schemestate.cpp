/**
 * @file
 * 
 * Implementation file for the SchemeState class.
 */

#include "matrix.h"
#include "schemestate.h"
#include "signalstate.h"
#include "terminal.h"

SchemeState::SchemeState()
    : m_forceRedrawRequired(true)
    , m_memBase(nullptr)
    , m_numLayers(0)
{
}

bool SchemeState::loadScheme(const uint8_t *data, size_t length)
{
    // Reset to no schemes and mark that a forced
    // redraw is required
    
    m_forceRedrawRequired = true;
    m_memBase = nullptr;
    m_numLayers = 0;

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
    
    uint8_t layers = pgm_read_byte_near(data);

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
        m_layers[i].m_conditionMask = pgm_read_byte_near(data);
        m_layers[i].m_conditionValue = pgm_read_byte_near(data + 1);
        m_layers[i].m_bitmapBase = data + 2;

        data += (2 + BITMAP_BYTES);
        length -= (2 + BITMAP_BYTES);
    }

    if (length != 0)
        return false;

    m_memBase = orig_base;
    m_numLayers = layers;
    return true;
}

bool SchemeState::skipString(const uint8_t *&data, size_t &length)
{
    if (length < 1)
        return false;

    uint8_t str_len = pgm_read_byte_near(data);

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
        
    matrix.fillScreen(0);

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
                    uint8_t b1 = pgm_read_byte_near(data + 0);
                    uint8_t b2 = pgm_read_byte_near(data + 1);
                    uint8_t b3 = pgm_read_byte_near(data + 2);
                    uint8_t b4 = pgm_read_byte_near(data + 3);
                    uint8_t b5 = pgm_read_byte_near(data + 4);

                    uint8_t color;
                    
                    color = b1 >> 3;
                    if (color < 27) matrix.setPixel(column + 0, row, color);
                    color = ((b1 & 0x07) << 2) | (b2 >> 6);
                    if (color < 27) matrix.setPixel(column + 1, row, color);
                    color = (b2 >> 1) & 0x1F;
                    if (color < 27) matrix.setPixel(column + 2, row, color);
                    color = ((b2 << 4) & 0x10) | ((b3 >> 4) & 0x0F);
                    if (color < 27) matrix.setPixel(column + 3, row, color);
                    color = ((b3 << 1) & 0x1E) | ((b4 >> 7) & 0x01);
                    if (color < 27) matrix.setPixel(column + 4, row, color);
                    color = (b4 >> 2) & 0x1F;
                    if (color < 27) matrix.setPixel(column + 5, row, color);
                    color = ((b4 << 3) & 0x18) | ((b5 >> 5) & 0x07);
                    if (color < 27) matrix.setPixel(column + 6, row, color);
                    color = b5 & 0x1F;
                    if (color < 27) matrix.setPixel(column + 7, row, color);
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

