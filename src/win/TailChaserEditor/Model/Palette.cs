using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model
{
    public class Palette
    {
        public Palette()
        {
            m_Name = "6-bit Palette";
            m_Colors = new Color[64];
            m_TransparentIndex = 0;

            for (int i = 0; i < 64; ++i)
            {
                int red = i % 4;
                int green = (i / 4) % 4;
                int blue = (i / 16) % 4;

                m_Colors[i] = Color.FromArgb(red * 255 / 3, green * 255 / 3, blue * 255 / 3);
            }
        }

        public override bool Equals(object obj)
        {
            Palette p = obj as Palette;
            if (p == null)
                return false;

            return m_Name.Equals(p.m_Name);
        }

        public override int GetHashCode()
        {
            return m_Name.GetHashCode();
        }

        public override string ToString()
        {
            return m_Name;
        }

        public int Length
        {
            get
            {
                return m_Colors.Length;
            }
        }

        public Color this[int index]
        {
            get
            {
                return m_Colors[index];
            }
        }

        public int TransparentIndex
        {
            get
            {
                return m_TransparentIndex;
            }
        }

        public bool IsTransparent(Color color)
        {
            return m_Colors[m_TransparentIndex].Equals(color);
        }

        public bool IsTransparent(int index)
        {
            return index == m_TransparentIndex;
        }

        private string m_Name;
        private Color [] m_Colors;
        private int m_TransparentIndex;
    }
}
