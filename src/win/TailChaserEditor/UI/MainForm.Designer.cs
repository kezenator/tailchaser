namespace Com.TailChaser.Editor.UI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Com.TailChaser.Editor.Model.Palette palette1 = new Com.TailChaser.Editor.Model.Palette();
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditUndoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditRedoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_BitmapView = new Com.TailChaser.Editor.UI.Controls.BitmapView();
            this.m_PaletteView = new Com.TailChaser.Editor.UI.Controls.PaletteView();
            this.m_UndoRedoBuffer = new Com.TailChaser.Editor.UI.Controls.UndoRedoBuffer(this.components);
            this.m_MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileMenu,
            this.m_EditMenu});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Size = new System.Drawing.Size(775, 24);
            this.m_MenuStrip.TabIndex = 4;
            this.m_MenuStrip.Text = "menuStrip1";
            // 
            // m_FileMenu
            // 
            this.m_FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileExitMenuItem});
            this.m_FileMenu.Name = "m_FileMenu";
            this.m_FileMenu.Size = new System.Drawing.Size(37, 20);
            this.m_FileMenu.Text = "&File";
            // 
            // m_FileExitMenuItem
            // 
            this.m_FileExitMenuItem.Name = "m_FileExitMenuItem";
            this.m_FileExitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m_FileExitMenuItem.Text = "E&xit";
            this.m_FileExitMenuItem.Click += new System.EventHandler(this.m_FileExitMenuItem_Click);
            // 
            // m_EditMenu
            // 
            this.m_EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_EditUndoMenuItem,
            this.m_EditRedoMenuItem});
            this.m_EditMenu.Name = "m_EditMenu";
            this.m_EditMenu.Size = new System.Drawing.Size(39, 20);
            this.m_EditMenu.Text = "&Edit";
            // 
            // m_EditUndoMenuItem
            // 
            this.m_EditUndoMenuItem.Name = "m_EditUndoMenuItem";
            this.m_EditUndoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.m_EditUndoMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m_EditUndoMenuItem.Text = "&Undo";
            this.m_EditUndoMenuItem.Click += new System.EventHandler(this.m_EditUndoMenuItem_Click);
            // 
            // m_EditRedoMenuItem
            // 
            this.m_EditRedoMenuItem.Name = "m_EditRedoMenuItem";
            this.m_EditRedoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.m_EditRedoMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m_EditRedoMenuItem.Text = "&Redo";
            this.m_EditRedoMenuItem.Click += new System.EventHandler(this.m_EditRedoMenuItem_Click);
            // 
            // m_BitmapView
            // 
            this.m_BitmapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BitmapView.Location = new System.Drawing.Point(12, 27);
            this.m_BitmapView.Name = "m_BitmapView";
            this.m_BitmapView.PaletteView = this.m_PaletteView;
            this.m_BitmapView.Size = new System.Drawing.Size(751, 321);
            this.m_BitmapView.TabIndex = 3;
            this.m_BitmapView.UndoRedoBuffer = this.m_UndoRedoBuffer;
            // 
            // m_PaletteView
            // 
            this.m_PaletteView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PaletteView.Location = new System.Drawing.Point(368, 354);
            this.m_PaletteView.Name = "m_PaletteView";
            this.m_PaletteView.Palette = palette1;
            this.m_PaletteView.SelectedIndex = 0;
            this.m_PaletteView.Size = new System.Drawing.Size(395, 308);
            this.m_PaletteView.TabIndex = 2;
            // 
            // m_UndoRedoBuffer
            // 
            this.m_UndoRedoBuffer.OnUndoAvailableChanged += new Com.TailChaser.Editor.UI.Controls.UndoRedoAvailableChangedDelegate(this.m_UndoRedoBuffer_OnUndoAvailableChanged);
            this.m_UndoRedoBuffer.OnRedoAvailableChanged += new Com.TailChaser.Editor.UI.Controls.UndoRedoAvailableChangedDelegate(this.m_UndoRedoBuffer_OnRedoAvailableChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 674);
            this.Controls.Add(this.m_BitmapView);
            this.Controls.Add(this.m_PaletteView);
            this.Controls.Add(this.m_MenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.m_MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "TailChaser Editor";
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.PaletteView m_PaletteView;
        private Controls.BitmapView m_BitmapView;
        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_FileMenu;
        private System.Windows.Forms.ToolStripMenuItem m_FileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_EditMenu;
        private System.Windows.Forms.ToolStripMenuItem m_EditUndoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_EditRedoMenuItem;
        private Controls.UndoRedoBuffer m_UndoRedoBuffer;
    }
}