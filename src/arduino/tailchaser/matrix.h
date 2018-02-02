/**
 * @file
 * 
 * Header file for the Matrix class.
 */

#ifndef __MATRIX_H__
#define __MATRIX_H__

#if !defined(__AVR_ATmega2560__)
#error Code only works on AVR ATmega2560
#endif

class Matrix
{
public:
    Matrix() = default;
    ~Matrix() = default;
    Matrix(const Matrix &other) = delete;
    Matrix &operator =(const Matrix &other) = delete;
    
    void init();
    void showNextRow();

private:
    uint8_t m_Row;
};

#endif // __MATRIX_H__

