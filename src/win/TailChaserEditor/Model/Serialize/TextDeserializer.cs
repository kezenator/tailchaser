using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model.Serialize
{
    public class TextDeserializer
    {
        public TextDeserializer(string contents)
        {
            m_NextIndex = 0;
            m_Lines = contents.Split(new char[] { '\n' });

            for (int i = 0; i < m_Lines.Length; ++i)
            {
                m_Lines[i] = m_Lines[i].TrimEnd();
            }
        }

        public void ExpectLine(string expected)
        {
            if (!m_Lines[m_NextIndex].Equals(expected))
            {
                throw new FormatException("Line " + (m_NextIndex + 1)
                    + ": Expected \"" + expected + "\""
                    + " but found \"" + m_Lines[m_NextIndex] + "\"");
            }

            m_NextIndex += 1;
        }

        public string DecodeExpectedLine(string head, string tail)
        {
            string line = m_Lines[m_NextIndex];

            if ((line.Length < (head.Length + tail.Length))
                || !head.Equals(line.Substring(0, head.Length))
                || !tail.Equals(line.Substring(line.Length - tail.Length, tail.Length)))
            {
                string expected = head = "..." + tail;
                throw new FormatException("Line " + (m_NextIndex + 1)
                    + ": Expected \"" + expected + "\""
                    + " but found \"" + m_Lines[m_NextIndex] + "\"");
            }

            m_NextIndex += 1;
            return line.Substring(head.Length, line.Length - head.Length - tail.Length);
        }

        public byte[] ExpectBytesUntil(string end_line)
        {
            List<byte> bytes = new List<byte>();

            while (true)
            {
                string line = PeekLine();

                if (line.Equals(end_line))
                    break;

                bool valid = true;

                int comment_index = line.IndexOf("/*");

                if (comment_index == -1)
                {
                    valid = false;
                }
                else
                {
                    string comment = line.Substring(comment_index);
                    string[] parts = line.Substring(0, comment_index).Split(new char[] { ',' });

                    if ((comment.Length < 4)
                        || !comment.StartsWith("/*")
                        || !comment.EndsWith("*/"))
                    {
                        valid = false;
                    }
                    else
                    {
                        for (int i = 0; i < parts.Length - 1; ++i)
                        {
                            string num_str = parts[i].Trim();

                            if ((num_str.Length != 4)
                                || !num_str.StartsWith("0x"))
                            {
                                valid = false;
                                break;
                            }

                            bytes.Add(byte.Parse(num_str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber));
                        }

                        if (!parts[parts.Length - 1].Trim().Equals(""))
                            valid = false;
                    }
                }

                if (!valid)
                {
                    throw new FormatException("Line " + (m_NextIndex + 1)
                        + ": Expected binary data or \"" + end_line + "\"");
                }

                m_NextIndex += 1;
            }

            return bytes.ToArray();
        }

        public void ExpectEof()
        {
            while ((m_NextIndex < m_Lines.Length)
                && m_Lines[m_NextIndex].Trim().Equals(""))
            {
                m_NextIndex += 1;
            }

            if (m_NextIndex != m_Lines.Length)
            {
                throw new FormatException("Line " + (m_NextIndex + 1)
                    + ": Expected end of file");
            }
        }

        public string PeekLine()
        {
            if (m_NextIndex < m_Lines.Length)
                return m_Lines[m_NextIndex];
            else
                throw new FormatException("Line " + (m_NextIndex + 1)
                    + ": Unexpected end of file");
        }

        private string[] m_Lines;
        private int m_NextIndex;
    }
}
