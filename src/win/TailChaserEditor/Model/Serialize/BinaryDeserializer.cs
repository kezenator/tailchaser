using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model.Serialize
{
    public class BinaryDeserializer
    {
        public BinaryDeserializer(byte[] bytes)
        {
            m_Bytes = bytes;
            m_Index = 0;
        }

        public byte ReadUint8()
        {
            if (m_Index >= m_Bytes.Length)
                throw new FormatException("More binary data expected");

            m_Index += 1;
            return m_Bytes[m_Index - 1];
        }

        public byte[] ReadBytes(int length)
        {
            if ((m_Index >= m_Bytes.Length)
                || (length > m_Bytes.Length)
                || ((m_Index + length) > m_Bytes.Length))
            {
                throw new FormatException("More binary data expected");
            }

            byte[] result = new byte[length];

            System.Array.Copy(m_Bytes, m_Index, result, 0, length);

            m_Index += length;

            return result;
        }

        public string ReadString()
        {
            byte utf8_len = ReadUint8();

            if (utf8_len == 0)
                return "";

            return Encoding.UTF8.GetString(ReadBytes(utf8_len));
        }

        public void CheckNoBytesRemaining()
        {
            if (m_Index != m_Bytes.Length)
                throw new FormatException("Unexpectd bytes at end of binary data");
        }

        private byte[] m_Bytes;
        private int m_Index;
    }
}
