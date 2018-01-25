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
    public partial class BitmapView : UserControl
    {
        public BitmapView()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Opaque, true);
            DoubleBuffered = true;

            m_PaletteView = null;
            m_Bitmap = null;
            m_UndoRedoBuffer = null;
        }

        public PaletteView PaletteView
        {
            get
            {
                return m_PaletteView;
            }

            set
            {
                if (m_PaletteView != null)
                    m_PaletteView.OnPaletteChanged -= OnPaletteChanged;

                m_PaletteView = value;

                m_Bitmap = null;

                if (m_PaletteView != null)
                {
                    m_PaletteView.OnPaletteChanged += OnPaletteChanged;
                    m_Bitmap = new Model.Bitmap(m_PaletteView.Palette);
                }
            }
        }

        public UndoRedoBuffer UndoRedoBuffer
        {
            get
            {
                return m_UndoRedoBuffer;
            }

            set
            {
                m_UndoRedoBuffer = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw transparent

            using (Brush b = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.LightGray, Color.Gray))
                e.Graphics.FillRectangle(b, 0, 0, Width, Height);

            // Draw bitmap's non-transparent pixels (if any...)

            if (m_Bitmap != null)
            {
                for (int x = 0; x < m_Bitmap.Width; ++x)
                {
                    for (int y = 0; y < m_Bitmap.Height; ++y)
                    {
                        int palette_index = m_Bitmap[x, y];

                        if (!m_Bitmap.Palette.IsTransparent(palette_index))
                        {
                            Rectangle rect = new Rectangle(
                                x * Width / m_Bitmap.Width,
                                y * Height / m_Bitmap.Height,
                                Width / m_Bitmap.Width + 1,
                                Height / m_Bitmap.Height + 1);

                            using (Brush b = new SolidBrush(m_Bitmap.Palette[palette_index]))
                                e.Graphics.FillRectangle(b, rect);
                        }
                    }
                }
            }

            // Grids and Border

            using (Pen p = new Pen(Color.FromKnownColor(KnownColor.WindowFrame), 1.0f))
            {
                if (m_Bitmap != null)
                {
                    for (int x = 0; x < m_Bitmap.Width; ++x)
                    {
                        int px = x * Width / m_Bitmap.Width;
                        e.Graphics.DrawLine(p, px, 0, px, Height);
                    }

                    for (int y = 0; y < m_Bitmap.Height; ++y)
                    {
                        int py = y * Height / m_Bitmap.Height;
                        e.Graphics.DrawLine(p, 0, py, Width, py);
                    }
                }

                e.Graphics.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
            }

            // Allow user override

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();

            base.OnResize(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            DoMouseClick(e.X, e.Y);

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoMouseClick(e.X, e.Y);
            }

            base.OnMouseMove(e);
        }

        private void OnPaletteChanged()
        {
            m_Bitmap = null;

            if (m_PaletteView != null)
            {
                m_Bitmap = new Model.Bitmap(m_PaletteView.Palette);
            }

            Invalidate();
        }

        private void DoMouseClick(int x, int y)
        {
            if (m_Bitmap != null)
            {
                int bx = x * m_Bitmap.Width / Width;
                int by = y * m_Bitmap.Height / Height;

                if ((bx >= 0) && (bx < m_Bitmap.Width)
                    && (by >= 0) && (by < m_Bitmap.Height))
                {
                    int old_index = m_Bitmap[bx, by];
                    int new_index = m_PaletteView.SelectedIndex;

                    if (new_index != old_index)
                    {
                        Operation op = new Operation(m_UndoRedoBuffer, this, m_Bitmap, bx, by, old_index, new_index);
                        op.PerformAndPushUndo();
                    }
                }
            }
        }

        private class Operation : UndoRedoBuffer.Entry
        {
            public Operation(UndoRedoBuffer parent, BitmapView view, Model.Bitmap bitmap, int x, int y, int from, int to)
                : base(parent)
            {
                m_View = view;
                m_Bitmap = bitmap;
                m_X = x;
                m_Y = y;
                m_From = from;
                m_To = to;
            }

            public override void Undo()
            {
                m_Bitmap[m_X, m_Y] = m_From;
                m_View.Invalidate();
            }

            public override void Redo()
            {
                m_Bitmap[m_X, m_Y] = m_To;
                m_View.Invalidate();
            }

            private BitmapView m_View;
            private Model.Bitmap m_Bitmap;
            private int m_X;
            private int m_Y;
            private int m_From;
            private int m_To;
        }

        private PaletteView m_PaletteView;
        private Model.Bitmap m_Bitmap;
        private UndoRedoBuffer m_UndoRedoBuffer;
    }
}
