/**
 * @file
 * 
 * Implementation file for the CMatrix class.
 */

#include "Arduino.h"
#include "matrix.h"
#include "terminal.h"

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

Matrix *Matrix::g_activeMatrix = nullptr;

// Defined the overall refresh-rate (in Hz),
// plus the fraction of time the dithered pixels are off (numerator/denum)
// plus the timer pre-scaler

static constexpr uint16_t REFRESH_RATE = 80 * Matrix::DISPLAY_ROWS;
static constexpr uint16_t OFF_NUMERATOR = 1;
static constexpr uint16_t OFF_DENOM = 12;
static constexpr uint16_t TIMER_PRESCALER = 8;

// Calculate the number of timer counts for the on and off times

static constexpr uint16_t OFF_COUNT = uint16_t(16000000ULL / TIMER_PRESCALER * (OFF_DENOM - OFF_NUMERATOR) / OFF_DENOM / REFRESH_RATE);
static constexpr uint16_t ON_COUNT = uint16_t(16000000ULL / TIMER_PRESCALER * OFF_NUMERATOR / OFF_DENOM / REFRESH_RATE);

// Check that we've got enough time to run the ISR
// NOTE - the duration is an empirically determined from testing....

static constexpr uint16_t ISR_DURATION_CLKS = 2000;

static_assert(uint32_t(ISR_DURATION_CLKS) <= (uint32_t(OFF_COUNT) * uint32_t(TIMER_PRESCALER)), "Off time is too short");
static_assert(uint32_t(ISR_DURATION_CLKS) <= (uint32_t(ON_COUNT) * uint32_t(TIMER_PRESCALER)), "On time is too short");

// Timer works in increment until overflow mode - subtract from 16-bit max.

static constexpr uint16_t OFF_TIMER_INIT = uint16_t(65536UL - OFF_COUNT);
static constexpr uint16_t ON_TIMER_INIT = uint16_t(65536UL - ON_COUNT);

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

    m_DisplayIndex = 0;
    m_RowIndex = 0;
    m_DitherIndex = 0;

    m_MaxRowTimeMicros = 0;
    m_MaxDrawTimeMicros = 0;
    m_MaxCalcTimeMicros = 0;

    fillScreen(1);
    swapBuffers();

    g_activeMatrix = this;

    noInterrupts();           // disable all interrupts
    TCCR1A = 0;
    TCCR1B = 0;

    TCNT1 = ON_TIMER_INIT;
    TCCR1B |= (1 << CS11);    // /8 prescaler 
    TIMSK1 |= (1 << TOIE1);   // enable timer overflow interrupt
    interrupts();             // enable all interrupts
}

ISR(TIMER1_OVF_vect)        
{
    if (Matrix::g_activeMatrix->m_DitherIndex == 0)
        TCNT1 = OFF_TIMER_INIT;
    else
        TCNT1 = ON_TIMER_INIT;
    
    Matrix::g_activeMatrix->showNextRow();
}

void Matrix::showNextRow()
{
    Measure measure(m_MaxRowTimeMicros);
    
    // Clock out the data

    OutputBits *bits = m_outputBuffers[m_DisplayIndex][m_DitherIndex].bits[m_RowIndex];
    for (int i = 0; i < WIDTH; ++i, ++bits)
    {
        // Write data bits
        
        PORTE &= ~0x38;
        PORTG &= ~0x20;
        PORTH &= ~0x18;

        PORTE |= bits->port_e;
        PORTG |= bits->port_g;
        PORTH |= bits->port_h;

        // Toggle clock

        PORTH |= 0x20;
        PORTH &= ~0x20;
    }

    // Turn off the output enable by setting it high

    PORTH |= 0x40;

    // Set the address bits

    PORTF &= ~0x07;
    PORTF |= m_RowIndex;

    // Latch the data

    PORTF |= 0x08;
    PORTF &= ~0x08;

    // Display the row by setting OE low again

    PORTH &= ~0x40;

    // Increment the row, reset, and change
    // the dither index for the next display

    m_RowIndex += 1;

    if (m_RowIndex == DISPLAY_ROWS)
    {
        m_RowIndex = 0;
        m_DitherIndex = (m_DitherIndex + 1) & 1;
    }
}

