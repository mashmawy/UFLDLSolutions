namespace ColorFeaturesLearning
{
    partial class ColorFeaturesLearning
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
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.FeaturesFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.EndTimeLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.StartTimeLabel = new System.Windows.Forms.Label();
            this.CostLabel = new System.Windows.Forms.Label();
            this.IterationLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HiddenTextBox = new System.Windows.Forms.TextBox();
            this.VisibileTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.LearnColor = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(247, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "Start Time:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.FeaturesFlowLayoutPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(651, 391);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Learned Features";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FeaturesFlowLayoutPanel
            // 
            this.FeaturesFlowLayoutPanel.AutoScroll = true;
            this.FeaturesFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeaturesFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.FeaturesFlowLayoutPanel.Name = "FeaturesFlowLayoutPanel";
            this.FeaturesFlowLayoutPanel.Size = new System.Drawing.Size(645, 385);
            this.FeaturesFlowLayoutPanel.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "End Time :";
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(335, 61);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.EndTimeLabel.TabIndex = 72;
            this.EndTimeLabel.Text = "00:00";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(401, 60);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(255, 17);
            this.checkBox1.TabIndex = 70;
            this.checkBox1.Text = "Display Feature (Uncheck to boost performance)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // StartTimeLabel
            // 
            this.StartTimeLabel.AutoSize = true;
            this.StartTimeLabel.Location = new System.Drawing.Point(335, 44);
            this.StartTimeLabel.Name = "StartTimeLabel";
            this.StartTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.StartTimeLabel.TabIndex = 68;
            this.StartTimeLabel.Text = "00:00";
            // 
            // CostLabel
            // 
            this.CostLabel.AutoSize = true;
            this.CostLabel.Location = new System.Drawing.Point(100, 61);
            this.CostLabel.Name = "CostLabel";
            this.CostLabel.Size = new System.Drawing.Size(13, 13);
            this.CostLabel.TabIndex = 67;
            this.CostLabel.Text = "0";
            // 
            // IterationLabel
            // 
            this.IterationLabel.AutoSize = true;
            this.IterationLabel.Location = new System.Drawing.Point(100, 47);
            this.IterationLabel.Name = "IterationLabel";
            this.IterationLabel.Size = new System.Drawing.Size(13, 13);
            this.IterationLabel.TabIndex = 66;
            this.IterationLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 65;
            this.label8.Text = "Iteration :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Visible Layer Size :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Hidden Layer Size :";
            // 
            // HiddenTextBox
            // 
            this.HiddenTextBox.Location = new System.Drawing.Point(115, 5);
            this.HiddenTextBox.Name = "HiddenTextBox";
            this.HiddenTextBox.Size = new System.Drawing.Size(43, 20);
            this.HiddenTextBox.TabIndex = 61;
            this.HiddenTextBox.Text = "100";
            // 
            // VisibileTextBox
            // 
            this.VisibileTextBox.Location = new System.Drawing.Point(279, 6);
            this.VisibileTextBox.Name = "VisibileTextBox";
            this.VisibileTextBox.Size = new System.Drawing.Size(43, 20);
            this.VisibileTextBox.TabIndex = 60;
            this.VisibileTextBox.Text = "192";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "Cost :";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 80);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(659, 417);
            this.tabControl1.TabIndex = 59;
            // 
            // LearnColor
            // 
            this.LearnColor.Location = new System.Drawing.Point(401, 12);
            this.LearnColor.Name = "LearnColor";
            this.LearnColor.Size = new System.Drawing.Size(153, 23);
            this.LearnColor.TabIndex = 76;
            this.LearnColor.Text = "Learn Color Features";
            this.LearnColor.UseVisualStyleBackColor = true;
            this.LearnColor.Click += new System.EventHandler(this.LearnColor_Click);
            // 
            // ColorFeaturesLearning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 509);
            this.Controls.Add(this.LearnColor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EndTimeLabel);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.StartTimeLabel);
            this.Controls.Add(this.CostLabel);
            this.Controls.Add(this.IterationLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HiddenTextBox);
            this.Controls.Add(this.VisibileTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tabControl1);
            this.Name = "ColorFeaturesLearning";
            this.Text = "Color Features Learning";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ColorFeaturesLearning_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel FeaturesFlowLayoutPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label EndTimeLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label StartTimeLabel;
        private System.Windows.Forms.Label CostLabel;
        private System.Windows.Forms.Label IterationLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HiddenTextBox;
        private System.Windows.Forms.TextBox VisibileTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button LearnColor;
    }
}

