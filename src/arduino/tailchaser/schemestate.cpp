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

    if (layers > MAX_LAYERS)
        return false;

    data += 1;
    length -= 1;

    // Layer data
    
    for (int i = 0; i < layers; ++i)
    {
        if (!skipString(data, length))
            return false;

        if (length < (9 + BITMAP_BYTES))
            return false;

        m_layers[i].m_currentlyDisplayed = false;
        m_layers[i].m_conditionMask = data[0];
        m_layers[i].m_conditionValue = data[1];
        m_layers[i].m_pattern = data[2];
        m_layers[i].m_field1 = data[3] | (uint16_t(data[4]) << 8);
        m_layers[i].m_field2 = data[5] | (uint16_t(data[6]) << 8);
        m_layers[i].m_field3 = data[7] | (uint16_t(data[8]) << 8);
        m_layers[i].m_bitmapBase = data + 9;

        data += (9 + BITMAP_BYTES);
        length -= (9 + BITMAP_BYTES);
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
        // First, update if this layer needs to be displayed or not
        
        bool needs_display = signal_state.matchesLayerMask(m_layers[i].m_conditionMask, m_layers[i].m_conditionValue);

        if (needs_display != m_layers[i].m_currentlyDisplayed)
        {
            m_layers[i].m_currentlyDisplayed = needs_display;
            m_layers[i].m_startMillis = millis();
            m_layers[i].m_pixels = 0;
            updated = true;
        }

        // Now update it's position on screen

        if (m_layers[i].m_currentlyDisplayed)
        {
            unsigned long duration = millis() - m_layers[i].m_startMillis;
            uint8_t new_pixels = m_layers[i].m_pixels;

            switch (m_layers[i].m_pattern)
            {
            case SOLID:
                new_pixels = Matrix::WIDTH;
                break;
                
            case FLASH:
                // Field1 = on time (ms)
                // Field2 = off time (ms)
                if ((m_layers[i].m_field1 != 0)
                    || (m_layers[i].m_field2 != 0))
                {
                    if ((duration % (m_layers[i].m_field1 + m_layers[i].m_field2)) <= m_layers[i].m_field1)
                        new_pixels = Matrix::WIDTH;
                    else
                        new_pixels = 0;
                }
                break;
                
            case SWIPE_LEFT:
            case SWIPE_RIGHT:
            case SWIPE_DOWN:
            case SWIPE_UP:
                // Field1 = swipe time (ms)
                // Field2 = on time (ms)
                // Field3 = off time (ms)
                if ((m_layers[i].m_field1 != 0)
                    || (m_layers[i].m_field2 != 0)
                    || (m_layers[i].m_field3 != 0))
                {
                    unsigned long dimension = Matrix::WIDTH;
                    if ((m_layers[i].m_pattern == SWIPE_DOWN)
                        || (m_layers[i].m_pattern == SWIPE_UP))
                    {
                        dimension = Matrix::HEIGHT;
                    }

                    unsigned long progress = duration
                        % (m_layers[i].m_field1 + m_layers[i].m_field2 + m_layers[i].m_field3);

                    if (progress < m_layers[i].m_field1)
                        new_pixels = uint8_t(progress * dimension / m_layers[i].m_field1);
                    else if (progress <= (m_layers[i].m_field1 + m_layers[i].m_field2))
                        new_pixels = dimension;
                    else
                        new_pixels = 0;
                }
                break;

            case SCROLL_LEFT:
            case SCROLL_RIGHT:
            case SCROLL_DOWN:
            case SCROLL_UP:
                // Field1 = scroll time (ms)
                // Field2 = size (pixels)
                // Field3 = N/A
                
                if ((m_layers[i].m_field1 != 0)
                    && (m_layers[i].m_field2 != 0))
                {
                    unsigned long dimension = Matrix::WIDTH;
                    if ((m_layers[i].m_pattern == SCROLL_DOWN)
                        || (m_layers[i].m_pattern == SCROLL_UP))
                    {
                        dimension = Matrix::HEIGHT;
                    }

                    new_pixels = uint8_t((duration % m_layers[i].m_field1) * m_layers[i].m_field2 / m_layers[i].m_field1);
                }
                break;
            }

            if (new_pixels != m_layers[i].m_pixels)
            {
                m_layers[i].m_pixels = new_pixels;
                updated = true;
            }
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
                switch (m_layers[i].m_pattern)
                {
                case SOLID:
                case FLASH:
                case SWIPE_LEFT:
                    decodeRow(data, row);
                    for (uint8_t column = Matrix::WIDTH - m_layers[i].m_pixels; column < Matrix::WIDTH; ++column)
                    {
                        if (m_decodedRow[column] < PALETTE_SIZE)
                            matrix.setPixel(column, row, PALETTE[m_decodedRow[column]]);
                    }
                    break;

                case SWIPE_RIGHT:
                    decodeRow(data, row);
                    for (uint8_t column = 0; column < m_layers[i].m_pixels; ++column)
                    {
                        if (m_decodedRow[column] < PALETTE_SIZE)
                            matrix.setPixel(column, row, PALETTE[m_decodedRow[column]]);
                    }
                    break;

                case SWIPE_DOWN:
                    if (row < m_layers[i].m_pixels)
                    {
                        decodeRow(data, row);
                        for (uint8_t column = 0; column < Matrix::WIDTH; ++column)
                        {
                            if (m_decodedRow[column] < PALETTE_SIZE)
                                matrix.setPixel(column, row, PALETTE[m_decodedRow[column]]);
                        }
                    }
                    break;

                case SWIPE_UP:
                    if (row >= (Matrix::HEIGHT - m_layers[i].m_pixels))
                    {
                        decodeRow(data, row);
                        for (uint8_t column = 0; column < Matrix::WIDTH; ++column)
                        {
                            if (m_decodedRow[column] < PALETTE_SIZE)
                                matrix.setPixel(column, row, PALETTE[m_decodedRow[column]]);
                        }
                    }
                    break;

                case SCROLL_LEFT:
                    if (m_layers[i].m_field2 != 0)
                    {
                        decodeRow(data, row);
                        for (uint8_t column = 0; column < Matrix::WIDTH; ++column)
                        {
                            int src_column = (column + m_layers[i].m_pixels) % m_layers[i].m_field2 % Matrix::WIDTH;
    
                            if (m_decodedRow[src_column] < PALETTE_SIZE)
                                matrix.setPixel(column, row, PALETTE[m_decodedRow[src_column]]);
                        }
                    }
                    break;

                case SCROLL_RIGHT:
                    if (m_layers[i].m_field2 != 0)
                    {
                        decodeRow(data, row);
                        for (uint8_t column = 0; column < Matrix::WIDTH; ++column)
                        {
                            int src_column = (column + m_layers[i].m_field2 - m_layers[i].m_pixels) % m_layers[i].m_field2 % Matrix::WIDTH;
    
                            if (m_decodedRow[src_column] < PALETTE_SIZE)
                                matrix.setPixel(column, row, PALETTE[m_decodedRow[src_column]]);
                        }
                    }
                    break;

                case SCROLL_DOWN:
                    if (m_layers[i].m_field2 != 0)
                    {
                        uint8_t src_row = uint8_t((row + m_layers[i].m_field2 - m_layers[i].m_pixels) % m_layers[i].m_field2 % Matrix::HEIGHT);
                        
                        decodeRow(data, src_row);
                        for (uint8_t column = 0; column < Matrix::WIDTH; ++column)
                        {
                            if (m_decodedRow[column] < PALETTE_SIZE)
                                matrix.setPixel(column, row, PALETTE[m_decodedRow[column]]);
                        }
                    }
                    break;

                case SCROLL_UP:
                    if (m_layers[i].m_field2 != 0)
                    {
                        uint8_t src_row = uint8_t((row + m_layers[i].m_pixels) % m_layers[i].m_field2 % Matrix::HEIGHT);
                        
                        decodeRow(data, src_row);
                        for (uint8_t column = 0; column < Matrix::WIDTH; ++column)
                        {
                            if (m_decodedRow[column] < PALETTE_SIZE)
                                matrix.setPixel(column, row, PALETTE[m_decodedRow[column]]);
                        }
                    }
                    break;
                }
            }
        }
    }
}

