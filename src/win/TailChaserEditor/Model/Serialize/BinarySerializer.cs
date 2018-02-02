﻿using System;
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
            m_CurLine = new List<byte>();
        }

        public List<KeyValuePair<byte[], string>> Lines
        {
            get
            {
                return m_Lines;
            }
        }

        public void CommitLine(string comment)
        {
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
        List<byte> m_CurLine;
    }
}