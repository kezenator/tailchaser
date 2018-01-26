namespace Com.TailChaser.Editor.UI.Controls
{
    partial class LayerView
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_NameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_IndicatorSolidCheckbox = new System.Windows.Forms.CheckBox();
            this.m_IndicatorFlashCheckbox = new System.Windows.Forms.CheckBox();
            this.m_ReverseCheckbox = new System.Windows.Forms.CheckBox();
            this.m_BrakeCheckbox = new System.Windows.Forms.CheckBox();
            this.m_TailCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // m_NameTextBox
            // 
            this.m_NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_NameTextBox.Location = new System.Drawing.Point(5, 27);
            this.m_NameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_NameTextBox.Name = "m_NameTextBox";
            this.m_NameTextBox.Size = new System.Drawing.Size(295, 25);
            this.m_NameTextBox.TabIndex = 1;
            this.m_NameTextBox.TextChanged += new System.EventHandler(this.m_NameTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.m_IndicatorSolidCheckbox);
            this.groupBox1.Controls.Add(this.m_IndicatorFlashCheckbox);
            this.groupBox1.Controls.Add(this.m_ReverseCheckbox);
            this.groupBox1.Controls.Add(this.m_BrakeCheckbox);
            this.groupBox1.Controls.Add(this.m_TailCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(8, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(292, 179);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enabled By Signals";
            // 
            // m_IndicatorSolidCheckbox
            // 
            this.m_IndicatorSolidCheckbox.AutoSize = true;
            this.m_IndicatorSolidCheckbox.Location = new System.Drawing.Point(8, 150);
            this.m_IndicatorSolidCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_IndicatorSolidCheckbox.Name = "m_IndicatorSolidCheckbox";
            this.m_IndicatorSolidCheckbox.Size = new System.Drawing.Size(119, 21);
            this.m_IndicatorSolidCheckbox.TabIndex = 4;
            this.m_IndicatorSolidCheckbox.Text = "Indicator (Solid)";
            this.m_IndicatorSolidCheckbox.ThreeState = true;
            this.m_IndicatorSolidCheckbox.UseVisualStyleBackColor = true;
            this.m_IndicatorSolidCheckbox.CheckStateChanged += new System.EventHandler(this.m_IndicatorSolidCheckbox_CheckStateChanged);
            // 
            // m_IndicatorFlashCheckbox
            // 
            this.m_IndicatorFlashCheckbox.AutoSize = true;
            this.m_IndicatorFlashCheckbox.Location = new System.Drawing.Point(7, 119);
            this.m_IndicatorFlashCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_IndicatorFlashCheckbox.Name = "m_IndicatorFlashCheckbox";
            this.m_IndicatorFlashCheckbox.Size = new System.Drawing.Size(119, 21);
            this.m_IndicatorFlashCheckbox.TabIndex = 3;
            this.m_IndicatorFlashCheckbox.Text = "Indicator (Flash)";
            this.m_IndicatorFlashCheckbox.ThreeState = true;
            this.m_IndicatorFlashCheckbox.UseVisualStyleBackColor = true;
            this.m_IndicatorFlashCheckbox.CheckStateChanged += new System.EventHandler(this.m_IndicatorFlashCheckbox_CheckStateChanged);
            // 
            // m_ReverseCheckbox
            // 
            this.m_ReverseCheckbox.AutoSize = true;
            this.m_ReverseCheckbox.Location = new System.Drawing.Point(7, 88);
            this.m_ReverseCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_ReverseCheckbox.Name = "m_ReverseCheckbox";
            this.m_ReverseCheckbox.Size = new System.Drawing.Size(73, 21);
            this.m_ReverseCheckbox.TabIndex = 2;
            this.m_ReverseCheckbox.Text = "Reverse";
            this.m_ReverseCheckbox.ThreeState = true;
            this.m_ReverseCheckbox.UseVisualStyleBackColor = true;
            this.m_ReverseCheckbox.CheckStateChanged += new System.EventHandler(this.m_ReverseCheckbox_CheckStateChanged);
            // 
            // m_BrakeCheckbox
            // 
            this.m_BrakeCheckbox.AutoSize = true;
            this.m_BrakeCheckbox.Location = new System.Drawing.Point(7, 56);
            this.m_BrakeCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_BrakeCheckbox.Name = "m_BrakeCheckbox";
            this.m_BrakeCheckbox.Size = new System.Drawing.Size(59, 21);
            this.m_BrakeCheckbox.TabIndex = 1;
            this.m_BrakeCheckbox.Text = "Brake";
            this.m_BrakeCheckbox.ThreeState = true;
            this.m_BrakeCheckbox.UseVisualStyleBackColor = true;
            this.m_BrakeCheckbox.CheckStateChanged += new System.EventHandler(this.m_BrakeCheckbox_CheckStateChanged);
            // 
            // m_TailCheckbox
            // 
            this.m_TailCheckbox.AutoSize = true;
            this.m_TailCheckbox.Location = new System.Drawing.Point(7, 25);
            this.m_TailCheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_TailCheckbox.Name = "m_TailCheckbox";
            this.m_TailCheckbox.Size = new System.Drawing.Size(46, 21);
            this.m_TailCheckbox.TabIndex = 0;
            this.m_TailCheckbox.Text = "Tail";
            this.m_TailCheckbox.ThreeState = true;
            this.m_TailCheckbox.UseVisualStyleBackColor = true;
            this.m_TailCheckbox.CheckStateChanged += new System.EventHandler(this.m_TailCheckbox_CheckStateChanged);
            // 
            // LayerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_NameTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(303, 244);
            this.Name = "LayerView";
            this.Size = new System.Drawing.Size(303, 244);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_NameTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox m_IndicatorSolidCheckbox;
        private System.Windows.Forms.CheckBox m_IndicatorFlashCheckbox;
        private System.Windows.Forms.CheckBox m_ReverseCheckbox;
        private System.Windows.Forms.CheckBox m_BrakeCheckbox;
        private System.Windows.Forms.CheckBox m_TailCheckbox;
    }
}
