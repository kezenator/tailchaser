using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Com.TailChaser.Editor.UI.Controls
{
    public partial class SimulatorView : UserControl
    {
        public SimulatorView()
        {
            InitializeComponent();

            m_Scheme = null;

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

                m_RedrawTimer.Start();
            }
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

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                m_RedrawTimer.Start();
            }

            base.OnVisibleChanged(e);
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

                m_RedrawTimer.Start();
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

                m_RedrawTimer.Start();
            }
        }

        private void m_RedrawTimer_Tick(object sender, EventArgs e)
        {
            m_RedrawTimer.Stop();

            if (m_Scheme != null)
            {
                Model.Bitmap result = new Model.Bitmap(m_Scheme.Palette);

                foreach (Model.Layer layer in m_Scheme.Layers)
                {
                    if (layer.SignalSet.ConditionIsMatchedByState(m_CurrentState))
                    {
                        result.CombineWith(layer.Bitmap);
                    }
                }

                m_BitmapView.Bitmap = result;
            }
        }

        private Model.Scheme m_Scheme;
        private Model.SignalSet m_CurrentState;
    }
}
