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
    static constexpr size_t FLASH_PAGE_SIZE = 256;
    static constexpr size_t NUM_FLASH_PAGES = 6;
    
    explicit SchemeLibrary(SchemeState &state);
    
    bool loadDefaultScheme();
    void writeFlashPage(size_t page_num, const uint8_t *data);
    bool loadSchemeFromFlash();

private:
    static constexpr size_t BUFFER_SIZE = FLASH_PAGE_SIZE * NUM_FLASH_PAGES;
    
    SchemeState &m_curState;
    uint8_t m_ramBuffer[BUFFER_SIZE];
};

#endif // __SCHEME_LIBRARY_H__

