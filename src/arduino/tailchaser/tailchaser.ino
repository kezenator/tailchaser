/**
 * @file
 * 
 * Main application file for the TailChaser application.
 */

#include "matrix.h"
#include "menu.h"
#include "schemelib.h"
#include "schemestate.h"
#include "signalstate.h"
#include "terminal.h"

Matrix matrix;
SchemeState cur_scheme;
SchemeLibrary library(cur_scheme);
SignalState signal_state;
Menu menu(Serial, matrix, cur_scheme, library, signal_state);

void setup()
{
    pinMode(LED_BUILTIN, OUTPUT);
    Serial.begin(9600);
    matrix.init();
    library.loadDefaultScheme();
}

void loop()
{
    menu.processInput();

    if (signal_state.update())
    {
        menu.sendStatus();
    }

    if (cur_scheme.update(signal_state))
    {
        cur_scheme.draw(matrix);
        matrix.swapBuffers();
    }
}
