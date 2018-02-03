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
    public partial class SchemeView : UserControl
    {
        public SchemeView()
        {
            InitializeComponent();

            UpdateEnables();
        }

        public Model.Scheme Scheme
        {
            get
            {
                return m_Scheme;
            }

            set
            {
                if (m_Scheme != null)
                {
                    // Remove old event handlers

                    m_Scheme.OnNameChanged -= OnNameChanged;
                    m_Scheme.OnDescriptionChanged -= OnDescriptionChanged;
                }

                // Save value

                m_Scheme = value;

                // Add new event handlers

                if (m_Scheme != null)
                {
                    m_Scheme.OnNameChanged += OnNameChanged;
                    m_Scheme.OnDescriptionChanged += OnDescriptionChanged;
                }

                // Update UI

                m_LayerListView.Scheme = value;
                UpdateEnables();

                if (m_Scheme != null)
                {
                    m_NameTextBox.Text = m_Scheme.Name;
                    m_DescriptionTextBox.Text = m_Scheme.Description;
                }
                else
                {
                    m_NameTextBox.Text = "";
                    m_DescriptionTextBox.Text = "";
                }
            }
        }

        public LayerView LayerView
        {
            get
            {
                return m_LayerListView.LayerView;
            }

            set
            {
                m_LayerListView.LayerView = value;
            }
        }

        private void m_AddLayerButton_Click(object sender, EventArgs e)
        {
            if (m_Scheme != null)
            {
                m_Scheme.AddLayer();
            }
        }

        private void UpdateEnables()
        {
            bool has_scheme = (m_Scheme != null);

            m_NameTextBox.Enabled = has_scheme;
            m_DescriptionTextBox.Enabled = has_scheme;
            m_AddLayerButton.Enabled = has_scheme;
        }

        private void OnNameChanged(Model.Scheme s)
        {
            if (s == m_Scheme)
            {
                m_NameTextBox.Text = m_Scheme.Name;
            }
        }

        private void OnDescriptionChanged(Model.Scheme s)
        {
            if (s == m_Scheme)
            {
                m_DescriptionTextBox.Text = m_Scheme.Description;
            }
        }

        private void m_NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (m_Scheme != null)
            {
                m_Scheme.Name = m_NameTextBox.Text;
            }
        }

        private void m_DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (m_Scheme != null)
            {
                m_Scheme.Description = m_DescriptionTextBox.Text;
            }
        }

        private Model.Scheme m_Scheme;
    }
}
