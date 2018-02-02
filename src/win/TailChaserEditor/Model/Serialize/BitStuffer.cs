using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model.Serialize
{
    public class BitStuffer
    {
        public BitStuffer()
        {
            m_Bytes = new List<byte>();
            m_BitsInLastByte = 8;
        }

        public byte[] Bytes
        {
            get
            {
                return m_Bytes.ToArray();
            }
        }

        public void AddBits(byte value, int num_bits)
        {
            Debug.Assert(num_bits > 0);
            Debug.Assert(num_bits <= 8);

            // Work out the bit-shift of the highest bit
            // in the value

            int cur_value_shift = num_bits - 1;

            // Keep inserting bits into bytes
            // until all bits are inserted

            while (num_bits > 0)
            {
                // Work out how many bytes are free
                // in the last byte of the list, and
                // add a new byte if we're totally full already

                int remaining_in_last_byte = 8 - m_BitsInLastByte;

                if (remaining_in_last_byte == 0)
                {
                    m_Bytes.Add(0);
                    m_BitsInLastByte = 0;
                    remaining_in_last_byte = 8;
                }

                // Work out how many bits to insert
                // from the value into the last byte

                int this_time = num_bits;
                if (this_time > remaining_in_last_byte)
                    this_time = remaining_in_last_byte;

                // Work out the shift of the highest bit
                // to insert into the current byte

                int cur_dest_shift = remaining_in_last_byte - 1;

                // Insert the required number of bits
                // into the last byte

                byte last_byte = m_Bytes[m_Bytes.Count - 1];

                for (int i = 0; i < this_time; ++i)
                {
                    byte value_bit = (byte)((value >> cur_value_shift) & 0x01);

                    last_byte |= (byte)(value_bit << cur_dest_shift);

                    cur_value_shift -= 1;
                    cur_dest_shift -= 1;
                }

                m_Bytes[m_Bytes.Count - 1] = last_byte;

                num_bits -= this_time;
                m_BitsInLastByte += this_time;
            }
        }

        private List<byte> m_Bytes;
        private int m_BitsInLastByte;
    }
}
