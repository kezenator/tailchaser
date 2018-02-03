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

static constexpr uint16_t TIMER_PRESCALER = 8;

static constexpr uint16_t MIN_CLKS = 1200;
static constexpr uint16_t MID_CLKS = 4000;
static constexpr uint16_t MAX_CLKS = 8000;

static constexpr uint16_t MIN_TIMER_INIT = uint16_t(65536UL - (MIN_CLKS / TIMER_PRESCALER));
static constexpr uint16_t MID_TIMER_INIT = uint16_t(65536UL - (MID_CLKS / TIMER_PRESCALER));
static constexpr uint16_t MAX_TIMER_INIT = uint16_t(65536UL - (MAX_CLKS / TIMER_PRESCALER));

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

    // Swap buffers twice:
    // 1) Once to zero a new buffer
    // 2) Twice to display the zeroed buffer
    //    and prepare a new one for drawing

    swapBuffers();
    swapBuffers();

    g_activeMatrix = this;

    noInterrupts();           // disable all interrupts
    TCCR1A = 0;
    TCCR1B = 0;

    TCNT1 = MIN_TIMER_INIT;
    TCCR1B |= (1 << CS11);    // /8 prescaler 
    TIMSK1 |= (1 << TOIE1);   // enable timer overflow interrupt
    interrupts();             // enable all interrupts
}

ISR(TIMER1_OVF_vect)        
{
    if (Matrix::g_activeMatrix->m_DitherIndex == 0)
        TCNT1 = MIN_TIMER_INIT;
    else if (Matrix::g_activeMatrix->m_DitherIndex == 1)
        TCNT1 = MID_TIMER_INIT;
    else
        TCNT1 = MAX_TIMER_INIT;
    
    Matrix::g_activeMatrix->showNextRow();
}

