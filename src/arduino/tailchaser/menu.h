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

/**
 * Implements the main menu system.
 */
class Menu: public Terminal
{
public:
    Menu(Stream &stream, Matrix &matrix);
    void handleKey(int key) override;

private:
    Matrix &m_matrix;
};

#endif // __MENU_H__

