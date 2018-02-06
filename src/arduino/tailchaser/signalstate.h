/**
 * @file
 * 
 * Header file for the SignalState class.
 */

#ifndef __SIGNAL_STATE_H__
#define __SIGNAL_STATE_H__

#include <stdint.h>

/**
 * Maintains the state of the signals.
 */
class SignalState
{
    SignalState(const SignalState &) = delete;
    SignalState &operator =(const SignalState &) = delete;

public:

    static constexpr uint8_t SIGNAL_TAIL            = 0x01;
    static constexpr uint8_t SIGNAL_BRAKE           = 0x02;
    static constexpr uint8_t SIGNAL_REVERSE         = 0x04;
    static constexpr uint8_t SIGNAL_INDICATOR_FLASH = 0x08;
    static constexpr uint8_t SIGNAL_INDICATOR_SOLID = 0x10;

    SignalState();
    ~SignalState() = default;

    bool matchesLayerMask(uint8_t layer_mask, uint8_t layer_value) const
    {
        return (m_value & layer_mask) == layer_value;
    }

    bool isSet(uint8_t mask) const
    {
        return (m_value & mask) != 0;
    }

    bool update();
    void toggle(uint8_t mask);

private:
    uint8_t m_value;
    uint8_t m_lastReportedValue;
    int m_indicatorMillis;
};

#endif // __SCHEME_STATE_H__

