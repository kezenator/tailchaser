using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.TailChaser.Editor.Model
{
    public delegate void OnBitmapChangedDelegate(Bitmap bitmap);

    public class Bitmap
    {
        public Bitmap(Palette palette)
        {
            m_Palette = palette;
            m_Pixels = new int[WIDTH * HEIGHT];

            int init = palette.TransparentIndex;

            for (int i = 0; i < m_Pixels.Length; ++i)
                m_Pixels[i] = init;
        }

        public event OnBitmapChangedDelegate OnChanged;

        public Palette Palette
        {
            get
            {
                return m_Palette;
            }
        }

        public int Width
        {
            get
            {
                return WIDTH;
            }
        }

        public int Height
        {
            get
            {
                return HEIGHT;
            }
        }

        public int this[int x, int y]
        {
            get
            {
                if ((x < 0) || (x >= WIDTH))
                    throw new ArgumentOutOfRangeException("x");
                if ((y < 0) || (y >= HEIGHT))
                    throw new ArgumentOutOfRangeException("y");

                return m_Pixels[x + y * WIDTH];
            }

            set
            {
                if ((x < 0) || (x >= WIDTH))
                    throw new ArgumentOutOfRangeException("x");
                if ((y < 0) || (y >= HEIGHT))
                    throw new ArgumentOutOfRangeException("y");
                if ((value < 0) || (value >= m_Palette.Length))
                    throw new ArgumentOutOfRangeException("value");

                int index = x + y * WIDTH;

                int old_value = m_Pixels[index];

                m_Pixels[index] = value;

                if ((old_value != value)
                    && (OnChanged != null))
                {
                    OnChanged(this);
                }

            }
        }

        public void CombineWith(Bitmap other)
        {
            Debug.Assert(m_Palette.Equals(other.m_Palette));

            for (int i = 0; i < m_Pixels.Length; ++i)
            {
                if (!other.m_Palette.IsTransparent(other.m_Pixels[i]))
                    m_Pixels[i] = other.m_Pixels[i];
            }
        }

        private const int WIDTH = 32;
        private const int HEIGHT = 16;

        private Palette m_Palette;
        private int[] m_Pixels;
    }
}
