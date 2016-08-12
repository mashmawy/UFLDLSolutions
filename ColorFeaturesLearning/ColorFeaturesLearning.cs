using UFLDLSolutions;
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

namespace ColorFeaturesLearning
{
    public partial class ColorFeaturesLearning : Form
    {
        public ColorFeaturesLearning()
        {
            InitializeComponent();
        }
        List<PictureBox> features = new List<PictureBox>();
        Thread MainThread;
        SparseAutoencoder encoder = null;
        delegate void RecordEndTimeDeleget();
        private void RecordEndTime()
        { 
            EndTimeLabel.Text = DateTime.Now.ToShortTimeString();

        }
        delegate void VisualizeDeleget(double[][] theta, CostEventArg e);
        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void LearnColor_Click(object sender, EventArgs e)
        {
            if (!(
                File.Exists(Application.StartupPath + "\\data_batch_1.bin")
                ))
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Please go to : http://yann.lecun.com/exdb/mnist/ and download the following files :");
                sb.AppendLine("https://www.cs.toronto.edu/~kriz/cifar-10-binary.tar.gz");
                sb.AppendLine("extract and copy data_batch_1.bin to the application folder");
                sb.AppendLine("and make sure the files is in the application startup path (Debug folder for example)");
                MessageBox.Show(sb.ToString());
                return;

            }
            FeaturesFlowLayoutPanel.Controls.Clear();


            MainThread = new Thread(new ThreadStart(LearningColorsThread));
            features.Clear();
            int hidden = Convert.ToInt32(HiddenTextBox.Text);
            for (int i = 0; i < hidden; i++)
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
            StartTimeLabel.Text = DateTime.Now.ToShortTimeString();
            MainThread.Start();
        }
        private void LearningColorsThread()
        {


            int hidden = Convert.ToInt32(HiddenTextBox.Text);
            int visible = Convert.ToInt32(VisibileTextBox.Text);
            var d = ImagesTools.cifarTrainingDataBinary(1000);
            double[][] X;
           
            X = ImagesTools.RandomPatchsCifarTrainingData(d.Item1, visible);
              X = Matrix.Whitening(X, 0.1);
            LinearAutoencoder a = new LinearAutoencoder(hidden, visible,X);



            a.IterationDone += a_IterationDone;

            var data = a.Run();
             
            MessageBox.Show("Done");
            this.Invoke(new RecordEndTimeDeleget(RecordEndTime));
        }

        void a_IterationDone(object sender, EventArgs e)
        {
            var data = sender as double[][];


            CostEventArg ep = e as CostEventArg;

            this.Invoke(new VisualizeDeleget(VisualizeColorFeatures), data, ep);
        }
        int counter = 0;
        public void VisualizeColorFeatures(double[][] theta, CostEventArg ep)
        {
            counter++;
            IterationLabel.Text = counter.ToString();
            if (ep != null)
            {
                CostLabel.Text = (ep.Cost).ToString();

            }
            if (!checkBox1.Checked)
            {
                return;
            }


            var visual = ImagesTools.VisualizeRGBFeatures(theta, 8, 8);
            for (int i = 0; i < visual.Count; i++)
            {
                features[i].Image = visual[i];

            }

        }

        private void ColorFeaturesLearning_FormClosing(object sender, FormClosingEventArgs e)
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

    }
}
