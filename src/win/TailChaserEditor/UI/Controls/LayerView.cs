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

            m_PatternCombo.Items.Clear();
            foreach (Model.Pattern value in Enum.GetValues(typeof(Model.Pattern)))
            {
                m_PatternCombo.Items.Add(value.ToString());
            }

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

                m_PatternCombo.SelectedIndex = 0;
                m_PatternCombo.Enabled = false;

                m_Field1Label.Text = "N/A";
                m_Field1Text.Text = "";
                m_Field1Text.Enabled = false;

                m_Field2Label.Text = "N/A";
                m_Field2Text.Text = "";
                m_Field2Text.Enabled = false;

                m_Field3Label.Text = "N/A";
                m_Field3Text.Text = "";
                m_Field3Text.Enabled = false;
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

                m_PatternCombo.SelectedIndex = (int)m_Layer.Pattern;
                m_PatternCombo.Enabled = true;

                m_Field1Text.Text = m_Layer.Field1.ToString();
                m_Field1Text.Enabled = true;

                m_Field2Text.Text = m_Layer.Field2.ToString();
                m_Field2Text.Enabled = true;

                m_Field3Text.Text = m_Layer.Field3.ToString();
                m_Field3Text.Enabled = true;

                UpdateFieldLabels();
            }
        }

        private void UpdateFieldLabels()
        {
            switch (m_Layer.Pattern)
            {
                case Model.Pattern.Solid:
                    m_Field1Label.Text = "N/A";
                    m_Field2Label.Text = "N/A";
                    m_Field3Label.Text = "N/A";
                    break;

                case Model.Pattern.Flashing:
                    m_Field1Label.Text = "On Time (ms)";
                    m_Field2Label.Text = "Off Time (ms)";
                    m_Field3Label.Text = "N/A";
                    break;

                case Model.Pattern.SwipeLeft:
                case Model.Pattern.SwipeRight:
                case Model.Pattern.SwipeUp:
                case Model.Pattern.SwipeDown:
                    m_Field1Label.Text = "Swipe Time (ms)";
                    m_Field2Label.Text = "On Time (ms)";
                    m_Field3Label.Text = "Off Time (ms)";
                    break;

                case Model.Pattern.ScrollLeft:
                case Model.Pattern.ScrollRight:
                case Model.Pattern.ScrollUp:
                case Model.Pattern.ScrollDown:
                    m_Field1Label.Text = "Scroll Period (ms)";
                    m_Field2Label.Text = "Size (pixels)";
                    m_Field3Label.Text = "N/A";
                    break;
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
                    UpdateValues(m_Layer.Name, new_value, m_Layer.Pattern, m_Layer.Field1, m_Layer.Field2, m_Layer.Field3);
                }
            }
        }

        private void UpdateValues(string name, Model.SignalSet signals, Model.Pattern pattern, UInt16 field_1, UInt16 field_2, UInt16 field_3)
        {
            UndoRedoBuffer.Entry entry = new LayerUndoEntry(
                m_UndoRedoBuffer,
                m_Layer,
                m_Layer.Name,
                m_Layer.SignalSet,
                m_Layer.Pattern,
                m_Layer.Field1,
                m_Layer.Field2,
                m_Layer.Field3,
                name,
                signals,
                pattern,
                field_1,
                field_2,
                field_3);

            entry.PerformAndPushUndo();
        }

        private void m_NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (m_Layer != null)
            {
                UpdateValues(m_NameTextBox.Text, m_Layer.SignalSet, m_Layer.Pattern, m_Layer.Field1, m_Layer.Field2, m_Layer.Field3);
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

        private void m_PatternCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Layer != null)
            {
                Model.Pattern new_pattern = (Model.Pattern)m_PatternCombo.SelectedIndex;

                UpdateFieldLabels();

                if (new_pattern != m_Layer.Pattern)
                    UpdateValues(m_Layer.Name, m_Layer.SignalSet, new_pattern, m_Layer.Field1, m_Layer.Field2, m_Layer.Field3);
            }
        }

        private void m_Field1Text_TextChanged(object sender, EventArgs e)
        {
            if (m_Layer != null)
            {
                UInt16 value = 0;
                UInt16.TryParse(m_Field1Text.Text, out value);

                if (value != m_Layer.Field1)
                    UpdateValues(m_Layer.Name, m_Layer.SignalSet, m_Layer.Pattern, value, m_Layer.Field2, m_Layer.Field3);
            }
        }

        private void m_Field2Text_TextChanged(object sender, EventArgs e)
        {
            if (m_Layer != null)
            {
                UInt16 value = 0;
                UInt16.TryParse(m_Field2Text.Text, out value);

                if (value != m_Layer.Field2)
                    UpdateValues(m_Layer.Name, m_Layer.SignalSet, m_Layer.Pattern, m_Layer.Field1, value, m_Layer.Field3);
            }
        }

        private void m_Field3Text_TextChanged(object sender, EventArgs e)
        {
            if (m_Layer != null)
            {
                UInt16 value = 0;
                UInt16.TryParse(m_Field3Text.Text, out value);

                if (value != m_Layer.Field3)
                    UpdateValues(m_Layer.Name, m_Layer.SignalSet, m_Layer.Pattern, m_Layer.Field1, m_Layer.Field2, value);
            }
        }

        private class LayerUndoEntry : UndoRedoBuffer.Entry
        {
            public LayerUndoEntry(UndoRedoBuffer parent, Model.Layer layer,
                string old_name, Model.SignalSet old_signals, Model.Pattern old_pattern, UInt16 old_field_1, UInt16 old_field_2, UInt16 old_field_3,
                string new_name, Model.SignalSet new_signals, Model.Pattern new_pattern, UInt16 new_field_1, UInt16 new_field_2, UInt16 new_field_3)
                : base(parent)
            {
                m_Layer = layer;
                m_OldName = old_name;
                m_OldSignals = old_signals;
                m_OldPattern = old_pattern;
                m_OldField1 = old_field_1;
                m_OldField2 = old_field_2;
                m_OldField3 = old_field_3;
                m_NewName = new_name;
                m_NewSignals = new_signals;
                m_NewPattern = new_pattern;
                m_NewField1 = new_field_1;
                m_NewField2 = new_field_2;
                m_NewField3 = new_field_3;
            }

            public override void Undo()
            {
                m_Layer.Name = m_OldName;
                m_Layer.SignalSet = m_OldSignals;
                m_Layer.Pattern = m_OldPattern;
                m_Layer.Field1 = m_OldField1;
                m_Layer.Field2 = m_OldField2;
                m_Layer.Field3 = m_OldField3;
            }

            public override void Redo()
            {
                m_Layer.Name = m_NewName;
                m_Layer.SignalSet = m_NewSignals;
                m_Layer.Pattern = m_NewPattern;
                m_Layer.Field1 = m_NewField1;
                m_Layer.Field2 = m_NewField2;
                m_Layer.Field3 = m_NewField3;
            }

            private Model.Layer m_Layer;
            private string m_OldName;
            private Model.SignalSet m_OldSignals;
            private Model.Pattern m_OldPattern;
            private UInt16 m_OldField1;
            private UInt16 m_OldField2;
            private UInt16 m_OldField3;
            private string m_NewName;
            private Model.SignalSet m_NewSignals;
            private Model.Pattern m_NewPattern;
            private UInt16 m_NewField1;
            private UInt16 m_NewField2;
            private UInt16 m_NewField3;
        }

        private BitmapView m_BitmapView;
        private UndoRedoBuffer m_UndoRedoBuffer;
        private Model.Layer m_Layer;
    }
}
