namespace Com.TailChaser.Editor.UI.Controls
{
    partial class LayerListView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_ListView = new DoubleBufferedListView();
            this.m_ListViewMainColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // m_ListView
            // 
            this.m_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_ListViewMainColumn});
            this.m_ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ListView.FullRowSelect = true;
            this.m_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.m_ListView.HideSelection = false;
            this.m_ListView.Location = new System.Drawing.Point(0, 0);
            this.m_ListView.MultiSelect = false;
            this.m_ListView.Name = "m_ListView";
            this.m_ListView.OwnerDraw = true;
            this.m_ListView.ShowGroups = false;
            this.m_ListView.Size = new System.Drawing.Size(320, 455);
            this.m_ListView.TabIndex = 0;
            this.m_ListView.UseCompatibleStateImageBehavior = false;
            this.m_ListView.View = System.Windows.Forms.View.Details;
            this.m_ListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.m_ListView_DrawItem);
            this.m_ListView.SelectedIndexChanged += new System.EventHandler(this.m_ListView_SelectedIndexChanged);
            this.m_ListView.ClientSizeChanged += new System.EventHandler(this.m_ListView_ClientSizeChanged);
            // 
            // LayerListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_ListView);
            this.Name = "LayerListView";
            this.Size = new System.Drawing.Size(320, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private DoubleBufferedListView m_ListView;
        private System.Windows.Forms.ColumnHeader m_ListViewMainColumn;
    }
}
