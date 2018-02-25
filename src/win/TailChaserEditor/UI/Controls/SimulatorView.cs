using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Com.TailChaser.Editor.UI.Controls
{
    public partial class SimulatorView : UserControl
    {
        public SimulatorView()
        {
            InitializeComponent();

            m_Scheme = null;

            m_LayerStates = new List<LayerState>();
            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();

            m_CurrentState.SetReset(Model.SignalMask.Tail);
            m_CurrentState.SetReset(Model.SignalMask.Brake);
            m_CurrentState.SetReset(Model.SignalMask.Reverse);
            m_CurrentState.SetReset(Model.SignalMask.IndicatorSolid);
            m_CurrentState.SetReset(Model.SignalMask.IndicatorFlash);

            m_TailLabel.Text = "";
            m_TailLabel.BackColor = Color.Black;

            m_BrakeLabel.Text = "";
            m_BrakeLabel.BackColor = Color.Black;

            m_ReverseLabel.Text = "";
            m_ReverseLabel.BackColor = Color.Black;

            m_IndicatorLabel.Text = "";
            m_IndicatorLabel.BackColor = Color.Black;

            m_RedrawTimer.Start();
        }

        public Model.Scheme Scheme
        {
            get
            {
                return m_Scheme;
            }

            set
            {
                m_Scheme = value;

                m_BitmapView.Invalidate();
            }
        }

        public void RequestRecalculate()
        {
            m_BitmapView.Invalidate();
        }

        public void ToggleSignal(Model.SignalMask signal)
        {
            SetSignal(signal, !m_CurrentState.IsSet(signal));
        }

        public void SetSignal(Model.SignalMask signal, bool state)
        {
            switch (signal)
            {
                case Model.SignalMask.Tail:
                    UpdateState(signal, state, m_TailLabel, m_TailButton, Color.DarkRed);
                    break;
                case Model.SignalMask.Brake:
                    UpdateState(signal, state, m_BrakeLabel, m_BrakeButton, Color.Red);
                    break;
                case Model.SignalMask.Reverse:
                    UpdateState(signal, state, m_ReverseLabel, m_ReverseButton, Color.White);
                    break;
                case Model.SignalMask.IndicatorSolid:
                    UpdateState(signal, state, m_IndicatorLabel, m_IndicatorButton, Color.Orange);
                    break;
                case Model.SignalMask.IndicatorFlash:
                    throw new NotSupportedException("Can't set Model.SignalMask.IndicatorFlash - set IndicatorSolid, and the flash is automatically generated");
            }
        }

        private void UpdateState(Model.SignalMask signal, bool new_state, Label label, Button button, Color color)
        {
            bool cur_state = m_CurrentState.IsSet(signal);

            if (cur_state != new_state)
            {
                if (new_state)
                {
                    m_CurrentState.SetSet(signal);
                    label.BackColor = color;

                    if (signal == Model.SignalMask.IndicatorSolid)
                    {
                        m_CurrentState.SetSet(Model.SignalMask.IndicatorFlash);
                        m_IndicatorTimer.Start();
                    }
                }
                else
                {
                    m_CurrentState.SetReset(signal);
                    label.BackColor = Color.Black;

                    if (signal == Model.SignalMask.IndicatorSolid)
                    {
                        m_CurrentState.SetReset(Model.SignalMask.IndicatorFlash);
                        m_IndicatorTimer.Stop();
                    }
                }

                m_BitmapView.Invalidate();
            }
        }

        private void m_TailButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.Tail);
        }

        private void m_BrakeButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.Brake);
        }

        private void m_ReverseButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.Reverse);
        }

        private void m_IndicatorButton_Click(object sender, EventArgs e)
        {
            ToggleSignal(Model.SignalMask.IndicatorSolid);
        }

        private void m_IndicatorTimer_Tick(object sender, EventArgs e)
        {
            if (m_CurrentState.IsSet(Model.SignalMask.IndicatorSolid))
            {
                if (m_CurrentState.IsSet(Model.SignalMask.IndicatorFlash))
                {
                    m_CurrentState.SetReset(Model.SignalMask.IndicatorFlash);
                    m_IndicatorLabel.BackColor = Color.Black;
                }
                else
                {
                    m_CurrentState.SetSet(Model.SignalMask.IndicatorFlash);
                    m_IndicatorLabel.BackColor = Color.Orange;
                }

                m_BitmapView.Invalidate();
            }
        }

        private void m_BitmapView_BeforePaint(object sender, PaintEventArgs e)
        {
            if (m_Scheme != null)
            {
                UInt64 cur_time_ms = (UInt64)m_Stopwatch.ElapsedMilliseconds;

                Model.Bitmap result = new Model.Bitmap(m_Scheme.Palette);

                int index = 0;

                foreach (Model.Layer layer in m_Scheme.Layers)
                {
                    if (m_LayerStates.Count <= index)
                        m_LayerStates.Add(new LayerState());

                    m_LayerStates[index].Update(
                        layer,
                        layer.SignalSet.ConditionIsMatchedByState(m_CurrentState),
                        cur_time_ms,
                        result);

                    index += 1;
                }

                m_BitmapView.Bitmap = result;
            }
        }

        private void m_RedrawTimer_Tick(object sender, EventArgs e)
        {
            m_BitmapView.Invalidate();
        }

        private class LayerState
        {
            public LayerState()
            {
                m_Layer = null;
                m_Pattern = Model.Pattern.Solid;
                m_Field1 = 0;
                m_Field2 = 0;
                m_Field3 = 0;
                m_Required = false;
                m_StartTime = 0;
                m_Pixels = 0;
            }

            public void Update(Model.Layer layer, bool required, UInt64 cur_time_ms, Model.Bitmap result)
            {
                // First, detect if the layer setup has changed

                bool changed = false;

                if (!object.ReferenceEquals(m_Layer, layer))
                {
                    changed = true;
                }
                else if ((m_Pattern != layer.Pattern)
                    || (m_Field1 != layer.Field1)
                    || (m_Field2 != layer.Field2)
                    || (m_Field3 != layer.Field3))
                {
                    changed = true;
                }

                if (changed)
                {
                    m_Layer = layer;
                    m_Pattern = layer.Pattern;
                    m_Field1 = layer.Field1;
                    m_Field2 = layer.Field2;
                    m_Field3 = layer.Field3;
                    m_Required = false;
                }

                // Now, reset the display if the required
                // setting has changed

                if (required != m_Required)
                {
                    m_Required = required;
                    m_StartTime = cur_time_ms;
                    m_Pixels = 0;
                }

                // Get the bitmap source we're drawing from

                Model.Bitmap source = m_Layer.Bitmap;
                int source_width = source.Width;
                int source_height = source.Height;

                // Update the progress based on the pattern

                switch (m_Pattern)
                {
                    case Model.Pattern.Solid:
                        // Always full on
                        m_Pixels = source_width;
                        break;

                    case Model.Pattern.Flashing:
                        // Field1 = on time (ms)
                        // Field2 = off time (ms)
                        if ((m_Field1 == 0) && (m_Field2 == 0))
                            m_Pixels = source_width;
                        else if (((cur_time_ms - m_StartTime) % (UInt64)(m_Field1 + m_Field2)) <= m_Field1)
                            m_Pixels = source_width;
                        else
                            m_Pixels = 0;
                        break;

                    case Model.Pattern.SwipeLeft:
                    case Model.Pattern.SwipeRight:
                    case Model.Pattern.SwipeUp:
                    case Model.Pattern.SwipeDown:
                        // Field1 = swipe time (ms)
                        // Field2 = on time (ms)
                        // Field3 = off time (ms)
                        {
                            int dimension = source_width;
                            if ((m_Pattern == Model.Pattern.SwipeUp)
                                || (m_Pattern == Model.Pattern.SwipeDown))
                            {
                                dimension = source_height;
                            }

                            if ((m_Field1 == 0) && (m_Field2 == 0) && (m_Field3 == 0))
                                m_Pixels = dimension;
                            else
                            {
                                UInt64 progress = (cur_time_ms - m_StartTime) % (UInt64)(m_Field1 + m_Field2 + m_Field3);

                                if (progress < m_Field1)
                                    m_Pixels = ((int)progress * dimension / m_Field1);
                                else if (progress <= (UInt64)(m_Field1 + m_Field2))
                                    m_Pixels = dimension;
                                else
                                    m_Pixels = 0;
                            }
                        }
                        break;

                    case Model.Pattern.ScrollLeft:
                    case Model.Pattern.ScrollRight:
                    case Model.Pattern.ScrollUp:
                    case Model.Pattern.ScrollDown:
                        // Field1 = swipe time (ms)
                        // Field2 = size (pixels)
                        // Field3 = N/A
                        {
                            int dimension = source_width;

                            if ((m_Pattern == Model.Pattern.SwipeUp)
                                || (m_Pattern == Model.Pattern.SwipeDown))
                            {
                                dimension = source_height;
                            }

                            if (m_Field1 == 0)
                                m_Pixels = 0;
                            else
                            {
                                UInt64 progress = (cur_time_ms - m_StartTime) % (UInt64)m_Field1;

                                m_Pixels = (int)progress * (int)m_Field2 / (int)m_Field1;
                            }
                        }
                        break;
                }

                // Now display (according to the pattern)
                // based on the progress

                if (m_Required)
                {
                    switch (m_Pattern)
                    {
                        case Model.Pattern.Solid:
                        case Model.Pattern.Flashing:
                        case Model.Pattern.SwipeLeft:
                            // Draw from left across - pixels code
                            // above handles solid and flashing as
                            // all/none

                            if (m_Pixels == source_width)
                                result.CombineWith(source);
                            else if (m_Pixels > 0)
                                result.CombineWith(source_width - m_Pixels, 0, m_Pixels, source_height, source, source_width - m_Pixels, 0);
                            break;

                        case Model.Pattern.SwipeRight:
                            if (m_Pixels == source_width)
                                result.CombineWith(source);
                            else if (m_Pixels > 0)
                                result.CombineWith(0, 0, m_Pixels, source_height, source, 0, 0);
                            break;

                        case Model.Pattern.SwipeUp:
                            if (m_Pixels == source_height)
                                result.CombineWith(source);
                            else if (m_Pixels > 0)
                                result.CombineWith(0, source_height - m_Pixels, source_width, m_Pixels, source, 0, source_height - m_Pixels);
                            break;

                        case Model.Pattern.SwipeDown:
                            if (m_Pixels == source_height)
                                result.CombineWith(source);
                            else if (m_Pixels > 0)
                                result.CombineWith(0, 0, source_width, m_Pixels, source, 0, 0);
                            break;

                        case Model.Pattern.ScrollLeft:
                            if (m_Field2 != 0)
                            {
                                for (int x = 0; x < source_width; ++x)
                                {
                                    int source_x = ((x + m_Pixels) % m_Field2) % source_width;

                                    result.CombineWith(x, 0, 1, source_height, source, source_x, 0);
                                }
                            }
                            break;

                        case Model.Pattern.ScrollRight:
                            if (m_Field2 != 0)
                            {
                                for (int x = 0; x < source_width; ++x)
                                {
                                    int source_x = ((x + m_Field2 - m_Pixels) % m_Field2) % source_width;

                                    result.CombineWith(x, 0, 1, source_height, source, source_x, 0);
                                }
                            }
                            break;

                        case Model.Pattern.ScrollUp:
                            if (m_Field2 != 0)
                            {
                                for (int y = 0; y < source_height; ++y)
                                {
                                    int source_y = ((y + m_Pixels) % m_Field2) % source_height;

                                    result.CombineWith(0, y, source_width, 1, source, 0, source_y);
                                }
                            }
                            break;

                        case Model.Pattern.ScrollDown:
                            if (m_Field2 != 0)
                            {
                                for (int y = 0; y < source_height; ++y)
                                {
                                    int source_y = ((y + m_Field2 - m_Pixels) % m_Field2) % source_height;

                                    result.CombineWith(0, y, source_width, 1, source, 0, source_y);
                                }
                            }
                            break;
                    }
                }
            }

            private Model.Layer m_Layer;
            private Model.Pattern m_Pattern;
            private UInt16 m_Field1;
            private UInt16 m_Field2;
            private UInt16 m_Field3;

            private bool m_Required;
            private UInt64 m_StartTime;
            private int m_Pixels;
        }

        private Model.Scheme m_Scheme;
        private Model.SignalSet m_CurrentState;
        private List<LayerState> m_LayerStates;
        private Stopwatch m_Stopwatch;
    }
}
