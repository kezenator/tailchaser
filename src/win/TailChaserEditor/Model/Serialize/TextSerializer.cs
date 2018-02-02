using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model.Serialize
{
    public class TextSerializer
    {
        public TextSerializer()
        {
            m_Lines = new List<string>();
        }

        public void AddMultilineComment(IEnumerable<string> comment_lines)
        {
            m_Lines.Add("/*");
            foreach (string line in comment_lines)
                m_Lines.Add(" * " + line);
            m_Lines.Add(" */");
        }

        public void AddLine(string line)
        {
            m_Lines.Add(line);
        }

        public void AddBinary(List<KeyValuePair<byte[], string>> binary_pairs)
        {
            int max_binary_length = 0;
            foreach (KeyValuePair<byte[], string> entry in binary_pairs)
            {
                if (entry.Key.Length > max_binary_length)
                    max_binary_length = entry.Key.Length;
            }

            foreach (KeyValuePair<byte[], string> entry in binary_pairs)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("   ");
                foreach (byte b in entry.Key)
                {
                    sb.AppendFormat(" 0x{0:X2},", b);
                }

                if (entry.Key.Length < max_binary_length)
                {
                    sb.Append(new string(' ', 6 * (max_binary_length - entry.Key.Length)));
                }

                sb.Append(" /* ");
                sb.Append(entry.Value);
                sb.Append(" */");

                m_Lines.Add(sb.ToString());
            }
        }

        public override string ToString()
        {
            return string.Join("\r\n", m_Lines) + "\r\n";
        }

        public List<string> m_Lines;
    }
}
