using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Com.TailChaser.Editor.UI.Controls
{
    public delegate void OnPaletteViewPaletteChangedDelegate();

    public partial class PaletteView : UserControl
    {
        public PaletteView()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Opaque, true);
            DoubleBuffered = true;

            m_NeedsLayout = true;
            m_Palette = new Model.Palette();
            m_SelectedIndex = 0;
        }

        public event OnPaletteViewPaletteChangedDelegate OnPaletteChanged;

        public Model.Palette Palette
        {
            get
            {
                return m_Palette;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                m_Palette = value;
                m_NeedsLayout = true;
                m_SelectedIndex = 0;
                Invalidate();

                if (OnPaletteChanged != null)
                    OnPaletteChanged();
            }
        }

        public int SelectedIndex
        {
            get
            {
                return m_SelectedIndex;
            }

            set
            {
                if ((value < 0) || (value >= m_Palette.Length))
                    throw new ArgumentOutOfRangeException();

                m_SelectedIndex = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_NeedsLayout)
            {
                DoLayout();
            }

            // Background

            using (Brush b = new SolidBrush(Color.FromKnownColor(KnownColor.Window)))
            {
                e.Graphics.FillRectangle(b, 0, 0, Width, Height);
            }

            // Draw colors

            using (Pen border_pen = new Pen(Color.FromKnownColor(KnownColor.WindowText), 1.0f))
            {
                SizeF width_height = LogicalToWindowSize(m_DotSize, m_DotSize);

                for (int i = 0; i < m_Positions.Length; ++i)
                {
                    PointF top_left = LogicalToWindowPoint(m_Positions[i].x - m_DotSize / 2.0f, m_Positions[i].y - m_DotSize / 2.0f);
                    RectangleF rect = new RectangleF(top_left, width_height);

                    if (m_Palette.IsTransparent(i))
                    {
                        using (Brush b = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.LightGray, Color.Gray))
                        {
                            e.Graphics.FillEllipse(b, rect);
                        }
                    }
                    else
                    {
                        using (Brush b = new SolidBrush(m_Palette[i]))
                        {
                            e.Graphics.FillEllipse(b, rect);
                        }
                    }

                    if (i == m_SelectedIndex)
                    {
                        using (Pen p = new Pen(Color.FromKnownColor(KnownColor.Highlight), 3.0f))
                        {
                            e.Graphics.DrawEllipse(p, rect);
                        }
                    }
                    else
                    {
                        e.Graphics.DrawEllipse(border_pen, rect);
                    }
                }
            }

            // Border

            using (Pen p = new Pen(Color.FromKnownColor(KnownColor.WindowFrame), 1.0f))
            {
                e.Graphics.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
            }

            // Print selected index

            //using (Brush b = new SolidBrush(Color.FromKnownColor(KnownColor.WindowText)))
            //{
            //    e.Graphics.DrawString(m_SelectedIndex.ToString(), Font, b, MARGIN, MARGIN);
            //}

            // Allow user override

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            PointF point = WindowToLogicalPoint(e.X, e.Y);

            for (int i = 0; i < m_Positions.Length; ++i)
            {
                double dx = point.X - m_Positions[i].x;
                double dy = point.Y - m_Positions[i].y;

                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (distance < (m_DotSize / 2.0f))
                {
                    if (i != m_SelectedIndex)
                    {
                        m_SelectedIndex = i;
                        Invalidate();
                        break;
                    }
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();

            base.OnResize(e);
        }

        private void DoLayout()
        {
            CreatePositions_HSL();
            CalcRange();
            CalcDotSize();
            ExpandRangeForDotSize();

            m_NeedsLayout = false;
        }

        private void CreatePositions_Grid()
        {
            m_Positions = new Position[m_Palette.Length];

            // Two 4x4 groups of 16 placed horizontally,
            // then repeated - each 4 rows is 32 entries
            //
            //  0  1  2  3   16 17 18 19
            //  4  5  6  7   20 21 22 23
            //  8  9 10 11   24 25 26 27
            // 12 13 14 15   28 29 30 31
            //
            // 32 33 34 35   48 49 50 51
            // ....

            for (int i = 0; i < m_Positions.Length; ++i)
            {
                m_Positions[i].x = (i % 4) + (4 * (i % 32 / 16));
                m_Positions[i].y = (4 * (i / 32)) + (i % 16 / 4);
            }
        }

        private void CreatePositions_HSL()
        {
            m_Positions = new Position[m_Palette.Length];

            // Black to white along the top,
            // Colors in a circle, angle is hue,
            // radius is lightness

            for (int i = 0; i < m_Positions.Length; ++i)
            {
                Color color = m_Palette[i];

                float hue = color.GetHue();
                float sat = color.GetSaturation();
                float lightness = color.GetBrightness();

                if ((lightness < 0.1)
                    || (lightness > 0.9)
                    || (sat < 0.1))
                {
                    // Black/white/grey

                    m_Positions[i].x = (2.0f * lightness) - 1.0f;
                    m_Positions[i].y = -1.0f;
                }
                else
                {
                    // Colors

                    // There are conflicting colors
                    // for instance RGB(255, 0, 0) and RGB(170, 85, 85)
                    // which are HSL(0, 1, 1/2) and HSL(0, 1/3, 1/2)
                    // i.e. same hue, same lightness, but one is
                    // desaturated.
                    //
                    // Put these along the bottom

                    if ((sat > 0.332) && (sat < 0.334)
                        && (lightness > 0.499) && (lightness < 0.501))
                    {
                        m_Positions[i].x = -1.0f + (2.0f * (hue + 30.0f) / 360.0f);
                        m_Positions[i].y = 1.0f;
                    }
                    else
                    {
                        float angle = hue;
                        float radius = lightness;

                        m_Positions[i].x = radius * (float)Math.Cos(angle / 180.0f * Math.PI);
                        m_Positions[i].y = radius * (float)Math.Sin(angle / 180.0f * Math.PI);
                    }
                }
            }
        }

        private void CalcRange()
        {
            float left = float.MaxValue;
            float top = float.MaxValue;
            float right = float.MinValue;
            float bottom = float.MinValue;

            for (int i = 0; i < m_Positions.Length; ++i)
            {
                if (m_Positions[i].x < left)
                    left = m_Positions[i].x;
                if (m_Positions[i].x > right)
                    right = m_Positions[i].x;

                if (m_Positions[i].y < top)
                    top = m_Positions[i].y;
                if (m_Positions[i].y > bottom)
                    bottom = m_Positions[i].y;
            }

            m_Range = new RectangleF(left, top, right - left, bottom - top);
        }

        private void CalcDotSize()
        {
            // The dot size is 85% of the
            // smallest distance between positions

            float min_distance = float.MaxValue;

            for (int i = 0; i < m_Positions.Length; ++i)
            {
                for (int j = 0; j < m_Positions.Length; ++j)
                {
                    if (i != j)
                    {
                        float xd = m_Positions[i].x - m_Positions[j].x;
                        float yd = m_Positions[i].y - m_Positions[j].y;
                        float distance = (float)Math.Sqrt(xd * xd + yd * yd);

                        if (distance < min_distance)
                        {
                            min_distance = distance;

#if DEBUG
                            if (distance == 0)
                                System.Diagnostics.Debugger.Break();
#endif
                        }
                    }
                }
            }

            m_DotSize = 0.85f * min_distance;
        }

        private void ExpandRangeForDotSize()
        {
            m_Range.X -= m_DotSize / 2.0f;
            m_Range.Y -= m_DotSize / 2.0f;
            m_Range.Width += m_DotSize;
            m_Range.Height += m_DotSize;
        }

        private PointF LogicalToWindowPoint(float x, float y)
        {
            return new PointF(
                MARGIN + ((x - m_Range.X) / m_Range.Width * (Width - 2 * MARGIN)),
                MARGIN + ((y - m_Range.Y) / m_Range.Height * (Height - 2 * MARGIN)));
        }

        private SizeF LogicalToWindowSize(float width, float height)
        {
            return new SizeF(
                width / m_Range.Width * (Width - 2 * MARGIN),
                height / m_Range.Height * (Height - 2 * MARGIN));
        }

        private PointF WindowToLogicalPoint(float x, float y)
        {
            return new PointF(
                m_Range.X + ((x - MARGIN) * m_Range.Width / (Width - 2 * MARGIN)),
                m_Range.Y + ((y - MARGIN) * m_Range.Height / (Height - 2 * MARGIN)));
        }

        private struct Position
        {
            public float x;
            public float y;

            public override string ToString()
            {
                return string.Format("[{0},{1}]", x, y);
            }
        }

        const float MARGIN = 5.0f;

        private bool m_NeedsLayout;
        private Model.Palette m_Palette;
        private int m_SelectedIndex;
        private Position[] m_Positions;
        private RectangleF m_Range;
        private float m_DotSize;
    }
}
