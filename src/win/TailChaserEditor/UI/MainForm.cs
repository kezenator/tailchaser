using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            m_DeviceView.Scheme = m_Scheme;

            m_HasFilePath = false;
            m_FileName = "<UNTITLED>";
            UpdateTitleBar();
        }

        private void m_FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void m_FileUploadMenuItem_Click(object sender, EventArgs e)
        {
            m_MainTabs.SelectedIndex = 2;
            m_DeviceView.Upload();
        }

        private void m_EditUndoMenuItem_Click(object sender, EventArgs e)
        {
            m_UndoRedoBuffer.Undo();
        }

        private void m_EditRedoMenuItem_Click(object sender, EventArgs e)
        {
            m_UndoRedoBuffer.Redo();
        }

        private void m_UndoRedoBuffer_OnDocumentChanged(Controls.UndoRedoBuffer source)
        {
            m_SimulatorView.RequestRecalculate();
        }

        private void m_UndoRedoBuffer_OnUndoAvailableChanged(Controls.UndoRedoBuffer source)
        {
            m_EditUndoMenuItem.Enabled = m_UndoRedoBuffer.UndoAvailable;
            UpdateTitleBar();
        }

        private void m_UndoRedoBuffer_OnRedoAvailableChanged(Controls.UndoRedoBuffer source)
        {
            m_EditRedoMenuItem.Enabled = m_UndoRedoBuffer.RedoAvailable;
        }

        private void m_ViewEditMenuItem_Click(object sender, EventArgs e)
        {
            m_MainTabs.SelectedIndex = 0;
        }

        private void m_ViewSimulateMenuItem_Click(object sender, EventArgs e)
        {
            m_MainTabs.SelectedIndex = 1;
        }

        private void m_ViewDeviceMenuItem_Click(object sender, EventArgs e)
        {
            m_MainTabs.SelectedIndex = 2;
        }

        private void m_SimulateTailMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(Model.SignalMask.Tail);
        }

        private void m_SimulateBrakeMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(Model.SignalMask.Brake);
        }

        private void m_SimulateReverseMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(Model.SignalMask.Reverse);
        }

        private void m_SimulateIndicatorMenuItem_Click(object sender, EventArgs e)
        {
            Toggle(Model.SignalMask.IndicatorSolid);
        }

        private void Toggle(Model.SignalMask signal_mask)
        {
            m_SimulatorView.ToggleSignal(signal_mask);
            m_DeviceView.ToggleSignal(signal_mask);
        }

        private void m_FileNewMenuItem_Click(object sender, EventArgs e)
        {
            if (m_UndoRedoBuffer.UndoAvailable)
            {
                switch (MessageBox.Show(
                    "Want to save changes to your document?",
                    "Confirm",
                    MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (m_HasFilePath)
                            m_FileSaveMenuItem_Click(sender, e);
                        else
                            m_FileSaveAsMenuItem_Click(sender, e);
                        break;

                    case DialogResult.No:
                        // Continue
                        break;

                    case DialogResult.Cancel:
                        return;
                }
            }

            Model.Scheme new_scheme = new Model.Scheme(m_PaletteView.Palette);

            m_HasFilePath = false;
            m_FilePath = "<UNTITLED>";

            m_Scheme = new_scheme;
            m_SchemeView.Scheme = new_scheme;
            m_SimulatorView.Scheme = new_scheme;
            m_DeviceView.Scheme = new_scheme;

            m_UndoRedoBuffer.Clear();
            UpdateTitleBar();
        }

        private void m_FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            if (m_OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string contents = File.ReadAllText(m_OpenFileDialog.FileName);

                    Model.Scheme new_scheme = Model.Serialize.CCodeFileFormat.Deserialize(contents, m_PaletteView.Palette);

                    m_HasFilePath = true;
                    m_FileName = Path.GetFileNameWithoutExtension(m_OpenFileDialog.FileName);
                    m_FilePath = m_OpenFileDialog.FileName;

                    m_Scheme = new_scheme;
                    m_SchemeView.Scheme = new_scheme;
                    m_SimulatorView.Scheme = new_scheme;
                    m_DeviceView.Scheme = new_scheme;

                    m_UndoRedoBuffer.Clear();
                    UpdateTitleBar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void m_FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string contents = Model.Serialize.CCodeFileFormat.Serialize(m_Scheme);

                File.WriteAllText(m_FilePath, contents);

                m_UndoRedoBuffer.Clear();
                UpdateTitleBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (m_SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string contents = Model.Serialize.CCodeFileFormat.Serialize(m_Scheme);

                    File.WriteAllText(m_SaveFileDialog.FileName, contents);

                    m_HasFilePath = true;
                    m_FileName = Path.GetFileNameWithoutExtension(m_SaveFileDialog.FileName);
                    m_FilePath = m_SaveFileDialog.FileName;

                    m_UndoRedoBuffer.Clear();
                    UpdateTitleBar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateTitleBar()
        {
            Text = "TailChaser Editor - "
                + m_FileName
                + (m_UndoRedoBuffer.UndoAvailable ? " (modified)" : "");

            m_FileSaveMenuItem.Enabled = m_HasFilePath;
        }

        private Model.Scheme m_Scheme;

        private bool m_HasFilePath;
        private string m_FileName;
        private string m_FilePath;
    }
}
