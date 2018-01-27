using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Com.TailChaser.Editor.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            m_EditUndoMenuItem.Enabled = m_UndoRedoBuffer.UndoAvailable;
            m_EditRedoMenuItem.Enabled = m_UndoRedoBuffer.RedoAvailable;

            m_Scheme = new Model.Scheme(m_PaletteView.Palette);
            m_SchemeView.Scheme = m_Scheme;
            m_SimulatorView.Scheme = m_Scheme;
        }

        private void m_FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void m_EditUndoMenuItem_Click(object sender, EventArgs e)
        {
            m_UndoRedoBuffer.Undo();
        }

        private void m_EditRedoMenuItem_Click(object sender, EventArgs e)
        {
            m_UndoRedoBuffer.Redo();
        }

        private void m_UndoRedoBuffer_OnUndoAvailableChanged(Controls.UndoRedoBuffer source)
        {
            m_EditUndoMenuItem.Enabled = m_UndoRedoBuffer.UndoAvailable;
        }

        private void m_UndoRedoBuffer_OnRedoAvailableChanged(Controls.UndoRedoBuffer source)
        {
            m_EditRedoMenuItem.Enabled = m_UndoRedoBuffer.RedoAvailable;
        }

        private Model.Scheme m_Scheme;

        private void m_ViewEditMenuItem_Click(object sender, EventArgs e)
        {
            m_MainTabs.SelectedIndex = 0;
        }

        private void m_ViewSimulateMenuItem_Click(object sender, EventArgs e)
        {
            m_MainTabs.SelectedIndex = 1;
        }

        private void m_SimulateTailMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSimulate(m_SimulateTailMenuItem, Model.SignalMask.Tail);
        }

        private void m_SimulateBrakeMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSimulate(m_SimulateBrakeMenuItem, Model.SignalMask.Brake);
        }

        private void m_SimulateReverseMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSimulate(m_SimulateReverseMenuItem, Model.SignalMask.Reverse);
        }

        private void m_SimulateIndicatorMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSimulate(m_SimulateIndicatorMenuItem, Model.SignalMask.IndicatorSolid);
        }

        private void ToggleSimulate(ToolStripMenuItem menu_item, Model.SignalMask signal_mask)
        {
            m_SimulatorView.ToggleSignal(signal_mask);
        }
    }
}
