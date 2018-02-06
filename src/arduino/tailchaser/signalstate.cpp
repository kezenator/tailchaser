/**
 * @file
 * 
 * Implementation file for the SignalState class.
 */

#include "signalstate.h"
#include "Arduino.h"

SignalState::SignalState()
    : m_value(0)
    , m_lastReportedValue(0xFF)
{
}

bool SignalState::update()
{
    if (m_value & SIGNAL_INDICATOR_SOLID)
    {
        int delay = millis() - m_indicatorMillis;

        if (delay > 500)
        {
            m_value ^= SIGNAL_INDICATOR_FLASH;
            m_indicatorMillis += 500;
        }
    }

    if (m_lastReportedValue != m_value)
    {
        m_lastReportedValue = m_value;
        return true;
    }
    return false;
}

void SignalState::toggle(uint8_t mask)
{
    m_value ^= mask & (SIGNAL_TAIL | SIGNAL_BRAKE | SIGNAL_REVERSE | SIGNAL_INDICATOR_SOLID);

    if (mask & SIGNAL_INDICATOR_SOLID)
    {
        if (m_value & SIGNAL_INDICATOR_SOLID)
        {
            m_value |= SIGNAL_INDICATOR_FLASH;
            m_indicatorMillis = millis();
        }
        else
        {
            m_value &= ~SIGNAL_INDICATOR_FLASH;
        }
    }
}

