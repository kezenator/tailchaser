namespace Com.TailChaser.Editor.UI.Controls
{
    partial class SchemeView
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
            this.label2 = new System.Windows.Forms.Label();
            this.m_DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_AddLayerButton = new System.Windows.Forms.Button();
            this.m_LayerListView = new Com.TailChaser.Editor.UI.Controls.LayerListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // m_NameTextBox
            // 
            this.m_NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_NameTextBox.Location = new System.Drawing.Point(3, 21);
            this.m_NameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_NameTextBox.Name = "m_NameTextBox";
            this.m_NameTextBox.Size = new System.Drawing.Size(518, 25);
            this.m_NameTextBox.TabIndex = 1;
            this.m_NameTextBox.TextChanged += new System.EventHandler(this.m_NameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            // 
            // m_DescriptionTextBox
            // 
            this.m_DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_DescriptionTextBox.Location = new System.Drawing.Point(3, 70);
            this.m_DescriptionTextBox.Name = "m_DescriptionTextBox";
            this.m_DescriptionTextBox.Size = new System.Drawing.Size(518, 25);
            this.m_DescriptionTextBox.TabIndex = 3;
            this.m_DescriptionTextBox.TextChanged += new System.EventHandler(this.m_DescriptionTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Layers";
            // 
            // m_AddLayerButton
            // 
            this.m_AddLayerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_AddLayerButton.Location = new System.Drawing.Point(3, 538);
            this.m_AddLayerButton.Name = "m_AddLayerButton";
            this.m_AddLayerButton.Size = new System.Drawing.Size(100, 27);
            this.m_AddLayerButton.TabIndex = 6;
            this.m_AddLayerButton.Text = "Add Layer";
            this.m_AddLayerButton.UseVisualStyleBackColor = true;
            this.m_AddLayerButton.Click += new System.EventHandler(this.m_AddLayerButton_Click);
            // 
            // m_LayerListView
            // 
            this.m_LayerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_LayerListView.LayerView = null;
            this.m_LayerListView.Location = new System.Drawing.Point(3, 123);
            this.m_LayerListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.m_LayerListView.Name = "m_LayerListView";
            this.m_LayerListView.Scheme = null;
            this.m_LayerListView.Size = new System.Drawing.Size(518, 408);
            this.m_LayerListView.TabIndex = 5;
            // 
            // SchemeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_AddLayerButton);
            this.Controls.Add(this.m_LayerListView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_DescriptionTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_NameTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SchemeView";
            this.Size = new System.Drawing.Size(524, 568);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_NameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_DescriptionTextBox;
        private System.Windows.Forms.Label label3;
        private LayerListView m_LayerListView;
        private System.Windows.Forms.Button m_AddLayerButton;
    }
}
