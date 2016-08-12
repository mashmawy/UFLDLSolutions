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
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SelfTaughtLearning
{
    public partial class SelfTaughtLearning : Form
    {
        public SelfTaughtLearning()
        {
            InitializeComponent();
        }
        List<PictureBox> features = new List<PictureBox>();
        Thread mainThread;
        SparseAutoencoder encoder = null;

        delegate void VisualizeDeleget(double[][] theta, CostEventArg e);
        delegate void EnableSaveDeleget();
        private void EnableSave()
        {
            this.StopAndSaveButton.Enabled = true;
            
            EndTimeLabel.Text = DateTime.Now.ToShortTimeString();
          
        }
        private void LearnButton_Click(object sender, EventArgs e)
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


         



            FeaturesFlowLayoutPanel.Controls.Clear();


            mainThread = new Thread(new ThreadStart(LearningThread));
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
            mainThread.Start();
            StopAndSaveButton.Enabled = true;
        }

        private void LearningThread()
        {
            int hidden = Convert.ToInt32(HiddenTextBox.Text);
            int visible = Convert.ToInt32(VisibileTextBox.Text);
            var data = ImagesTools.MnistTrainingData();
            double[][] X = data.Item1;
            double[] Y = data.Item2;
            SparseAutoencoder a = new SparseAutoencoder(hidden, visible, X);
            encoder = a;
             
            a.IterationDone += Autoencoder_IterationDone;

            var result = a.Run();
            this.Invoke(new EnableSaveDeleget(EnableSave));
            MessageBox.Show("Done");
        }

        private void Autoencoder_IterationDone(object sender, EventArgs e)
        {
            var data = sender as double[][];


            CostEventArg ep = e as CostEventArg;

            this.Invoke(new VisualizeDeleget(VisualizeFeatures), data, ep);

        }
        int counter = 0;
        public void VisualizeFeatures(double[][] theta, CostEventArg ep)
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


            var visual = ImagesTools.VisualizeGrayScaleFeatures(theta);
            for (int i = 0; i < visual.Count; i++)
            {
                features[i].Image = visual[i];

            }

        }

        private void StopAndSaveButton_Click(object sender, EventArgs e)
        {
            mainThread.Abort();
            var fs = File.Create(Application.StartupPath + "\\MNISTFeatures.dat");
            AutoEncoderData toSave = new AutoEncoderData();

            toSave.b1 = encoder.b1;
            toSave.b1cash = encoder.b1cash;
            toSave.b2 = encoder.b2;
            toSave.b2cash = encoder.b2cash;
            toSave.BatchNumber = encoder.BatchNumber;
            toSave.Beta = encoder.Beta;

            toSave.HiddenCount = encoder.HiddenCount;
            toSave.Iteration = encoder.Iteration;
            toSave.Lambda = encoder.Lambda;
            toSave.LearningRate = encoder.LearningRate;
            toSave.Momentum = encoder.Momentum;
            toSave.Rho = encoder.Rho;
            toSave.TiedWeight = encoder.TiedWeight;
            toSave.VisibleCount = encoder.VisibleCount;
            toSave.W1 = encoder.W1;
            toSave.W1cash = encoder.W1cash;
            toSave.w2 = encoder.W2;
            toSave.W2cash = encoder.W2cash;
            toSave.Cost = encoder.Cost;


            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, toSave);
            fs.Close();
            this.StopAndSaveButton.Enabled = false;
            this.RunSoftMax.Enabled = true;
            toSave = null;
            encoder = null;
            GC.Collect();
        }

        private void RunSoftMax_Click(object sender, EventArgs e)
        {
            
            if (!File.Exists(Application.StartupPath + "\\MNISTFeatures.dat"))
            {
                MessageBox.Show("Please Extract the features first.");

                return;
            }
            this.LearnButton.Enabled = false;
            IterationLabel.Text = "0";
            CostLabel.Text = "0";
            counter = 0;
            mainThread = new Thread(new ThreadStart(SoftMaxThread));
            mainThread.Start();
        }

        private void SoftMaxThread()
        {
            var fs = File.Open(Application.StartupPath + "\\MNISTFeatures.dat", FileMode.Open, FileAccess.Read);
            AutoEncoderData lf;

            BinaryFormatter bf = new BinaryFormatter();
            lf = bf.Deserialize(fs) as AutoEncoderData;
            fs.Close();

            var input = ImagesTools.MnistTrainingData();
            var tesInput = ImagesTools.MnistTestData();

            double[][] X = input.Item1;

            double[][] Xtest = tesInput.Item1;
            double[] Y = input.Item2;
            double[] Ytest = tesInput.Item2;

            int input_size = 196;
            int classes = 10;
            double lambda = 1e-4;

            X = SparseAutoencoder.ExtractFeatures(X, lf);
            Xtest = SparseAutoencoder.ExtractFeatures(Xtest, lf);


            Softmax s = new Softmax();
            s.IterationDone += s_IterationDone;
            var start = DateTime.Now;
            var d = s.Run(classes, input_size, lambda, X, Y);
            var p = s.softmax_predict(d, Xtest);
            double[] yD = new double[10000];
            for (int i = 0; i < 10000; i++)
            {
                var col = p.GetColumn(i);
                var ma = col.Max();
                var ind = col.ToList().IndexOf(ma);
                yD[i] = ind;


            }

    

            double[] bo = new double[10000];

            for (int i = 0; i < 10000; i++)
            {
                if (yD[i] == Ytest[i])
                {
                    bo[i] = 1;
                }
            }
            var acc = bo.Sum() * 100 / 10000;
            var end = DateTime.Now;
            MessageBox.Show("Accuracy : " + acc.ToString() + "% ");


            SoftmaxModel model = new SoftmaxModel();
            model.Model = d;
            var fs2 = File.Create(Application.StartupPath + "\\mnistsoftmax.dat");

            BinaryFormatter bf2 = new BinaryFormatter();
            bf2.Serialize(fs2, model);
            fs2.Close();
            EndTimeLabel.Text = DateTime.Now.ToShortTimeString();
        }

        void s_IterationDone(object sender, EventArgs e)
        {
            var data = sender as double[][];


            CostEventArg ep = e as CostEventArg;

            this.Invoke(new VisualizeDeleget(VisualizeSoftmaxCost), data, ep);
            //CostEventArg
        }
        public void VisualizeSoftmaxCost(double[][] theta, CostEventArg ep)
        {
            counter++;
            IterationLabel.Text = counter.ToString();
            if (ep != null)
            {
                CostLabel.Text = (ep.Cost).ToString();

            }




        }

        private void MainWIndow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainThread!=null)
            {
                if (mainThread.IsAlive)
                {
                    try
                    {

                    mainThread.Abort();
                    }
                    catch
                    {
                         
                    }
                
                }
            }
        }


    }
}