void Matrix::printStats(Terminal &terminal) const
{
    auto print_measurement = [&terminal](const char *label, const int &val)
    {
        terminal.print(label);
        terminal.print(" (us) = ");
        terminal.printInt(val);
        terminal.newLine();
    };

    print_measurement("Row ", m_MaxRowTimeMicros);
    print_measurement("Draw", m_MaxDrawTimeMicros);
    print_measurement("Calc", m_MaxCalcTimeMicros);
}

void Matrix::fillScreen(uint8_t color)
{
    Measure measure(m_MaxDrawTimeMicros);
    
    memset(m_Bitmap, color, WIDTH * HEIGHT);
}

void Matrix::fillRect(uint8_t x, uint8_t y, uint8_t width, uint8_t height, uint8_t color)
{
    Measure measure(m_MaxDrawTimeMicros);

    if ((x >= WIDTH)
        || (y >= HEIGHT)
        || (width > WIDTH)
        || (height > HEIGHT)
        || ((x + width) > WIDTH)
        || ((y + height) > HEIGHT))
    {
        return;
    }

    for (uint8_t r = 0; r < height; ++r)
    {
        memset(m_Bitmap[y + r] + x, color, width);
    }
}

void Matrix::swapBuffers()
{
    Measure measure(m_MaxCalcTimeMicros);

    // This function takes bitmap data in the m_Bitmap back
    // buffer, transforms it into the correct port bit data
    // in the back output buffer, and then swaps the
    // output buffers to display.

    // Work out which buffer we're writing to

    uint8_t back_buffer = (m_DisplayIndex + 1) & 1;

    // Calculate the pixel data to write out:
    // 1) Each row display actually contains data for
    //    two physical rows on the screen
    // 2) Pin assignments are:
    //      R1  PE4    R2 PE3
    //      G1  PE5    G2 PH3
    //      B1  PG5    B2 PH4
    // 

    for (uint8_t row = 0; row < DISPLAY_ROWS; ++row)
    {
        for (uint8_t column = 0; column < WIDTH; ++column)
        {
            uint8_t e1 = 0;
            uint8_t g1 = 0;
            uint8_t h1 = 0;
            uint8_t e2 = 0;
            uint8_t g2 = 0;
            uint8_t h2 = 0;

            uint8_t color = PaletteColorToBits(m_Bitmap[row][column]);

            if (color & 0x01) e1 |= 0x10;
            if (color & 0x02) e2 |= 0x10;

            if (color & 0x04) e1 |= 0x20;
            if (color & 0x08) e2 |= 0x20;

            if (color & 0x10) g1 |= 0x20;
            if (color & 0x20) g2 |= 0x20;

            color = PaletteColorToBits(m_Bitmap[row + DISPLAY_ROWS][column]);

            if (color & 0x01) e1 |= 0x08;
            if (color & 0x02) e2 |= 0x08;

            if (color & 0x04) h1 |= 0x08;
            if (color & 0x08) h2 |= 0x08;

            if (color & 0x10) h1 |= 0x10;
            if (color & 0x20) h2 |= 0x10;

            m_outputBuffers[back_buffer][0].bits[row][column].port_e = e1;
            m_outputBuffers[back_buffer][0].bits[row][column].port_g = g1;
            m_outputBuffers[back_buffer][0].bits[row][column].port_h = h1;
            m_outputBuffers[back_buffer][1].bits[row][column].port_e = e2;
            m_outputBuffers[back_buffer][1].bits[row][column].port_g = g2;
            m_outputBuffers[back_buffer][1].bits[row][column].port_h = h2;
        }
    }

    // Finally, swap the display bufers

    m_DisplayIndex = back_buffer;
}

uint8_t Matrix::PaletteColorToBits(uint8_t color)
{
    // Takes a palette entry which is three-levels for
    // each of red, green and blue - i.e. 0..26.
    //
    // Returns a bitmap
    // 0x01 = red   dither 0
    // 0x02 = red   dither 1
    // 0x04 = green dither 0
    // 0x08 = green dither 1
    // 0x10 = blue  dither 0
    // 0x20 = blue  dither 1

    if (color > 26)
        color = 0;

    uint8_t red   = color % 3;
    uint8_t green = (color / 3) % 3;
    uint8_t blue  = color / 9;

    uint8_t result = 0;

    if (red == 2)
        result |= 0x03;
    else if (red == 1)
        result |= 0x02;

    if (green == 2)
        result |= 0x0C;
    else if (green == 1)
        result |= 0x08;

    if (blue == 2)
        result |= 0x30;
    else if (blue == 1)
        result |= 0x20;

    return result;
}

