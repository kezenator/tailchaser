/**
 * @file
 * 
 * Header file for the SchemeState class.
 */

#ifndef __SCHEME_STATE_H__
#define __SCHEME_STATE_H__

#include <stdint.h>
#include <string.h>

// Forward definitions
class Matrix;
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

    SchemeState();

    bool loadScheme(const uint8_t *data, size_t length);
    bool update(const SignalState &signal_state);
    void draw(Matrix &matrix);

    void printState(Terminal &terminal) const;

private:

    struct LayerInfo
    {
        bool m_currentlyDisplayed;
        uint8_t m_conditionMask;
        uint8_t m_conditionValue;
        const uint8_t *m_bitmapBase;
    };

    bool skipString(const uint8_t *&data, size_t &length);

    bool m_forceRedrawRequired;
    const uint8_t *m_memBase;
    uint8_t m_numLayers;
    LayerInfo m_layers[MAX_LAYERS];
};

#endif // __SCHEME_STATE_H__

