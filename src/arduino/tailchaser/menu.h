/**
 * @file
 * 
 * Header file for the Menu class.
 */

#ifndef __MENU_H__
#define __MENU_H__

#include "terminal.h"

// Forward definitions
class Matrix;
class SchemeState;
class SignalState;

/**
 * Implements the main menu system.
 */
class Menu: public Terminal
{
public:
    Menu(Stream &stream, Matrix &matrix, SchemeState &scheme_state, SignalState &signal_state);
    void sendStatus();
    void handleKey(int key) override;

private:

    void processLine();

    Matrix &m_matrix;
    SchemeState &m_schemeState;
    SignalState &m_signalState;

    enum States
    {
        LINE_DATA,
        ERROR_WAIT_FOR_NL,
        GOT_CR,
    };

    static constexpr size_t BUFFER_LENGTH = 100;
    
    States m_state;
    size_t m_length;
    char m_buffer[BUFFER_LENGTH];
};

#endif // __MENU_H__

