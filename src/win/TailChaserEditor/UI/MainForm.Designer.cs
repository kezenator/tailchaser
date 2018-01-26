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
            Com.TailChaser.Editor.Model.Palette palette2 = new Com.TailChaser.Editor.Model.Palette();
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditUndoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditRedoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MainTabs = new System.Windows.Forms.TabControl();
            this.m_EditTab = new System.Windows.Forms.TabPage();
            this.m_SimulateTab = new System.Windows.Forms.TabPage();
            this.m_ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ViewEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ViewSimulateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SchemeView = new Com.TailChaser.Editor.UI.Controls.SchemeView();
            this.m_LayerView = new Com.TailChaser.Editor.UI.Controls.LayerView();
            this.m_BitmapView = new Com.TailChaser.Editor.UI.Controls.BitmapView();
            this.m_PaletteView = new Com.TailChaser.Editor.UI.Controls.PaletteView();
            this.m_UndoRedoBuffer = new Com.TailChaser.Editor.UI.Controls.UndoRedoBuffer(this.components);
            this.m_MenuStrip.SuspendLayout();
            this.m_MainTabs.SuspendLayout();
            this.m_EditTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileMenu,
            this.m_EditMenu,
            this.m_ViewMenu});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Size = new System.Drawing.Size(1202, 24);
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
            this.m_FileExitMenuItem.Size = new System.Drawing.Size(92, 22);
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
            this.m_EditUndoMenuItem.Size = new System.Drawing.Size(144, 22);
            this.m_EditUndoMenuItem.Text = "&Undo";
            this.m_EditUndoMenuItem.Click += new System.EventHandler(this.m_EditUndoMenuItem_Click);
            // 
            // m_EditRedoMenuItem
            // 
            this.m_EditRedoMenuItem.Name = "m_EditRedoMenuItem";
            this.m_EditRedoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.m_EditRedoMenuItem.Size = new System.Drawing.Size(144, 22);
            this.m_EditRedoMenuItem.Text = "&Redo";
            this.m_EditRedoMenuItem.Click += new System.EventHandler(this.m_EditRedoMenuItem_Click);
            // 
            // m_MainTabs
            // 
            this.m_MainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_MainTabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.m_MainTabs.Controls.Add(this.m_EditTab);
            this.m_MainTabs.Controls.Add(this.m_SimulateTab);
            this.m_MainTabs.Location = new System.Drawing.Point(12, 27);
            this.m_MainTabs.Name = "m_MainTabs";
            this.m_MainTabs.SelectedIndex = 0;
            this.m_MainTabs.Size = new System.Drawing.Size(1178, 771);
            this.m_MainTabs.TabIndex = 7;
            // 
            // m_EditTab
            // 
            this.m_EditTab.Controls.Add(this.m_SchemeView);
            this.m_EditTab.Controls.Add(this.m_LayerView);
            this.m_EditTab.Controls.Add(this.m_BitmapView);
            this.m_EditTab.Controls.Add(this.m_PaletteView);
            this.m_EditTab.Location = new System.Drawing.Point(4, 29);
            this.m_EditTab.Name = "m_EditTab";
            this.m_EditTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_EditTab.Size = new System.Drawing.Size(1170, 738);
            this.m_EditTab.TabIndex = 0;
            this.m_EditTab.Text = "Edit (F4)";
            this.m_EditTab.UseVisualStyleBackColor = true;
            // 
            // m_SimulateTab
            // 
            this.m_SimulateTab.Location = new System.Drawing.Point(4, 29);
            this.m_SimulateTab.Name = "m_SimulateTab";
            this.m_SimulateTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_SimulateTab.Size = new System.Drawing.Size(1170, 738);
            this.m_SimulateTab.TabIndex = 1;
            this.m_SimulateTab.Text = "Simulate (F5)";
            this.m_SimulateTab.UseVisualStyleBackColor = true;
            // 
            // m_ViewMenu
            // 
            this.m_ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ViewEditMenuItem,
            this.m_ViewSimulateMenuItem});
            this.m_ViewMenu.Name = "m_ViewMenu";
            this.m_ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.m_ViewMenu.Text = "&View";
            // 
            // m_ViewEditMenuItem
            // 
            this.m_ViewEditMenuItem.Name = "m_ViewEditMenuItem";
            this.m_ViewEditMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.m_ViewEditMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m_ViewEditMenuItem.Text = "&Edit";
            this.m_ViewEditMenuItem.Click += new System.EventHandler(this.m_ViewEditMenuItem_Click);
            // 
            // m_ViewSimulateMenuItem
            // 
            this.m_ViewSimulateMenuItem.Name = "m_ViewSimulateMenuItem";
            this.m_ViewSimulateMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.m_ViewSimulateMenuItem.Size = new System.Drawing.Size(152, 22);
            this.m_ViewSimulateMenuItem.Text = "&Simulate";
            this.m_ViewSimulateMenuItem.Click += new System.EventHandler(this.m_ViewSimulateMenuItem_Click);
            // 
            // m_SchemeView
            // 
            this.m_SchemeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_SchemeView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_SchemeView.LayerView = this.m_LayerView;
            this.m_SchemeView.Location = new System.Drawing.Point(0, 0);
            this.m_SchemeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_SchemeView.Name = "m_SchemeView";
            this.m_SchemeView.Scheme = null;
            this.m_SchemeView.Size = new System.Drawing.Size(275, 730);
            this.m_SchemeView.TabIndex = 7;
            // 
            // m_LayerView
            // 
            this.m_LayerView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_LayerView.BitmapView = this.m_BitmapView;
            this.m_LayerView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_LayerView.Layer = null;
            this.m_LayerView.Location = new System.Drawing.Point(281, 468);
            this.m_LayerView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_LayerView.MinimumSize = new System.Drawing.Size(303, 262);
            this.m_LayerView.Name = "m_LayerView";
            this.m_LayerView.Size = new System.Drawing.Size(303, 262);
            this.m_LayerView.TabIndex = 9;
            // 
            // m_BitmapView
            // 
            this.m_BitmapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BitmapView.Bitmap = null;
            this.m_BitmapView.Location = new System.Drawing.Point(281, 8);
            this.m_BitmapView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.m_BitmapView.Name = "m_BitmapView";
            this.m_BitmapView.PaletteView = this.m_PaletteView;
            this.m_BitmapView.Size = new System.Drawing.Size(883, 451);
            this.m_BitmapView.TabIndex = 11;
            this.m_BitmapView.UndoRedoBuffer = this.m_UndoRedoBuffer;
            // 
            // m_PaletteView
            // 
            this.m_PaletteView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PaletteView.Location = new System.Drawing.Point(775, 465);
            this.m_PaletteView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_PaletteView.Name = "m_PaletteView";
            this.m_PaletteView.Palette = palette2;
            this.m_PaletteView.SelectedIndex = 0;
            this.m_PaletteView.Size = new System.Drawing.Size(392, 262);
            this.m_PaletteView.TabIndex = 10;
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
            this.ClientSize = new System.Drawing.Size(1202, 810);
            this.Controls.Add(this.m_MainTabs);
            this.Controls.Add(this.m_MenuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.m_MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "TailChaser Editor";
            this.m_MenuStrip.ResumeLayout(false);
            this.m_MenuStrip.PerformLayout();
            this.m_MainTabs.ResumeLayout(false);
            this.m_EditTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip m_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_FileMenu;
        private System.Windows.Forms.ToolStripMenuItem m_FileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_EditMenu;
        private System.Windows.Forms.ToolStripMenuItem m_EditUndoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_EditRedoMenuItem;
        private Controls.UndoRedoBuffer m_UndoRedoBuffer;
        private System.Windows.Forms.TabControl m_MainTabs;
        private System.Windows.Forms.TabPage m_EditTab;
        private Controls.PaletteView m_PaletteView;
        private Controls.LayerView m_LayerView;
        private Controls.SchemeView m_SchemeView;
        private System.Windows.Forms.TabPage m_SimulateTab;
        private Controls.BitmapView m_BitmapView;
        private System.Windows.Forms.ToolStripMenuItem m_ViewMenu;
        private System.Windows.Forms.ToolStripMenuItem m_ViewEditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_ViewSimulateMenuItem;
    }
}