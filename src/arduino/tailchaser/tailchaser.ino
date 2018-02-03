/**
 * @file
 * 
 * Main application file for the TailChaser application.
 */

#include "matrix.h"
#include "menu.h"
#include "terminal.h"

Matrix matrix;
Menu menu(Serial, matrix);

void setup()
{
    pinMode(LED_BUILTIN, OUTPUT);
    Serial.begin(9600);
    matrix.init();

    for (int c = 0; c < 8; ++c)
    {
        for (int r = 0; r < 4; ++r)
        {
            int color = (c + (8 * r)) % 27;
            matrix.fillRect(4 * c, 4 * r, 4, 4, color);
        }
    }
    matrix.swapBuffers();
}

void loop()
{
    static int last_sec = 10000;
    int sec = millis() / 5000;
    if (last_sec != sec)
    {
        last_sec = sec;

        for (int c = 0; c < 8; ++c)
        {
            for (int r = 0; r < 4; ++r)
            {
                uint8_t color = 0;

                if ((c < 6) && (r < 3))
                {
                    color = (c % 3)
                        + (r * 3)
                        + ((c >= 3) ? 9 : 0)
                        + (9 * (sec % 3));

                    color = color % 27;
                }
                else if ((c == 7) && (r < 3))
                {
                    color = r;
                }
                else if ((r == 3) && (c < 6))
                {
                    if (c < 3)
                        color = 9 * c;
                    else
                        color = (3 * (c - 3));
                }
                else if ((c == 7) && (r == 3))
                {
                    color = 2 + (1 * 3);
                }
                    
                matrix.fillRect(4 * c, 4 * r, 4, 4, color);
            }
        }
        matrix.swapBuffers();
    }
    
    menu.processInput();
    //matrix.showNextRow();
}
