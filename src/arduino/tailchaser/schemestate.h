/**
 * @file
 * 
 * Header file for the SchemeState class.
 */

#ifndef __SCHEME_STATE_H__
#define __SCHEME_STATE_H__

#include <stdint.h>
#include <string.h>

#include "Matrix.h"

// Forward definitions
class SignalState;
class Terminal;

/**
 * Loads schemes from programme memory and
 * maintains the current state of display.
 */
class SchemeState
{
public:

    static constexpr uint8_t MAX_LAYERS = 8;
    static constexpr size_t BITMAP_BYTES = 320;

    enum PATTERN
    {
        SOLID = 0,
        FLASH = 1,
        SWIPE_LEFT = 2,
        SWIPE_RIGHT = 3,
        SWIPE_DOWN = 4,
        SWIPE_UP = 5,
        SCROLL_LEFT = 6,
        SCROLL_RIGHT = 7,
        SCROLL_DOWN = 8,
        SCROLL_UP = 9,
    };

    SchemeState();

    void clearScheme();
    bool loadSchemeFromRam(const uint8_t *data, size_t max_length);

    bool update(const SignalState &signal_state);
    void draw(Matrix &matrix);

    void printState(Terminal &terminal) const;

private:

    struct LayerInfo
    {
        bool m_currentlyDisplayed;
        unsigned long m_startMillis;
        uint8_t m_pixels;
        
        uint8_t m_conditionMask;
        uint8_t m_conditionValue;
        const uint8_t *m_bitmapBase;
        PATTERN m_pattern;
        uint16_t m_field1;
        uint16_t m_field2;
        uint16_t m_field3;
    };

    void decodeRow(const uint8_t *bitmap, uint8_t row);
    bool skipString(const uint8_t *&data, size_t &length);

    bool m_forceRedrawRequired;
    const uint8_t *m_memBase;
    uint8_t m_numLayers;
    LayerInfo m_layers[MAX_LAYERS];

    uint8_t m_decodedRow[Matrix::WIDTH];
};

#endif // __SCHEME_STATE_H__

