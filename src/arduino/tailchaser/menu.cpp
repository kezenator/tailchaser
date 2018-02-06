/**
 * @file
 * 
 * Header file for the Menu class.
 */

#include "matrix.h"
#include "menu.h"
#include "schemestate.h"
#include "signalstate.h"

Menu::Menu(Stream &stream, Matrix &matrix, SchemeState &scheme_state, SignalState &signal_state)
    : Terminal(stream)
    , m_matrix(matrix)
    , m_schemeState(scheme_state)
    , m_signalState(signal_state)
    , m_state(LINE_DATA)
    , m_length(0)
{
}

void Menu::sendStatus()
{
    print("S");
    if (m_signalState.isSet(SignalState::SIGNAL_TAIL))
        print("T");
    if (m_signalState.isSet(SignalState::SIGNAL_BRAKE))
        print("B");
    if (m_signalState.isSet(SignalState::SIGNAL_REVERSE))
        print("R");
    if (m_signalState.isSet(SignalState::SIGNAL_INDICATOR_SOLID))
        print("I");
    if (m_signalState.isSet(SignalState::SIGNAL_INDICATOR_FLASH))
        print("F");
    newLine();
}

void Menu::handleKey(int key)
{
    switch (m_state)
    {
    case LINE_DATA:
        if (key == '\r')
        {
            m_buffer[m_length] = 0;
            m_state = GOT_CR;
        }
        else if (key == '\n')
        {
            m_buffer[m_length] = 0;

            processLine();

            m_length = 0;
        }
        else
        {
            m_buffer[m_length] = key;
            m_length += 1;
    
            if (m_length >= BUFFER_LENGTH)
            {
                m_state = ERROR_WAIT_FOR_NL;
            }
        }
        break;

    case ERROR_WAIT_FOR_NL:
        if (key == '\n')
        {
            m_length = 0;
            m_state = LINE_DATA;
        }
        break;

    case GOT_CR:
        if (key == '\n')
        {
            m_buffer[m_length] = 0;
            processLine();

            m_length = 0;
            m_state = LINE_DATA;
        }
        else
        {
            m_state = ERROR_WAIT_FOR_NL;
        }
        break;
    }
}

void Menu::processLine()
{
    bool send_state = false;
    
    if ((m_length == 1)
        && (m_buffer[0] == 'H'))
    {
        // Heartbeat
        send_state = true;
    }
    else if ((m_length == 2)
        && (m_buffer[0] == 'T'))
    {
        // Toggle

        if (m_buffer[1] == 'T')
        {
            m_signalState.toggle(SignalState::SIGNAL_TAIL);
            send_state = true;
        }
        else if (m_buffer[1] == 'B')
        {
            m_signalState.toggle(SignalState::SIGNAL_BRAKE);
            send_state = true;
        }
        else if (m_buffer[1] == 'R')
        {
            m_signalState.toggle(SignalState::SIGNAL_REVERSE);
            send_state = true;
        }
        else if (m_buffer[1] == 'I')
        {
            m_signalState.toggle(SignalState::SIGNAL_INDICATOR_SOLID);
            send_state = true;
        }
    }

    if (send_state)
    {
        sendStatus();
    }
}

