/**
 * @file
 * 
 * Main application file for the TailChaser application.
 */

#include "matrix.h"

Matrix matrix;

void setup()
{
  pinMode(LED_BUILTIN, OUTPUT);
  matrix.init();
}

void loop()
{
    matrix.showNextRow();
}
