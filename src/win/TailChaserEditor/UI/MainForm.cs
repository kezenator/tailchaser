﻿using System;
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
    }
}
