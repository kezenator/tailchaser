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
                }

                // Save value

                m_Scheme = value;

                // Add new event handlers

                // Update UI

                m_LayerListView.Scheme = value;
                UpdateEnables();
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

        private Model.Scheme m_Scheme;
    }
}
