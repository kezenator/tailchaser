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
SchemeLibrary library;
SchemeState cur_scheme;
SignalState signal_state;
Menu menu(Serial, matrix, cur_scheme, signal_state);

void setup()
{
    pinMode(LED_BUILTIN, OUTPUT);
    Serial.begin(9600);
    matrix.init();
    library.loadDefaultScheme(cur_scheme);
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
