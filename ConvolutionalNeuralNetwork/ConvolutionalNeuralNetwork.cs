using UFLDLSolutions;
using UFLDLSolutions.Core;
using UFLDLSolutions.Core.Tools;
using UFLDLSolutions.MathHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvolutionalNeuralNetwork
{
    public partial class ConvolutionalNeuralNetwork : Form
    {
        public ConvolutionalNeuralNetwork()
        {
            InitializeComponent();
        }

        List<PictureBox> features = new List<PictureBox>();
        Thread MainThread; 
         
        delegate void VisualizeDeleget(List<Bitmap> images, CostAndAcc e);

        int counter = 0;

        private void TestCNNBtn_Click(object sender, EventArgs e)
        {
            if (!(
             File.Exists("train-images.idx3-ubyte") &&
             File.Exists("train-labels.idx1-ubyte") &&
             File.Exists("t10k-images.idx3-ubyte") &&
             File.Exists("t10k-labels.idx1-ubyte")
             ))
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Please go to : http://yann.lecun.com/exdb/mnist/ and download the following files :");
                sb.AppendLine("train-images.idx3-ubyte");
                sb.AppendLine("train-labels.idx1-ubyte");
                sb.AppendLine("t10k-images.idx3-ubyte");
                sb.AppendLine("t10k-labels.idx1-ubyte");
                sb.AppendLine("and make sure the files is in the application startup path (Debug folder for example)");

                MessageBox.Show(sb.ToString());
                return;

            }
            IterationLabel.Text = "0";
            CostLabel.Text = "0";
            counter = 0;
            StartTimeLabel.Text = DateTime.Now.ToShortTimeString();
            for (int i = 0; i < 10; i++)
            { 
                PictureBox box = new PictureBox();
                box.Height = 50;
                box.Width = 50;
                box.SizeMode = PictureBoxSizeMode.StretchImage;
                FeaturesFlowLayoutPanel.Controls.Add(box);

                box.BackColor = Color.Black;
                FeaturesFlowLayoutPanel.VerticalScroll.Enabled = true;
                features.Add(box);
            }
            MainThread = new Thread(new ThreadStart(CnnThread));
            MainThread.Start();
        }
        private void CnnThread()
        {
            ConvolutionNetwork cn = new ConvolutionNetwork();
            cn.IterationDone += cn_IterationDone;
            cn.Run();
        }

        void cn_IterationDone(object sender, EventArgs e)
        {

            CostAndAcc ep = e as CostAndAcc;
            List<double[][]> d = sender as List<double[][]>;
            List<Bitmap> images = new List<Bitmap>();
            double[][] all = Matrix.MatrixCreate(10, 5 * 5);
            int r = 0;
            foreach (var item in d)
            {
                int c = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        all[r][c] = item[i][j];
                        c++;
                    }

                }
                r++;
            }
            images.AddRange(ImagesTools.VisualizeGrayScaleFeatures(all));

            this.Invoke(new VisualizeDeleget(VisualizeCNN), images, ep);
             
        }
        public void VisualizeCNN(List<Bitmap> data, CostAndAcc ep)
        {
            counter++;
            IterationLabel.Text = counter.ToString();

            EndTimeLabel.Text = DateTime.Now.ToShortTimeString();
            if (ep != null)
            {
                CostLabel.Text = (ep.Cost).ToString();
                AccuracyLabel.Text = (ep.Acc).ToString();
            }
            if (!checkBox1.Checked)
            {
                return;
            }

            for (int i = 0; i < data.Count; i++)
            {
                features[i].Image = data[i];

            }



        }

        private void ConvolutionalNeuralNetwork_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainThread != null)
            {
                if (MainThread.IsAlive)
                {
                    try
                    {

                        MainThread.Abort();
                    }
                    catch
                    {

                    }

                }
            }
        }

        private void ConvolutionalNeuralNetwork_Load(object sender, EventArgs e)
        {

        }
    }
}
