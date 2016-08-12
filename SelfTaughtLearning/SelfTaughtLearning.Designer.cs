namespace SelfTaughtLearning
{
    partial class SelfTaughtLearning
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
            this.EndTimeLabel = new System.Windows.Forms.Label();
            this.RunSoftMax = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.LearnButton = new System.Windows.Forms.Button();
            this.StartTimeLabel = new System.Windows.Forms.Label();
            this.CostLabel = new System.Windows.Forms.Label();
            this.IterationLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HiddenTextBox = new System.Windows.Forms.TextBox();
            this.VisibileTextBox = new System.Windows.Forms.TextBox();
            this.FeaturesFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.StopAndSaveButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(292, 65);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.EndTimeLabel.TabIndex = 55;
            this.EndTimeLabel.Text = "00:00";
            // 
            // RunSoftMax
            // 
            this.RunSoftMax.Enabled = false;
            this.RunSoftMax.Location = new System.Drawing.Point(681, 13);
            this.RunSoftMax.Name = "RunSoftMax";
            this.RunSoftMax.Size = new System.Drawing.Size(138, 23);
            this.RunSoftMax.TabIndex = 52;
            this.RunSoftMax.Text = "Run Softmax";
            this.RunSoftMax.UseVisualStyleBackColor = true;
            this.RunSoftMax.Click += new System.EventHandler(this.RunSoftMax_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(564, 61);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(255, 17);
            this.checkBox1.TabIndex = 49;
            this.checkBox1.Text = "Display Feature (Uncheck to boost performance)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // LearnButton
            // 
            this.LearnButton.Location = new System.Drawing.Point(360, 12);
            this.LearnButton.Name = "LearnButton";
            this.LearnButton.Size = new System.Drawing.Size(138, 23);
            this.LearnButton.TabIndex = 48;
            this.LearnButton.Text = "Learn MNIST Features";
            this.LearnButton.UseVisualStyleBackColor = true;
            this.LearnButton.Click += new System.EventHandler(this.LearnButton_Click);
            // 
            // StartTimeLabel
            // 
            this.StartTimeLabel.AutoSize = true;
            this.StartTimeLabel.Location = new System.Drawing.Point(292, 48);
            this.StartTimeLabel.Name = "StartTimeLabel";
            this.StartTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.StartTimeLabel.TabIndex = 47;
            this.StartTimeLabel.Text = "00:00";
            // 
            // CostLabel
            // 
            this.CostLabel.AutoSize = true;
            this.CostLabel.Location = new System.Drawing.Point(108, 65);
            this.CostLabel.Name = "CostLabel";
            this.CostLabel.Size = new System.Drawing.Size(13, 13);
            this.CostLabel.TabIndex = 45;
            this.CostLabel.Text = "0";
            // 
            // IterationLabel
            // 
            this.IterationLabel.AutoSize = true;
            this.IterationLabel.Location = new System.Drawing.Point(108, 51);
            this.IterationLabel.Name = "IterationLabel";
            this.IterationLabel.Size = new System.Drawing.Size(13, 13);
            this.IterationLabel.TabIndex = 44;
            this.IterationLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 43;
            this.label8.Text = "Iteration :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Visible Layer Size :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Hidden Layer Size :";
            // 
            // HiddenTextBox
            // 
            this.HiddenTextBox.Location = new System.Drawing.Point(123, 9);
            this.HiddenTextBox.Name = "HiddenTextBox";
            this.HiddenTextBox.Size = new System.Drawing.Size(43, 20);
            this.HiddenTextBox.TabIndex = 39;
            this.HiddenTextBox.Text = "196";
            // 
            // VisibileTextBox
            // 
            this.VisibileTextBox.Location = new System.Drawing.Point(287, 10);
            this.VisibileTextBox.Name = "VisibileTextBox";
            this.VisibileTextBox.Size = new System.Drawing.Size(43, 20);
            this.VisibileTextBox.TabIndex = 38;
            this.VisibileTextBox.Text = "784";
            // 
            // FeaturesFlowLayoutPanel
            // 
            this.FeaturesFlowLayoutPanel.AutoScroll = true;
            this.FeaturesFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeaturesFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.FeaturesFlowLayoutPanel.Name = "FeaturesFlowLayoutPanel";
            this.FeaturesFlowLayoutPanel.Size = new System.Drawing.Size(790, 303);
            this.FeaturesFlowLayoutPanel.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.FeaturesFlowLayoutPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(796, 309);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Learned Features";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Cost :";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 84);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(804, 335);
            this.tabControl1.TabIndex = 37;
            // 
            // StopAndSaveButton
            // 
            this.StopAndSaveButton.Enabled = false;
            this.StopAndSaveButton.Location = new System.Drawing.Point(513, 13);
            this.StopAndSaveButton.Name = "StopAndSaveButton";
            this.StopAndSaveButton.Size = new System.Drawing.Size(162, 23);
            this.StopAndSaveButton.TabIndex = 56;
            this.StopAndSaveButton.Text = "Stop and save features";
            this.StopAndSaveButton.UseVisualStyleBackColor = true;
            this.StopAndSaveButton.Click += new System.EventHandler(this.StopAndSaveButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "End Time :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Start Time:";
            // 
            // SelfTaughtLearning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 431);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.StopAndSaveButton);
            this.Controls.Add(this.EndTimeLabel);
            this.Controls.Add(this.RunSoftMax);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.LearnButton);
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
            this.Name = "SelfTaughtLearning";
            this.Text = "Self Taught Learning";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWIndow_FormClosing);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label EndTimeLabel;
        private System.Windows.Forms.Button RunSoftMax;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button LearnButton;
        private System.Windows.Forms.Label StartTimeLabel;
        private System.Windows.Forms.Label CostLabel;
        private System.Windows.Forms.Label IterationLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HiddenTextBox;
        private System.Windows.Forms.TextBox VisibileTextBox;
        private System.Windows.Forms.FlowLayoutPanel FeaturesFlowLayoutPanel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button StopAndSaveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}

