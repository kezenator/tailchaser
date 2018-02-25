using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model.Serialize
{
    public class BinarySerializer
    {
        public BinarySerializer()
        {
            m_Lines = new List<KeyValuePair<byte[], string>>();
            m_TotalBytes = 0;
            m_CurLine = new List<byte>();
        }

        public int TotalBytes
        {
            get
            {
                return m_TotalBytes;
            }
        }

        public List<KeyValuePair<byte[], string>> Lines
        {
            get
            {
                return m_Lines;
            }
        }

        public byte[] Bytes
        {
            get
            {
                byte[] result = new byte[m_TotalBytes];

                int index = 0;
                foreach (KeyValuePair<byte[], string> entry in m_Lines)
                {
                    System.Array.Copy(entry.Key, 0, result, index, entry.Key.Length);
                    index += entry.Key.Length;
                }

                return result;
            }
        }

        public void CommitLine(string comment)
        {
            m_TotalBytes += m_CurLine.Count;
            m_Lines.Add(new KeyValuePair<byte[], string>(m_CurLine.ToArray(), comment));
            m_CurLine = new List<byte>();
        }

        public void CommitLineFormat(string format, params object[] values)
        {
            CommitLine(string.Format(format, values));
        }

        public void WriteIntAsUint8(int value, string error_msg)
        {
            if ((value < 0) || (value > 255))
                throw new FormatException(error_msg);

            m_CurLine.Add((byte)value);
        }

        public void WriteUint16(int value)
        {
            m_CurLine.Add((byte)value);
            m_CurLine.Add((byte)(value >> 8));
        }

        public void WriteLengthAndUtf8String(string value, string error_msg)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);

                WriteIntAsUint8(bytes.Length, error_msg);
                WriteBytes(bytes);
            }
            catch (Exception)
            {
                throw new FormatException(error_msg);
            }
        }

        public void WriteBytes(byte[] bytes)
        {
            foreach (byte value in bytes)
                m_CurLine.Add(value);
        }

        List<KeyValuePair<byte[], string>> m_Lines;
        int m_TotalBytes;
        List<byte> m_CurLine;
    }
}
