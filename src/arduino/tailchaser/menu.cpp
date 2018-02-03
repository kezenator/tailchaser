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
{
}

void Menu::handleKey(int key)
{
    print("key=");
    printInt(key);
    newLine();

    if (key == '?')
    {
        println("? - Print help");
        println("p - print statistics");
        println("a - toggle tail");
        println("s - toggle brake");
        println("d - toggle reverse");
        println("f - toggle indicator");
    }
    else if (key == 'p')
    {    
        m_matrix.printStats(*this);
        m_schemeState.printState(*this);
    }
    else if (key == 'a')
    {
        m_signalState.toggle(SignalState::SIGNAL_TAIL);
        println("Toggled TAIL");
    }
    else if (key == 's')
    {
        m_signalState.toggle(SignalState::SIGNAL_BRAKE);
        println("Toggled BRAKE");
    }
    else if (key == 'd')
    {
        m_signalState.toggle(SignalState::SIGNAL_REVERSE);
        println("Toggled REVERSE");
    }
    else if (key == 'f')
    {
        m_signalState.toggle(SignalState::SIGNAL_INDICATOR_SOLID);
        println("Toggled INDICATOR");
    }
    else
    {
        println("Press '?' for help");
    }
}

