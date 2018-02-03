/**
 * @file
 * 
 * Implementation file for the SchemeLibrary class.
 */

#include <avr/pgmspace.h>
#include "schemelib.h"
#include "schemestate.h"

#include "default_scheme.h"

bool SchemeLibrary::loadDefaultScheme(SchemeState &state)
{
    return state.loadScheme(default_scheme, default_scheme_size);
}

