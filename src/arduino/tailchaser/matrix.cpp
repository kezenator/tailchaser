/**
 * @file
 * 
 * Implementation file for the CMatrix class.
 */

#include "Arduino.h"
#include "matrix.h"

#define MATRIX_PIN_R1  2
#define MATRIX_PIN_G1  3
#define MATRIX_PIN_B1  4
#define MATRIX_PIN_R2  5
#define MATRIX_PIN_G2  6
#define MATRIX_PIN_B2  7
#define MATRIX_PIN_A   A0
#define MATRIX_PIN_B   A1
#define MATRIX_PIN_C   A2
#define MATRIX_PIN_CLK 8
#define MATRIX_PIN_LAT A3
#define MATRIX_PIN_OE  9

void Matrix::init()
{
    auto init_pin = [](int pin)
    {
        pinMode(pin, OUTPUT);
        digitalWrite(pin, LOW);
    };

    init_pin(MATRIX_PIN_R1);
    init_pin(MATRIX_PIN_G1);
    init_pin(MATRIX_PIN_B1);
    init_pin(MATRIX_PIN_R2);
    init_pin(MATRIX_PIN_G2);
    init_pin(MATRIX_PIN_B2);
    init_pin(MATRIX_PIN_A);
    init_pin(MATRIX_PIN_B);
    init_pin(MATRIX_PIN_C);
    init_pin(MATRIX_PIN_CLK);
    init_pin(MATRIX_PIN_LAT);
    init_pin(MATRIX_PIN_OE);
    digitalWrite(MATRIX_PIN_OE, HIGH);

    m_Row = 0;
}

void Matrix::showNextRow()
{
    // Work out the colors

    int color = (millis() / 2000 + 1) % 8;

    uint8_t red   = (color & 1) ? 1 : 0;
    uint8_t green = (color & 2) ? 1 : 0;
    uint8_t blue  = (color & 4) ? 1 : 0;

    // Clock out the data

    for (int i = 0; i < 4; ++i)
    {
        digitalWrite(MATRIX_PIN_R1, red);
        digitalWrite(MATRIX_PIN_G1, green);
        digitalWrite(MATRIX_PIN_B1, blue);
        digitalWrite(MATRIX_PIN_R2, red);
        digitalWrite(MATRIX_PIN_G2, green);
        digitalWrite(MATRIX_PIN_B2, blue);

        digitalWrite(MATRIX_PIN_CLK, HIGH);
        digitalWrite(MATRIX_PIN_CLK, LOW);
    }

    // Turn off the output enable
    
    digitalWrite(MATRIX_PIN_OE, HIGH);

    // Set the address bits

    if (m_Row & 1) digitalWrite(MATRIX_PIN_A, HIGH); else digitalWrite(MATRIX_PIN_A, LOW);
    if (m_Row & 2) digitalWrite(MATRIX_PIN_B, HIGH); else digitalWrite(MATRIX_PIN_B, LOW);
    if (m_Row & 4) digitalWrite(MATRIX_PIN_C, HIGH); else digitalWrite(MATRIX_PIN_C, LOW);

    // Latch the data

    digitalWrite(MATRIX_PIN_LAT, HIGH);
    digitalWrite(MATRIX_PIN_LAT, LOW);

    // Display the row

    digitalWrite(MATRIX_PIN_OE, LOW);

    // Increment the row

    m_Row = (m_Row + 1) % 8;
}

