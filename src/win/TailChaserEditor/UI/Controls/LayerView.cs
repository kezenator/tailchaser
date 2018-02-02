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
    public partial class LayerView : UserControl
    {
        public LayerView()
        {
            InitializeComponent();

            m_BitmapView = null;
            m_UndoRedoBuffer = null;
            m_Layer = null;
            UpdateDisplay();
        }

        public BitmapView BitmapView
        {
            get
            {
                return m_BitmapView;
            }

            set
            {
                if (m_BitmapView != value)
                {
                    // Remove bitmap from old view

                    if (m_BitmapView != null)
                    {
                        m_BitmapView.Bitmap = null;
                    }

                    // Save value

                    m_BitmapView = value;

                    // Intall new bitmap

                    if (m_BitmapView != null)
                    {
                        if (m_Layer != null)
                        {
                            m_BitmapView.Bitmap = m_Layer.Bitmap;
                        }
                        else
                        {
                            m_BitmapView.Bitmap = null;
                        }
                    }
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

        public Model.Layer Layer
        {
            get
            {
                return m_Layer;
            }

            set
            {
                if (m_Layer != value)
                {
                    // Remove old change notification

                    if (m_Layer != null)
                    {
                        m_Layer.OnChange -= OnLayerChange;
                    }

                    // Save new value

                    m_Layer = value;

                    // Add new change notification and
                    // update the display to the correct value

                    if (m_Layer != null)
                    {
                        m_Layer.OnChange += OnLayerChange;
                    }

                    UpdateDisplay();
                }
            }
        }

        private void OnLayerChange(Model.Layer l)
        {
            if (l == m_Layer)
            {
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            if (m_Layer == null)
            {
                m_NameTextBox.Text = "";
                m_NameTextBox.Enabled = false;

                m_TailCheckbox.CheckState = CheckState.Indeterminate;
                m_TailCheckbox.Enabled = false;

                m_BrakeCheckbox.CheckState = CheckState.Indeterminate;
                m_BrakeCheckbox.Enabled = false;

                m_ReverseCheckbox.CheckState = CheckState.Indeterminate;
                m_ReverseCheckbox.Enabled = false;

                m_IndicatorFlashCheckbox.CheckState = CheckState.Indeterminate;
                m_IndicatorFlashCheckbox.Enabled = false;

                m_IndicatorSolidCheckbox.CheckState = CheckState.Indeterminate;
                m_IndicatorSolidCheckbox.Enabled = false;

                if (m_BitmapView != null)
                {
                    m_BitmapView.Bitmap = null;
                }
            }
            else
            {
                m_NameTextBox.Text = m_Layer.Name;
                m_NameTextBox.Enabled = true;

                m_TailCheckbox.CheckState = LayerMaskToCheckState(Model.SignalMask.Tail);
                m_TailCheckbox.Enabled = true;

                m_BrakeCheckbox.CheckState = LayerMaskToCheckState(Model.SignalMask.Brake);
                m_BrakeCheckbox.Enabled = true;

                m_ReverseCheckbox.CheckState = LayerMaskToCheckState(Model.SignalMask.Reverse);
                m_ReverseCheckbox.Enabled = true;

                m_IndicatorFlashCheckbox.CheckState = LayerMaskToCheckState(Model.SignalMask.IndicatorFlash);
                m_IndicatorFlashCheckbox.Enabled = true;

                m_IndicatorSolidCheckbox.CheckState = LayerMaskToCheckState(Model.SignalMask.IndicatorSolid);
                m_IndicatorSolidCheckbox.Enabled = true;

                if (m_BitmapView != null)
                {
                    m_BitmapView.Bitmap = m_Layer.Bitmap;
                }
            }
        }

        private CheckState LayerMaskToCheckState(Model.SignalMask mask)
        {
            if (m_Layer.SignalSet.IsSet(mask))
                return CheckState.Checked;
            else if (m_Layer.SignalSet.IsReset(mask))
                return CheckState.Unchecked;
            else
                return CheckState.Indeterminate;
        }

        public void UpdateLayerMask(Model.SignalMask mask, CheckState check_state)
        {
            if (m_Layer != null)
            {
                Model.SignalSet old_value = m_Layer.SignalSet;
                Model.SignalSet new_value = m_Layer.SignalSet;

                switch (check_state)
                {
                case CheckState.Checked:
                        new_value.SetSet(mask);
                        break;
                case CheckState.Unchecked:
                    new_value.SetReset(mask);
                    break;
                case CheckState.Indeterminate:
                    new_value.SetExcluded(mask);
                    break;
                }

                if (!new_value.Equals(old_value))
                {
                    UndoRedoBuffer.Entry entry = new SignalSetUndoEntry(m_UndoRedoBuffer, m_Layer, old_value, new_value);
                    entry.PerformAndPushUndo();
                }
            }
        }

        private void m_NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (m_Layer != null)
            {
                m_Layer.Name = m_NameTextBox.Text;
            }
        }

        private void m_TailCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateLayerMask(Model.SignalMask.Tail, m_TailCheckbox.CheckState);
        }

        private void m_BrakeCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateLayerMask(Model.SignalMask.Brake, m_BrakeCheckbox.CheckState);
        }

        private void m_ReverseCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateLayerMask(Model.SignalMask.Reverse, m_ReverseCheckbox.CheckState);
        }

        private void m_IndicatorFlashCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateLayerMask(Model.SignalMask.IndicatorFlash, m_IndicatorFlashCheckbox.CheckState);
        }

        private void m_IndicatorSolidCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            UpdateLayerMask(Model.SignalMask.IndicatorSolid, m_IndicatorSolidCheckbox.CheckState);
        }

        private class SignalSetUndoEntry : UndoRedoBuffer.Entry
        {
            public SignalSetUndoEntry(UndoRedoBuffer parent, Model.Layer layer, Model.SignalSet old_value, Model.SignalSet new_value)
                : base(parent)
            {
                m_Layer = layer;
                m_OldValue = old_value;
                m_NewValue = new_value;
            }

            public override void Undo()
            {
                m_Layer.SignalSet = m_OldValue;
            }

            public override void Redo()
            {
                m_Layer.SignalSet = m_NewValue;
            }

            private Model.Layer m_Layer;
            private Model.SignalSet m_OldValue;
            private Model.SignalSet m_NewValue;
        }

        private BitmapView m_BitmapView;
        private UndoRedoBuffer m_UndoRedoBuffer;
        private Model.Layer m_Layer;
    }
}
