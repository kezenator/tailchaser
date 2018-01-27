namespace Com.TailChaser.Editor.UI.Controls
{
    partial class SimulatorView
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
            this.components = new System.ComponentModel.Container();
            this.m_BitmapView = new Com.TailChaser.Editor.UI.Controls.BitmapView();
            this.m_TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.m_TailButton = new System.Windows.Forms.Button();
            this.m_BrakeButton = new System.Windows.Forms.Button();
            this.m_ReverseButton = new System.Windows.Forms.Button();
            this.m_IndicatorButton = new System.Windows.Forms.Button();
            this.m_TailLabel = new System.Windows.Forms.Label();
            this.m_BrakeLabel = new System.Windows.Forms.Label();
            this.m_ReverseLabel = new System.Windows.Forms.Label();
            this.m_IndicatorLabel = new System.Windows.Forms.Label();
            this.m_IndicatorTimer = new System.Windows.Forms.Timer(this.components);
            this.m_RedrawTimer = new System.Windows.Forms.Timer(this.components);
            this.m_TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_BitmapView
            // 
            this.m_BitmapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BitmapView.Bitmap = null;
            this.m_BitmapView.Location = new System.Drawing.Point(3, 4);
            this.m_BitmapView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_BitmapView.Name = "m_BitmapView";
            this.m_BitmapView.PaletteView = null;
            this.m_BitmapView.Size = new System.Drawing.Size(1001, 483);
            this.m_BitmapView.TabIndex = 0;
            this.m_BitmapView.UndoRedoBuffer = null;
            // 
            // m_TableLayoutPanel
            // 
            this.m_TableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TableLayoutPanel.ColumnCount = 4;
            this.m_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_TableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.m_TableLayoutPanel.Controls.Add(this.m_TailButton, 0, 0);
            this.m_TableLayoutPanel.Controls.Add(this.m_BrakeButton, 1, 0);
            this.m_TableLayoutPanel.Controls.Add(this.m_ReverseButton, 2, 0);
            this.m_TableLayoutPanel.Controls.Add(this.m_IndicatorButton, 3, 0);
            this.m_TableLayoutPanel.Controls.Add(this.m_TailLabel, 0, 1);
            this.m_TableLayoutPanel.Controls.Add(this.m_BrakeLabel, 1, 1);
            this.m_TableLayoutPanel.Controls.Add(this.m_ReverseLabel, 2, 1);
            this.m_TableLayoutPanel.Controls.Add(this.m_IndicatorLabel, 3, 1);
            this.m_TableLayoutPanel.Location = new System.Drawing.Point(3, 495);
            this.m_TableLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_TableLayoutPanel.Name = "m_TableLayoutPanel";
            this.m_TableLayoutPanel.RowCount = 2;
            this.m_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.m_TableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.m_TableLayoutPanel.Size = new System.Drawing.Size(1001, 107);
            this.m_TableLayoutPanel.TabIndex = 1;
            // 
            // m_TailButton
            // 
            this.m_TailButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TailButton.Location = new System.Drawing.Point(3, 4);
            this.m_TailButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_TailButton.Name = "m_TailButton";
            this.m_TailButton.Size = new System.Drawing.Size(244, 68);
            this.m_TailButton.TabIndex = 0;
            this.m_TailButton.Text = "Tail Light (F6)";
            this.m_TailButton.UseVisualStyleBackColor = true;
            this.m_TailButton.Click += new System.EventHandler(this.m_TailButton_Click);
            // 
            // m_BrakeButton
            // 
            this.m_BrakeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_BrakeButton.Location = new System.Drawing.Point(253, 4);
            this.m_BrakeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_BrakeButton.Name = "m_BrakeButton";
            this.m_BrakeButton.Size = new System.Drawing.Size(244, 68);
            this.m_BrakeButton.TabIndex = 1;
            this.m_BrakeButton.Text = "Brake Light (F7)";
            this.m_BrakeButton.UseVisualStyleBackColor = true;
            this.m_BrakeButton.Click += new System.EventHandler(this.m_BrakeButton_Click);
            // 
            // m_ReverseButton
            // 
            this.m_ReverseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ReverseButton.Location = new System.Drawing.Point(503, 4);
            this.m_ReverseButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_ReverseButton.Name = "m_ReverseButton";
            this.m_ReverseButton.Size = new System.Drawing.Size(244, 68);
            this.m_ReverseButton.TabIndex = 2;
            this.m_ReverseButton.Text = "Reverse (F8)";
            this.m_ReverseButton.UseVisualStyleBackColor = true;
            this.m_ReverseButton.Click += new System.EventHandler(this.m_ReverseButton_Click);
            // 
            // m_IndicatorButton
            // 
            this.m_IndicatorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_IndicatorButton.Location = new System.Drawing.Point(753, 4);
            this.m_IndicatorButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_IndicatorButton.Name = "m_IndicatorButton";
            this.m_IndicatorButton.Size = new System.Drawing.Size(245, 68);
            this.m_IndicatorButton.TabIndex = 3;
            this.m_IndicatorButton.Text = "indicator (F9)";
            this.m_IndicatorButton.UseVisualStyleBackColor = true;
            this.m_IndicatorButton.Click += new System.EventHandler(this.m_IndicatorButton_Click);
            // 
            // m_TailLabel
            // 
            this.m_TailLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_TailLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TailLabel.Location = new System.Drawing.Point(3, 76);
            this.m_TailLabel.Name = "m_TailLabel";
            this.m_TailLabel.Size = new System.Drawing.Size(244, 31);
            this.m_TailLabel.TabIndex = 4;
            this.m_TailLabel.Text = "label1";
            // 
            // m_BrakeLabel
            // 
            this.m_BrakeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_BrakeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_BrakeLabel.Location = new System.Drawing.Point(253, 76);
            this.m_BrakeLabel.Name = "m_BrakeLabel";
            this.m_BrakeLabel.Size = new System.Drawing.Size(244, 31);
            this.m_BrakeLabel.TabIndex = 5;
            this.m_BrakeLabel.Text = "label2";
            // 
            // m_ReverseLabel
            // 
            this.m_ReverseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_ReverseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ReverseLabel.Location = new System.Drawing.Point(503, 76);
            this.m_ReverseLabel.Name = "m_ReverseLabel";
            this.m_ReverseLabel.Size = new System.Drawing.Size(244, 31);
            this.m_ReverseLabel.TabIndex = 6;
            this.m_ReverseLabel.Text = "label3";
            // 
            // m_IndicatorLabel
            // 
            this.m_IndicatorLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_IndicatorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_IndicatorLabel.Location = new System.Drawing.Point(753, 76);
            this.m_IndicatorLabel.Name = "m_IndicatorLabel";
            this.m_IndicatorLabel.Size = new System.Drawing.Size(245, 31);
            this.m_IndicatorLabel.TabIndex = 7;
            this.m_IndicatorLabel.Text = "label4";
            // 
            // m_IndicatorTimer
            // 
            this.m_IndicatorTimer.Interval = 500;
            this.m_IndicatorTimer.Tick += new System.EventHandler(this.m_IndicatorTimer_Tick);
            // 
            // m_RedrawTimer
            // 
            this.m_RedrawTimer.Interval = 1;
            this.m_RedrawTimer.Tick += new System.EventHandler(this.m_RedrawTimer_Tick);
            // 
            // SimulatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_TableLayoutPanel);
            this.Controls.Add(this.m_BitmapView);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SimulatorView";
            this.Size = new System.Drawing.Size(1008, 606);
            this.m_TableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BitmapView m_BitmapView;
        private System.Windows.Forms.TableLayoutPanel m_TableLayoutPanel;
        private System.Windows.Forms.Button m_TailButton;
        private System.Windows.Forms.Button m_BrakeButton;
        private System.Windows.Forms.Button m_ReverseButton;
        private System.Windows.Forms.Button m_IndicatorButton;
        private System.Windows.Forms.Label m_TailLabel;
        private System.Windows.Forms.Label m_BrakeLabel;
        private System.Windows.Forms.Label m_ReverseLabel;
        private System.Windows.Forms.Label m_IndicatorLabel;
        private System.Windows.Forms.Timer m_IndicatorTimer;
        private System.Windows.Forms.Timer m_RedrawTimer;
    }
}
