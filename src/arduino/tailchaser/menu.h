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
    void handleKey(int key) override;

private:
    Matrix &m_matrix;
    SchemeState &m_schemeState;
    SignalState &m_signalState;
};

#endif // __MENU_H__