void SchemeState::decodeRow(const uint8_t *bitmap, uint8_t row)
{
    // There are 5-bit values packed into 8-bit types -
    // so each 5 bytes of data contains 8 pixels
    
    const uint8_t *data = bitmap + (row * (Matrix::WIDTH / 8 * 5));

    for (uint8_t column = 0; column < Matrix::WIDTH; column += 8, data += 5)
    {
        uint8_t b1 = data[0];
        uint8_t b2 = data[1];
        uint8_t b3 = data[2];
        uint8_t b4 = data[3];
        uint8_t b5 = data[4];

        uint8_t color;
        
        m_decodedRow[column + 0] = b1 >> 3;
        m_decodedRow[column + 1] = ((b1 & 0x07) << 2) | (b2 >> 6);
        m_decodedRow[column + 2] = (b2 >> 1) & 0x1F;
        m_decodedRow[column + 3] = ((b2 << 4) & 0x10) | ((b3 >> 4) & 0x0F);
        m_decodedRow[column + 4] = ((b3 << 1) & 0x1E) | ((b4 >> 7) & 0x01);
        m_decodedRow[column + 5] = (b4 >> 2) & 0x1F;
        m_decodedRow[column + 6] = ((b4 << 3) & 0x18) | ((b5 >> 5) & 0x07);
        m_decodedRow[column + 7] = b5 & 0x1F;
    }
}

void SchemeState::printState(Terminal &terminal) const
{
    terminal.print("Num layers = ");
    terminal.printInt(m_numLayers);
    terminal.newLine();
}

