/**
 * @file
 * 
 * Implementation file for the SchemeLibrary class.
 */

#include <avr/pgmspace.h>
#include "schemelib.h"
#include "schemestate.h"

#include "default_scheme.h"

SchemeLibrary::SchemeLibrary(SchemeState &state)
    : m_curState(state)
{
}

bool SchemeLibrary::loadDefaultScheme()
{
    m_curState.clearScheme();

    if (default_scheme_size > (BUFFER_SIZE - 2))
        return false;

    m_ramBuffer[0] = default_scheme_size;
    m_ramBuffer[1] = default_scheme_size >> 8;
    memcpy_P(m_ramBuffer + 2, default_scheme, default_scheme_size);

    return m_curState.loadSchemeFromRam(m_ramBuffer, BUFFER_SIZE);
}

void SchemeLibrary::writeFlashPage(size_t page_num, const uint8_t *data)
{
    m_curState.clearScheme();
    
    if (page_num < NUM_FLASH_PAGES)
    {
        memcpy(m_ramBuffer + (page_num * FLASH_PAGE_SIZE), data, FLASH_PAGE_SIZE);
    }
}

bool SchemeLibrary::loadSchemeFromFlash()
{
    return m_curState.loadSchemeFromRam(m_ramBuffer, BUFFER_SIZE);
}

