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
    public partial class LayerListView : UserControl
    {
        public LayerListView()
        {
            InitializeComponent();

            m_Scheme = null;
            m_LayerView = null;
        }

        public Model.Scheme Scheme
        {
            get
            {
                return m_Scheme;
            }

            set
            {
                if (value != m_Scheme)
                {
                    // Remove old event handlers

                    if (m_Scheme != null)
                    {
                        m_Scheme.OnLayerListChanged -= OnSchemeLayerListChanged;
                    }

                    // Save value

                    m_Scheme = value;

                    // Add new event handlers
                    // and update the UI

                    if (m_Scheme != null)
                    {
                        m_Scheme.OnLayerListChanged += OnSchemeLayerListChanged;

                        m_ListView.Items.Clear();

                        foreach (Model.Layer layer in m_Scheme.Layers)
                        {
                            ListViewItem item = new ListViewItem("");
                            item.Tag = layer;
                            m_ListView.Items.Add(item);

                            layer.OnChange += OnLayerChanged;
                        }
                    }
                    else
                    {
                        m_ListView.Items.Clear();
                    }

                    m_ListView.SelectedItems.Clear();

                    if (m_LayerView != null)
                    {
                        m_LayerView.Layer = null;
                    }
                }
            }
        }

        public LayerView LayerView
        {
            get
            {
                return m_LayerView;
            }

            set
            {
                if (m_LayerView != value)
                {
                    // Remove selected layer (if any) from
                    // existing view

                    if (m_LayerView != null)
                    {
                        m_LayerView.Layer = null;
                    }

                    // Store new view

                    m_LayerView = value;

                    // Display currently selected layer
                    // in new view

                    if (m_LayerView != null)
                    {
                        if (m_ListView.SelectedItems.Count != 0)
                        {
                            m_LayerView.Layer = (Model.Layer)m_ListView.SelectedItems[0].Tag;
                        }
                        else // nothing selected
                        {
                            m_LayerView.Layer = null;
                        }
                    }
                }
            }
        }

        private void OnSchemeLayerListChanged(Model.Scheme scheme, int index, Model.Layer layer, bool add)
        {
            if (add)
            {
                ListViewItem item = new ListViewItem("");
                item.Tag = layer;
                m_ListView.Items.Insert(index, item);

                layer.OnChange += OnLayerChanged;
            }
            else
            {
                throw new NotSupportedException("TODO - support item delete");
            }
        }

        private void OnLayerChanged(Model.Layer layer)
        {
            m_ListView.Invalidate();
        }

        private Model.Scheme m_Scheme;
        private LayerView m_LayerView;

        private void m_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_LayerView != null)
            {
                Model.Layer layer = null;

                if (m_ListView.SelectedItems.Count != 0)
                {
                    layer = (Model.Layer)m_ListView.SelectedItems[0].Tag;
                }

                m_LayerView.Layer = layer;
            }
        }

        private void m_ListView_ClientSizeChanged(object sender, EventArgs e)
        {
            m_ListViewMainColumn.Width = Width - 6;
        }

        private void m_ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            int required_item_height = 0;
            if (m_ListView.SmallImageList != null)
                required_item_height = m_ListView.SmallImageList.ImageSize.Height;

            Model.Layer layer = (Model.Layer)e.Item.Tag;

            Color background = SystemColors.Window;
            Color foreground = SystemColors.WindowText;

            if (e.Item.Selected)
            {
                if (m_ListView.Focused)
                {
                    background = SystemColors.Highlight;
                    foreground = SystemColors.HighlightText;
                }
                else
                {
                    background = SystemColors.InactiveCaption;
                    foreground = SystemColors.InactiveCaptionText;
                }
            }

            using (Brush b = new SolidBrush(background))
            {
                e.Graphics.FillRectangle(b, e.Bounds);
            }

            using (Brush b = new SolidBrush(foreground))
            {
                string line_1 = layer.Name.Trim();
                if (line_1.Length == 0)
                    line_1 = "<UN-NAMED>";

                string line_2 = layer.SignalSet.ToString();

                int y = e.Bounds.Top;

                Size proposed_size = new Size(m_ListViewMainColumn.Width, 65534);

                TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.WordBreak;

                using (Font bold = new Font(Font, FontStyle.Bold))
                {
                    Rectangle line_1_rect = new Rectangle(e.Bounds.Left, y, proposed_size.Width, proposed_size.Height);
                    y += DrawText(e.Graphics, line_1, bold, line_1_rect, foreground, flags);
                }

                Rectangle line_2_rect = new Rectangle(e.Bounds.Left + 20, y, proposed_size.Width - 20, proposed_size.Height);
                y += DrawText(e.Graphics, line_2, Font, line_2_rect, foreground, flags);

                int item_height = y - e.Bounds.Top + 1;

                if (required_item_height < item_height)
                    required_item_height = item_height;
            }

            if ((m_ListView.SmallImageList == null)
                || (required_item_height != m_ListView.SmallImageList.ImageSize.Height))
            {
                ImageList list = new ImageList();
                list.ImageSize = new Size(1, required_item_height);
                m_ListView.SmallImageList = list;
            }
        }

        private int DrawText(Graphics g, string text, Font font, Rectangle rect, Color color, TextFormatFlags flags)
        {
            TextRenderer.DrawText(g, text, font, rect, color, flags);
            return TextRenderer.MeasureText(g, text, font, rect.Size, flags).Height;
        }
    }
}
