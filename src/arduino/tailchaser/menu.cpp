/**
 * @file
 * 
 * Header file for the Menu class.
 */

#include "matrix.h"
#include "menu.h"

Menu::Menu(Stream &stream, Matrix &matrix)
    : Terminal(stream)
    , m_matrix(matrix)
{
}

void Menu::handleKey(int key)
{
    print("key=");
    printInt(key);
    newLine();
    
    m_matrix.printStats(*this);
}

