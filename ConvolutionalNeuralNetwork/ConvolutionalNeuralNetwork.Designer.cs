namespace ConvolutionalNeuralNetwork
{
    partial class ConvolutionalNeuralNetwork
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
            this.FeaturesFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.EndTimeLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.StartTimeLabel = new System.Windows.Forms.Label();
            this.CostLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.IterationLabel = new System.Windows.Forms.Label();
            this.TestCNNBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AccuracyLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // FeaturesFlowLayoutPanel
            // 
            this.FeaturesFlowLayoutPanel.AutoScroll = true;
            this.FeaturesFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FeaturesFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.FeaturesFlowLayoutPanel.Name = "FeaturesFlowLayoutPanel";
            this.FeaturesFlowLayoutPanel.Size = new System.Drawing.Size(775, 382);
            this.FeaturesFlowLayoutPanel.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(345, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 89;
            this.label6.Text = "Start Time:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "End Time :";
            // 
            // EndTimeLabel
            // 
            this.EndTimeLabel.AutoSize = true;
            this.EndTimeLabel.Location = new System.Drawing.Point(433, 26);
            this.EndTimeLabel.Name = "EndTimeLabel";
            this.EndTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.EndTimeLabel.TabIndex = 88;
            this.EndTimeLabel.Text = "00:00";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(531, 54);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(255, 17);
            this.checkBox1.TabIndex = 87;
            this.checkBox1.Text = "Display Feature (Uncheck to boost performance)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // StartTimeLabel
            // 
            this.StartTimeLabel.AutoSize = true;
            this.StartTimeLabel.Location = new System.Drawing.Point(433, 9);
            this.StartTimeLabel.Name = "StartTimeLabel";
            this.StartTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.StartTimeLabel.TabIndex = 86;
            this.StartTimeLabel.Text = "00:00";
            // 
            // CostLabel
            // 
            this.CostLabel.AutoSize = true;
            this.CostLabel.Location = new System.Drawing.Point(205, 26);
            this.CostLabel.Name = "CostLabel";
            this.CostLabel.Size = new System.Drawing.Size(13, 13);
            this.CostLabel.TabIndex = 85;
            this.CostLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(117, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 83;
            this.label8.Text = "Iteration :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(117, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "Cost :";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 57);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(789, 414);
            this.tabControl1.TabIndex = 77;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.FeaturesFlowLayoutPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(781, 388);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Learned Features";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // IterationLabel
            // 
            this.IterationLabel.AutoSize = true;
            this.IterationLabel.Location = new System.Drawing.Point(205, 12);
            this.IterationLabel.Name = "IterationLabel";
            this.IterationLabel.Size = new System.Drawing.Size(13, 13);
            this.IterationLabel.TabIndex = 84;
            this.IterationLabel.Text = "0";
            // 
            // TestCNNBtn
            // 
            this.TestCNNBtn.Location = new System.Drawing.Point(11, 12);
            this.TestCNNBtn.Name = "TestCNNBtn";
            this.TestCNNBtn.Size = new System.Drawing.Size(100, 23);
            this.TestCNNBtn.TabIndex = 91;
            this.TestCNNBtn.Text = "Run CNN";
            this.TestCNNBtn.UseVisualStyleBackColor = true;
            this.TestCNNBtn.Click += new System.EventHandler(this.TestCNNBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(496, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Training Data Accuracy :";
            // 
            // AccuracyLabel
            // 
            this.AccuracyLabel.AutoSize = true;
            this.AccuracyLabel.Location = new System.Drawing.Point(627, 9);
            this.AccuracyLabel.Name = "AccuracyLabel";
            this.AccuracyLabel.Size = new System.Drawing.Size(13, 13);
            this.AccuracyLabel.TabIndex = 93;
            this.AccuracyLabel.Text = "0";
            // 
            // ConvolutionalNeuralNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 473);
            this.Controls.Add(this.AccuracyLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TestCNNBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EndTimeLabel);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.StartTimeLabel);
            this.Controls.Add(this.CostLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.IterationLabel);
            this.Name = "ConvolutionalNeuralNetwork";
            this.Text = "Convolutional Neural Network";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConvolutionalNeuralNetwork_FormClosing);
            this.Load += new System.EventHandler(this.ConvolutionalNeuralNetwork_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FeaturesFlowLayoutPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label EndTimeLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label StartTimeLabel;
        private System.Windows.Forms.Label CostLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label IterationLabel;
        private System.Windows.Forms.Button TestCNNBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AccuracyLabel;
    }
}

