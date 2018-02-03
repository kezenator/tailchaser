/**
 * @file
 * 
 * Header file for the Terminal class.
 */

#ifndef __TERMINAL_H__
#define __TERMINAL_H__

#include "Arduino.h"

/**
 * Provides VT100 terminal services across a serial port.
 */
class Terminal
{
public:
    explicit Terminal(Stream &stream);
    ~Terminal();

    void init();
    void processInput();

    void print(const char *str);
    void println(const char *str);
    void printInt(int val);
    void newLine();

    virtual void handleKey(int key) = 0;

private:
    Stream &m_stream;
};

#endif // __TERMINAL_H__

