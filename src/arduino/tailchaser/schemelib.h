/**
 * @file
 * 
 * Header file for the SchemeLibrary class.
 */

#ifndef __SCHEME_LIBRARY_H__
#define __SCHEME_LIBRARY_H__

#include <stdint.h>
#include <stddef.h>

// Forward declarations
class SchemeState;

/**
 * Maintains the list of schemes supported by the device.
 */
class SchemeLibrary
{
public:
    bool loadDefaultScheme(SchemeState &state);
};

#endif // __SCHEME_LIBRARY_H__

