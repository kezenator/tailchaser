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
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label2;
            this.m_Field3Text = new System.Windows.Forms.TextBox();
            this.m_Field3Label = new System.Windows.Forms.Label();
            this.m_Field2Text = new System.Windows.Forms.TextBox();
            this.m_Field2Label = new System.Windows.Forms.Label();
            this.m_Field1Text = new System.Windows.Forms.TextBox();
            this.m_Field1Label = new System.Windows.Forms.Label();
            this.m_PatternCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_NameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_IndicatorSolidCheckbox = new System.Windows.Forms.CheckBox();
            this.m_IndicatorFlashCheckbox = new System.Windows.Forms.CheckBox();
            this.m_ReverseCheckbox = new System.Windows.Forms.CheckBox();
            this.m_BrakeCheckbox = new System.Windows.Forms.CheckBox();
            this.m_TailCheckbox = new System.Windows.Forms.CheckBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.m_Field3Text);
            groupBox2.Controls.Add(this.m_Field3Label);
            groupBox2.Controls.Add(this.m_Field2Text);
            groupBox2.Controls.Add(this.m_Field2Label);
            groupBox2.Controls.Add(this.m_Field1Text);
            groupBox2.Controls.Add(this.m_Field1Label);
            groupBox2.Controls.Add(this.m_PatternCombo);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new System.Drawing.Point(191, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(172, 235);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Animation";
            // 
            // m_Field3Text
            // 
            this.m_Field3Text.Location = new System.Drawing.Point(6, 192);
            this.m_Field3Text.Name = "m_Field3Text";
            this.m_Field3Text.Size = new System.Drawing.Size(160, 25);
            this.m_Field3Text.TabIndex = 7;
            this.m_Field3Text.TextChanged += new System.EventHandler(this.m_Field3Text_TextChanged);
            // 
            // m_Field3Label
            // 
            this.m_Field3Label.AutoSize = true;
            this.m_Field3Label.Location = new System.Drawing.Point(7, 172);
            this.m_Field3Label.Name = "m_Field3Label";
            this.m_Field3Label.Size = new System.Drawing.Size(46, 17);
            this.m_Field3Label.TabIndex = 6;
            this.m_Field3Label.Text = "Field 3";
            // 
            // m_Field2Text
            // 
            this.m_Field2Text.Location = new System.Drawing.Point(6, 144);
            this.m_Field2Text.Name = "m_Field2Text";
            this.m_Field2Text.Size = new System.Drawing.Size(160, 25);
            this.m_Field2Text.TabIndex = 5;
            this.m_Field2Text.TextChanged += new System.EventHandler(this.m_Field2Text_TextChanged);
            // 
            // m_Field2Label
            // 
            this.m_Field2Label.AutoSize = true;
            this.m_Field2Label.Location = new System.Drawing.Point(7, 124);
            this.m_Field2Label.Name = "m_Field2Label";
            this.m_Field2Label.Size = new System.Drawing.Size(46, 17);
            this.m_Field2Label.TabIndex = 4;
            this.m_Field2Label.Text = "Field 2";
            // 
            // m_Field1Text
            // 
            this.m_Field1Text.Location = new System.Drawing.Point(6, 94);
            this.m_Field1Text.Name = "m_Field1Text";
            this.m_Field1Text.Size = new System.Drawing.Size(160, 25);
            this.m_Field1Text.TabIndex = 3;
            this.m_Field1Text.TextChanged += new System.EventHandler(this.m_Field1Text_TextChanged);
            // 
            // m_Field1Label
            // 
            this.m_Field1Label.AutoSize = true;
            this.m_Field1Label.Location = new System.Drawing.Point(7, 74);
            this.m_Field1Label.Name = "m_Field1Label";
            this.m_Field1Label.Size = new System.Drawing.Size(46, 17);
            this.m_Field1Label.TabIndex = 2;
            this.m_Field1Label.Text = "Field 1";
            // 
            // m_PatternCombo
            // 
            this.m_PatternCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_PatternCombo.FormattingEnabled = true;
            this.m_PatternCombo.Location = new System.Drawing.Point(6, 42);
            this.m_PatternCombo.Name = "m_PatternCombo";
            this.m_PatternCombo.Size = new System.Drawing.Size(160, 25);
            this.m_PatternCombo.TabIndex = 1;
            this.m_PatternCombo.SelectedIndexChanged += new System.EventHandler(this.m_PatternCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 22);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(49, 17);
            label2.TabIndex = 0;
            label2.Text = "Pattern";
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
            this.m_NameTextBox.Location = new System.Drawing.Point(5, 27);
            this.m_NameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_NameTextBox.Name = "m_NameTextBox";
            this.m_NameTextBox.Size = new System.Drawing.Size(180, 25);
            this.m_NameTextBox.TabIndex = 1;
            this.m_NameTextBox.TextChanged += new System.EventHandler(this.m_NameTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.m_IndicatorSolidCheckbox);
            this.groupBox1.Controls.Add(this.m_IndicatorFlashCheckbox);
            this.groupBox1.Controls.Add(this.m_ReverseCheckbox);
            this.groupBox1.Controls.Add(this.m_BrakeCheckbox);
            this.groupBox1.Controls.Add(this.m_TailCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(8, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(177, 179);
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
            this.Controls.Add(groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_NameTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(303, 244);
            this.Name = "LayerView";
            this.Size = new System.Drawing.Size(366, 244);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
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
        private System.Windows.Forms.TextBox m_Field3Text;
        private System.Windows.Forms.Label m_Field3Label;
        private System.Windows.Forms.TextBox m_Field2Text;
        private System.Windows.Forms.Label m_Field2Label;
        private System.Windows.Forms.TextBox m_Field1Text;
        private System.Windows.Forms.Label m_Field1Label;
        private System.Windows.Forms.ComboBox m_PatternCombo;
    }
}
