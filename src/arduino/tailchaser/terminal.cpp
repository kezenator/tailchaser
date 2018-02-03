/**
 * @file
 * 
 * Implementation file for the Terminal class.
 */

#include "terminal.h"

Terminal::Terminal(Stream &stream)
    : m_stream(stream)
{
}

Terminal::~Terminal()
{
}

void Terminal::processInput()
{
    while (m_stream.available())
    {
        handleKey(m_stream.read());
    }
}

void Terminal::print(const char *str)
{
    m_stream.print(str);
}

void Terminal::println(const char *str)
{
    m_stream.println(str);
}

void Terminal::printInt(int val)
{
    m_stream.print(val);
}

void Terminal::newLine()
{
    m_stream.print("\r\n");
}