void Matrix::showNextRow()
{
    //Measure measure(m_MaxRowTimeMicros);
    
    // First, we need to clock out 32-pixels of
    // 6 bits of data for the RGB values for 2
    // of the 16 rows.
    //
    // Port E and G only contain
    // the data bits - so we can write directly to them.
    //
    // Port H contains two data bits (PH3 and PH4) as
    // well as the CLK (PH5) and OE (PH5).
    //
    // CLK and OE are both low at this point in time -
    // so we can directly write to port H as well.
    // (because swapBuffers sets all other bits of
    // the port H data to zero).
    //
    // The final two writes to port H are to toggle the clock.
    //
    // Also, the loop across 32 pixels has been unrolled.

    OutputBits *bits = m_outputBuffers[m_DisplayIndex][m_DitherIndex].bits[m_RowIndex];

#define CLOCK_1_BIT   PORTE = bits->port_e; PORTG = bits->port_g; PORTH = bits->port_h; PORTH |= 0x20; PORTH &= ~0x20; ++bits;
#define CLOCK_4_BITS  CLOCK_1_BIT  CLOCK_1_BIT  CLOCK_1_BIT  CLOCK_1_BIT
#define CLOCK_16_BITS CLOCK_4_BITS CLOCK_4_BITS CLOCK_4_BITS CLOCK_4_BITS

    CLOCK_16_BITS
    CLOCK_16_BITS

#undef CLOCK_16_BITS
#undef CLOCK_4_BITS
#undef CLOCK_1_BIT

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
        m_DitherIndex = (m_DitherIndex + 1) % 3;
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

void Matrix::_setPixel(uint8_t x, uint8_t y, uint16_t color)
{
    // NOTE - this function is private an assumes that the
    // x and y co-ordinates are valid
    
    // Work out which buffer we're writing to

    uint8_t back_buffer = m_DisplayIndex ^ 1;

    // Calculate the pixel data to write out:
    // 1) Each row display actually contains data for
    //    two physical rows on the screen
    // 2) Pin assignments are:
    //      R1  PE4    R2 PE3
    //      G1  PE5    G2 PH3
    //      B1  PG5    B2 PH4

    if (y < DISPLAY_ROWS)
    {
        // RGB1 signals

        uint8_t e0 = m_outputBuffers[back_buffer][0].bits[y][x].port_e & ~0x30;
        uint8_t g0 = m_outputBuffers[back_buffer][0].bits[y][x].port_g & ~0x20;
        uint8_t e1 = m_outputBuffers[back_buffer][1].bits[y][x].port_e & ~0x30;
        uint8_t g1 = m_outputBuffers[back_buffer][1].bits[y][x].port_g & ~0x20;
        uint8_t e2 = m_outputBuffers[back_buffer][2].bits[y][x].port_e & ~0x30;
        uint8_t g2 = m_outputBuffers[back_buffer][2].bits[y][x].port_g & ~0x20;

        if (color & 0x0001) e0 |= 0x10;
        if (color & 0x0002) e1 |= 0x10;
        if (color & 0x0004) e2 |= 0x10;

        if (color & 0x0008) e0 |= 0x20;
        if (color & 0x0010) e1 |= 0x20;
        if (color & 0x0020) e2 |= 0x20;

        if (color & 0x0040) g0 |= 0x20;
        if (color & 0x0080) g1 |= 0x20;
        if (color & 0x0100) g2 |= 0x20;
        
        m_outputBuffers[back_buffer][0].bits[y][x].port_e = e0;
        m_outputBuffers[back_buffer][0].bits[y][x].port_g = g0;
        m_outputBuffers[back_buffer][1].bits[y][x].port_e = e1;
        m_outputBuffers[back_buffer][1].bits[y][x].port_g = g1;
        m_outputBuffers[back_buffer][2].bits[y][x].port_e = e2;
        m_outputBuffers[back_buffer][2].bits[y][x].port_g = g2;
    }
    else
    {
        // RGB2 signals

        uint8_t e0 = m_outputBuffers[back_buffer][0].bits[y - DISPLAY_ROWS][x].port_e & ~0x08;
        uint8_t h0 = m_outputBuffers[back_buffer][0].bits[y - DISPLAY_ROWS][x].port_h & ~0x18;
        uint8_t e1 = m_outputBuffers[back_buffer][1].bits[y - DISPLAY_ROWS][x].port_e & ~0x08;
        uint8_t h1 = m_outputBuffers[back_buffer][1].bits[y - DISPLAY_ROWS][x].port_h & ~0x18;
        uint8_t e2 = m_outputBuffers[back_buffer][2].bits[y - DISPLAY_ROWS][x].port_e & ~0x08;
        uint8_t h2 = m_outputBuffers[back_buffer][2].bits[y - DISPLAY_ROWS][x].port_h & ~0x18;

        if (color & 0x0001) e0 |= 0x08;
        if (color & 0x0002) e1 |= 0x08;
        if (color & 0x0004) e2 |= 0x08;

        if (color & 0x0008) h0 |= 0x08;
        if (color & 0x0010) h1 |= 0x08;
        if (color & 0x0020) h2 |= 0x08;

        if (color & 0x0040) h0 |= 0x10;
        if (color & 0x0080) h1 |= 0x10;
        if (color & 0x0100) h2 |= 0x10;
        
        m_outputBuffers[back_buffer][0].bits[y - DISPLAY_ROWS][x].port_e = e0;
        m_outputBuffers[back_buffer][0].bits[y - DISPLAY_ROWS][x].port_h = h0;
        m_outputBuffers[back_buffer][1].bits[y - DISPLAY_ROWS][x].port_e = e1;
        m_outputBuffers[back_buffer][1].bits[y - DISPLAY_ROWS][x].port_h = h1;
        m_outputBuffers[back_buffer][2].bits[y - DISPLAY_ROWS][x].port_e = e2;
        m_outputBuffers[back_buffer][2].bits[y - DISPLAY_ROWS][x].port_h = h2;
    }
}

void Matrix::fillScreen(uint16_t color)
{
    //Measure measure(m_MaxDrawTimeMicros);

    for (uint8_t r = 0; r < HEIGHT; ++r)
        for (uint8_t c = 0; c < WIDTH; ++c)
            _setPixel(c, r, color);
}

void Matrix::fillRect(uint8_t x, uint8_t y, uint8_t width, uint8_t height, uint16_t color)
{
    //Measure measure(m_MaxDrawTimeMicros);

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
        for (uint8_t c = 0; c < width; ++c)
        {
            _setPixel(x + c, y + r, color);
        }
    }
}

void Matrix::swapBuffers()
{
    //Measure measure(m_MaxCalcTimeMicros);

    // The function _setPixel has already formatted
    // all of the data in the correct format for writing
    // to the display panel - we just need to
    // update the display index to cause the ISR
    // to write data from the next bitmap.
    //
    // Also, we should reset the back-buffer to black
    // for the next frame to be written to it.

    m_DisplayIndex ^= 1;

    memset(m_outputBuffers[m_DisplayIndex ^ 1], 0, sizeof(m_outputBuffers[0]));
}

