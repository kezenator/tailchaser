/**
 * @file
 * 
 * Header file for the Matrix class.
 */

#ifndef __MATRIX_H__
#define __MATRIX_H__

#include "Arduino.h"
#include <stdint.h>

#if !defined(__AVR_ATmega2560__)
#error Code only works on AVR ATmega2560
#endif

// Forward definitions
class Terminal;
extern "C" void TIMER1_OVF_vect(void) __attribute__ ((signal));

/**
 * A class that manages the 16x32 RGB LED matrix panel.
 */
class Matrix
{
public:

    static constexpr uint8_t WIDTH = 32;
    static constexpr uint8_t HEIGHT = 16;
    static constexpr uint8_t DISPLAY_ROWS = HEIGHT / 2;
    static constexpr uint8_t DISPLAY_BITS = 3;
    
    Matrix() = default;
    ~Matrix() = default;
    Matrix(const Matrix &other) = delete;
    Matrix &operator =(const Matrix &other) = delete;
    
    void init();
    void showNextRow();
    void printStats(Terminal &terminal) const;

    void setPixel(uint8_t x, uint8_t y, uint16_t color)
    {
        if ((x < WIDTH) && (y < HEIGHT))
            _setPixel(x, y, color);
    }

    void fillScreen(uint16_t color);
    void fillRect(uint8_t x, uint8_t y, uint8_t width, uint8_t height, uint16_t color);

    void swapBuffers();

private:

    void _setPixel(uint8_t x, uint8_t y, uint16_t color);

    struct OutputBits
    {
        uint8_t port_e;
        uint8_t port_g;
        uint8_t port_h;
    };

    struct OutputBuffer
    {
        OutputBits bits[DISPLAY_ROWS][WIDTH];
    };

    //struct Measure
    //{
    //    Measure(int &max_ref)
    //        : m_startTime(micros())
    //        , m_maxRef(max_ref)
    //    {
    //    }
    //
    //    ~Measure()
    //    {
    //        int delay = micros() - m_startTime;
    //        if (delay > m_maxRef)
    //            m_maxRef = delay;
    //    }
    //
    //private:
    //    int m_startTime;
    //    int &m_maxRef;
    //};

    friend void TIMER1_OVF_vect();

    static Matrix *g_activeMatrix;

    uint8_t m_DisplayIndex;
    uint8_t m_RowIndex;
    uint8_t m_DitherIndex;
    int m_MaxRowTimeMicros;
    int m_MaxDrawTimeMicros;
    int m_MaxCalcTimeMicros;

    OutputBuffer m_outputBuffers[2][DISPLAY_BITS];
};

#endif // __MATRIX_H__

