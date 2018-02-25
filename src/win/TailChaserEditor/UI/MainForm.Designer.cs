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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            Com.TailChaser.Editor.Model.Palette palette1 = new Com.TailChaser.Editor.Model.Palette();
            this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.m_FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileUploadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_FileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditUndoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_EditRedoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ViewEditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ViewSimulateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ViewDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SimulateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SimulateTailMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SimulateBrakeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SimulateReverseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_SimulateIndicatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_MainTabs = new System.Windows.Forms.TabControl();
            this.m_EditTab = new System.Windows.Forms.TabPage();
            this.m_SchemeView = new Com.TailChaser.Editor.UI.Controls.SchemeView();
            this.m_LayerView = new Com.TailChaser.Editor.UI.Controls.LayerView();
            this.m_BitmapView = new Com.TailChaser.Editor.UI.Controls.BitmapView();
            this.m_PaletteView = new Com.TailChaser.Editor.UI.Controls.PaletteView();
            this.m_UndoRedoBuffer = new Com.TailChaser.Editor.UI.Controls.UndoRedoBuffer(this.components);
            this.m_SimulateTab = new System.Windows.Forms.TabPage();
            this.m_SimulatorView = new Com.TailChaser.Editor.UI.Controls.SimulatorView();
            this.m_DevicePage = new System.Windows.Forms.TabPage();
            this.m_DeviceView = new Com.TailChaser.Editor.UI.Controls.DeviceView();
            this.m_SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_MenuStrip.SuspendLayout();
            this.m_MainTabs.SuspendLayout();
            this.m_EditTab.SuspendLayout();
            this.m_SimulateTab.SuspendLayout();
            this.m_DevicePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(128, 6);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(128, 6);
            // 
            // m_MenuStrip
            // 
            this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileMenu,
            this.m_EditMenu,
            this.m_ViewMenu,
            this.m_SimulateMenu});
            this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.m_MenuStrip.Name = "m_MenuStrip";
            this.m_MenuStrip.Size = new System.Drawing.Size(1202, 24);
            this.m_MenuStrip.TabIndex = 4;
            this.m_MenuStrip.Text = "menuStrip1";
            // 
            // m_FileMenu
            // 
            this.m_FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_FileNewMenuItem,
            toolStripSeparator3,
            this.m_FileOpenMenuItem,
            this.m_FileSaveMenuItem,
            this.m_FileSaveAsMenuItem,
            toolStripSeparator1,
            this.m_FileUploadMenuItem,
            toolStripSeparator2,
            this.m_FileExitMenuItem});
            this.m_FileMenu.Name = "m_FileMenu";
            this.m_FileMenu.Size = new System.Drawing.Size(37, 20);
            this.m_FileMenu.Text = "&File";
            // 
            // m_FileNewMenuItem
            // 
            this.m_FileNewMenuItem.Name = "m_FileNewMenuItem";
            this.m_FileNewMenuItem.Size = new System.Drawing.Size(131, 22);
            this.m_FileNewMenuItem.Text = "&New";
            this.m_FileNewMenuItem.Click += new System.EventHandler(this.m_FileNewMenuItem_Click);
            // 
            // m_FileOpenMenuItem
            // 
            this.m_FileOpenMenuItem.Name = "m_FileOpenMenuItem";
            this.m_FileOpenMenuItem.Size = new System.Drawing.Size(131, 22);
            this.m_FileOpenMenuItem.Text = "&Open...";
            this.m_FileOpenMenuItem.Click += new System.EventHandler(this.m_FileOpenMenuItem_Click);
            // 
            // m_FileSaveMenuItem
            // 
            this.m_FileSaveMenuItem.Name = "m_FileSaveMenuItem";
            this.m_FileSaveMenuItem.Size = new System.Drawing.Size(131, 22);
            this.m_FileSaveMenuItem.Text = "&Save";
            this.m_FileSaveMenuItem.Click += new System.EventHandler(this.m_FileSaveMenuItem_Click);
            // 
            // m_FileSaveAsMenuItem
            // 
            this.m_FileSaveAsMenuItem.Name = "m_FileSaveAsMenuItem";
            this.m_FileSaveAsMenuItem.Size = new System.Drawing.Size(131, 22);
            this.m_FileSaveAsMenuItem.Text = "Save &as...";
            this.m_FileSaveAsMenuItem.Click += new System.EventHandler(this.m_FileSaveAsMenuItem_Click);
            // 
            // m_FileUploadMenuItem
            // 
            this.m_FileUploadMenuItem.Name = "m_FileUploadMenuItem";
            this.m_FileUploadMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.m_FileUploadMenuItem.Size = new System.Drawing.Size(131, 22);
            this.m_FileUploadMenuItem.Text = "&Upload";
            this.m_FileUploadMenuItem.Click += new System.EventHandler(this.m_FileUploadMenuItem_Click);
            // 
            // m_FileExitMenuItem
            // 
            this.m_FileExitMenuItem.Name = "m_FileExitMenuItem";
            this.m_FileExitMenuItem.Size = new System.Drawing.Size(131, 22);
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
            // m_ViewMenu
            // 
            this.m_ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ViewEditMenuItem,
            this.m_ViewSimulateMenuItem,
            this.m_ViewDeviceMenuItem});
            this.m_ViewMenu.Name = "m_ViewMenu";
            this.m_ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.m_ViewMenu.Text = "&View";
            // 
            // m_ViewEditMenuItem
            // 
            this.m_ViewEditMenuItem.Name = "m_ViewEditMenuItem";
            this.m_ViewEditMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.m_ViewEditMenuItem.Size = new System.Drawing.Size(139, 22);
            this.m_ViewEditMenuItem.Text = "&Edit";
            this.m_ViewEditMenuItem.Click += new System.EventHandler(this.m_ViewEditMenuItem_Click);
            // 
            // m_ViewSimulateMenuItem
            // 
            this.m_ViewSimulateMenuItem.Name = "m_ViewSimulateMenuItem";
            this.m_ViewSimulateMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.m_ViewSimulateMenuItem.Size = new System.Drawing.Size(139, 22);
            this.m_ViewSimulateMenuItem.Text = "&Simulate";
            this.m_ViewSimulateMenuItem.Click += new System.EventHandler(this.m_ViewSimulateMenuItem_Click);
            // 
            // m_ViewDeviceMenuItem
            // 
            this.m_ViewDeviceMenuItem.Name = "m_ViewDeviceMenuItem";
            this.m_ViewDeviceMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.m_ViewDeviceMenuItem.Size = new System.Drawing.Size(139, 22);
            this.m_ViewDeviceMenuItem.Text = "&Device";
            this.m_ViewDeviceMenuItem.Click += new System.EventHandler(this.m_ViewDeviceMenuItem_Click);
            // 
            // m_SimulateMenu
            // 
            this.m_SimulateMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_SimulateTailMenuItem,
            this.m_SimulateBrakeMenuItem,
            this.m_SimulateReverseMenuItem,
            this.m_SimulateIndicatorMenuItem});
            this.m_SimulateMenu.Name = "m_SimulateMenu";
            this.m_SimulateMenu.Size = new System.Drawing.Size(65, 20);
            this.m_SimulateMenu.Text = "&Simulate";
            // 
            // m_SimulateTailMenuItem
            // 
            this.m_SimulateTailMenuItem.Name = "m_SimulateTailMenuItem";
            this.m_SimulateTailMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.m_SimulateTailMenuItem.Size = new System.Drawing.Size(140, 22);
            this.m_SimulateTailMenuItem.Text = "&Tail";
            this.m_SimulateTailMenuItem.Click += new System.EventHandler(this.m_SimulateTailMenuItem_Click);
            // 
            // m_SimulateBrakeMenuItem
            // 
            this.m_SimulateBrakeMenuItem.Name = "m_SimulateBrakeMenuItem";
            this.m_SimulateBrakeMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.m_SimulateBrakeMenuItem.Size = new System.Drawing.Size(140, 22);
            this.m_SimulateBrakeMenuItem.Text = "&Brake";
            this.m_SimulateBrakeMenuItem.Click += new System.EventHandler(this.m_SimulateBrakeMenuItem_Click);
            // 
            // m_SimulateReverseMenuItem
            // 
            this.m_SimulateReverseMenuItem.Name = "m_SimulateReverseMenuItem";
            this.m_SimulateReverseMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.m_SimulateReverseMenuItem.Size = new System.Drawing.Size(140, 22);
            this.m_SimulateReverseMenuItem.Text = "&Reverse";
            this.m_SimulateReverseMenuItem.Click += new System.EventHandler(this.m_SimulateReverseMenuItem_Click);
            // 
            // m_SimulateIndicatorMenuItem
            // 
            this.m_SimulateIndicatorMenuItem.Name = "m_SimulateIndicatorMenuItem";
            this.m_SimulateIndicatorMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.m_SimulateIndicatorMenuItem.Size = new System.Drawing.Size(140, 22);
            this.m_SimulateIndicatorMenuItem.Text = "&Indicator";
            this.m_SimulateIndicatorMenuItem.Click += new System.EventHandler(this.m_SimulateIndicatorMenuItem_Click);
            // 
            // m_MainTabs
            // 
            this.m_MainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_MainTabs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.m_MainTabs.Controls.Add(this.m_EditTab);
            this.m_MainTabs.Controls.Add(this.m_SimulateTab);
            this.m_MainTabs.Controls.Add(this.m_DevicePage);
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
            this.m_EditTab.Text = "Edit (F3)";
            this.m_EditTab.UseVisualStyleBackColor = true;
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
            this.m_SchemeView.Size = new System.Drawing.Size(275, 670);
            this.m_SchemeView.TabIndex = 7;
            // 
            // m_LayerView
            // 
            this.m_LayerView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_LayerView.BitmapView = this.m_BitmapView;
            this.m_LayerView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_LayerView.Layer = null;
            this.m_LayerView.Location = new System.Drawing.Point(281, 408);
            this.m_LayerView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_LayerView.MinimumSize = new System.Drawing.Size(303, 262);
            this.m_LayerView.Name = "m_LayerView";
            this.m_LayerView.Size = new System.Drawing.Size(373, 262);
            this.m_LayerView.TabIndex = 9;
            this.m_LayerView.UndoRedoBuffer = this.m_UndoRedoBuffer;
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
            this.m_BitmapView.Size = new System.Drawing.Size(883, 391);
            this.m_BitmapView.TabIndex = 11;
            this.m_BitmapView.UndoRedoBuffer = this.m_UndoRedoBuffer;
            // 
            // m_PaletteView
            // 
            this.m_PaletteView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_PaletteView.Location = new System.Drawing.Point(775, 405);
            this.m_PaletteView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_PaletteView.Name = "m_PaletteView";
            this.m_PaletteView.Palette = palette1;
            this.m_PaletteView.SelectedIndex = 0;
            this.m_PaletteView.Size = new System.Drawing.Size(392, 262);
            this.m_PaletteView.TabIndex = 10;
            // 
            // m_UndoRedoBuffer
            // 
            this.m_UndoRedoBuffer.OnDocumentChanged += new Com.TailChaser.Editor.UI.Controls.UndoRedoChangedDelegate(this.m_UndoRedoBuffer_OnDocumentChanged);
            this.m_UndoRedoBuffer.OnUndoAvailableChanged += new Com.TailChaser.Editor.UI.Controls.UndoRedoChangedDelegate(this.m_UndoRedoBuffer_OnUndoAvailableChanged);
            this.m_UndoRedoBuffer.OnRedoAvailableChanged += new Com.TailChaser.Editor.UI.Controls.UndoRedoChangedDelegate(this.m_UndoRedoBuffer_OnRedoAvailableChanged);
            // 
            // m_SimulateTab
            // 
            this.m_SimulateTab.Controls.Add(this.m_SimulatorView);
            this.m_SimulateTab.Location = new System.Drawing.Point(4, 29);
            this.m_SimulateTab.Name = "m_SimulateTab";
            this.m_SimulateTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_SimulateTab.Size = new System.Drawing.Size(1170, 738);
            this.m_SimulateTab.TabIndex = 1;
            this.m_SimulateTab.Text = "Simulate (F4)";
            this.m_SimulateTab.UseVisualStyleBackColor = true;
            // 
            // m_SimulatorView
            // 
            this.m_SimulatorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_SimulatorView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_SimulatorView.Location = new System.Drawing.Point(3, 3);
            this.m_SimulatorView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_SimulatorView.Name = "m_SimulatorView";
            this.m_SimulatorView.Scheme = null;
            this.m_SimulatorView.Size = new System.Drawing.Size(1164, 732);
            this.m_SimulatorView.TabIndex = 0;
            // 
            // m_DevicePage
            // 
            this.m_DevicePage.Controls.Add(this.m_DeviceView);
            this.m_DevicePage.Location = new System.Drawing.Point(4, 29);
            this.m_DevicePage.Name = "m_DevicePage";
            this.m_DevicePage.Size = new System.Drawing.Size(1170, 738);
            this.m_DevicePage.TabIndex = 2;
            this.m_DevicePage.Text = "Device (F5)";
            this.m_DevicePage.UseVisualStyleBackColor = true;
            // 
            // m_DeviceView
            // 
            this.m_DeviceView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_DeviceView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_DeviceView.Location = new System.Drawing.Point(0, 0);
            this.m_DeviceView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_DeviceView.Name = "m_DeviceView";
            this.m_DeviceView.Scheme = null;
            this.m_DeviceView.Size = new System.Drawing.Size(1170, 738);
            this.m_DeviceView.TabIndex = 0;
            // 
            // m_SaveFileDialog
            // 
            this.m_SaveFileDialog.DefaultExt = "h";
            this.m_SaveFileDialog.Filter = "C/C++ Header Files|*.h|All Files|*.*";
            // 
            // m_OpenFileDialog
            // 
            this.m_OpenFileDialog.DefaultExt = "h";
            this.m_OpenFileDialog.FileName = "openFileDialog1";
            this.m_OpenFileDialog.Filter = "C/C++ Header Files|*.h|All Files|*.*";
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
            this.m_SimulateTab.ResumeLayout(false);
            this.m_DevicePage.ResumeLayout(false);
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
        private Controls.SimulatorView m_SimulatorView;
        private System.Windows.Forms.ToolStripMenuItem m_SimulateMenu;
        private System.Windows.Forms.ToolStripMenuItem m_SimulateTailMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SimulateBrakeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SimulateReverseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_SimulateIndicatorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_FileSaveMenuItem;
        private System.Windows.Forms.SaveFileDialog m_SaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem m_FileOpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_FileSaveAsMenuItem;
        private System.Windows.Forms.OpenFileDialog m_OpenFileDialog;
        private System.Windows.Forms.ToolStripMenuItem m_ViewDeviceMenuItem;
        private System.Windows.Forms.TabPage m_DevicePage;
        private Controls.DeviceView m_DeviceView;
        private System.Windows.Forms.ToolStripMenuItem m_FileUploadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_FileNewMenuItem;
    }
}