using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using UFLDLSolutions.MathHelper;
namespace UFLDLSolutions
{
    [Serializable]
    public class SparseAutoencoder
    {
        public bool TiedWeight { get; set; }
         
        public event EventHandler IterationDone;
        public int HiddenCount { get; set; }
        public int VisibleCount { get; set; }
        public int Iteration { get; set; }
        public int BatchNumber { get; set; }
        public double[] b1 { get; set; }
        public double[] b2 { get; set; }
        public double[][] Features { get; set; }
        public double[][] W1 { get; set; }
        private double[][] w2;
        public double[][] W2 
        {
            get
            {
                if (TiedWeight)
                {
                    return W1.Transpose();
                }
                else
                {
                    return w2;
                }
            }
            set {
                if (TiedWeight)
                {
                    W1= value.Transpose();
                }
                else
                {
                     w2=value;
                }
            }
        }
        public double[] b1cash { get; set; }
        public double[] b2cash { get; set; }
        public double[][] W1cash { get; set; }
        public double[][] W2cash { get; set; }
        public double[][] Data { get; set; }
        public double LearningRate { get; set; }
        public double Momentum { get; set; }
        public double Lambda { get; set; }
        public double Beta { get; set; }
        public double Cost { get; set; }
        public double[] Rho { get; set; }


        [NonSerialized]
        Random randomGenerator;

        public Random RandomGenerator
        {
            get { return randomGenerator; }
            set { randomGenerator = value; }
        }

        public  SparseAutoencoder(int hidden, int visible, double[][] data, bool tiedWeight=false, double lambda = 3e-3,
          double sparsity_param = .1, double beta = 2, int batchNumber = 200,
          double learningRate = 0.09, int iteration = 1500, double momentum = 0.5)
        {
           
            Momentum = momentum;
            Iteration = iteration;
            Lambda = lambda;
            LearningRate = learningRate;
            Beta = beta;
            Data = data;
            BatchNumber = batchNumber;
            TiedWeight = tiedWeight;
            RandomGenerator = new Random();
            HiddenCount = hidden;
            VisibleCount = visible;
            Rho = new double[hidden];
            Rho = Rho.Add(sparsity_param);
         //   this.TiedWeight = true;
            b1cash = new double[hidden];
            b2cash = new double[visible];

            W1cash = Matrix.MatrixCreate(hidden, visible);
            W2cash = Matrix.MatrixCreate(visible, hidden);

            //Initialize the weights
            b1 = new double[hidden];
            b2 = new double[visible];

            var r = Math.Sqrt(6) / Math.Sqrt(hidden + visible + 1);

            W1 = Matrix.Random(hidden, visible, -r, r).ToArray();
            if (!TiedWeight)
            {

                W2 = Matrix.Random(visible, hidden, -r, r).ToArray();
            }
            LearningRate = learningRate;
           
        }

        /// <summary>
        /// Run the sparse autoencoder and return the learned features
        /// </summary> 
        /// <returns> learned features matrix</returns>
        public double[][] Run()
        {


            for (int epic = 0; epic < Iteration; epic++)
            {


                double[][] batchData = null;

                int[] indexis = new int[BatchNumber];
                for (int i = 0; i < BatchNumber; i++)
                {
                    int ind = RandomGenerator.Next(0, Data[0].Length);
                    indexis[i] = ind;

                }
                batchData = Data.GetColumns(indexis);
                
            //    W2 = W1.Transpose();
                var batchgrades = AutoencoderGradeAndCost(W1, W2, b1, b2, batchData, Lambda, Beta, Rho);

                double cost = batchgrades.Item5;





                W1cash = W1cash.Multiply(Momentum).Subtract(batchgrades.Item1.Multiply(LearningRate));
                W2cash = W2cash.Multiply(Momentum).Subtract(batchgrades.Item2.Multiply(LearningRate));
                b1cash = b1cash.Multiply(Momentum).Subtract(batchgrades.Item3.Multiply(LearningRate));
                b2cash = b2cash.Multiply(Momentum).Subtract(batchgrades.Item4.Multiply(LearningRate));

                if (cost <30.0)
                {
                    Momentum = 0.99;
                }
                if (cost < 4.0)
                {
                    Momentum = 0.9999;
                }
                //v = mu * v - learning_rate * dx # integrate velocity

                W2 = W2.Add(W2cash);
                W1 = W1.Add(W1cash);
                b1 = b1.Add(b1cash);
                b2 = b2.Add(b2cash);
                if (IterationDone != null)
                {
                    this.Features = W1;
                    this.Cost = cost;
                    IterationDone(W1, new CostEventArg() { Cost = cost });
                }
                 


            } 
            return W1;
        }
         
        private Tuple<double[][], double[][], double[], double[], double> AutoencoderGradeAndCost(double[][] W1, double[][] W2, double[] b1, double[] b2, double[][] X,
            double lambda, double beta, double[] rho)
        {
            int m = X[0].Length;


            double[][] bRempat = repmat(b1, m);
            double[][] b2Rempat = repmat(b2, m);

            var z2 = W1.Multiply(X).Add(bRempat);
            var a2 = Sigmoid(z2);
            var z3 = W2.Multiply(a2).Add(b2Rempat);
            var h = Sigmoid(z3);



            var rho_hat = Matrix.Sum(a2, 1).Divide(m);

            var atom1 = rho.Multiply(-1).ElementwiseDivide(rho_hat);

            var atom2 = Matrix.Subtract(1, rho).ElementwiseDivide(Matrix.Subtract(1, rho_hat));

            var sparsity_delta = repmat(atom1.Add(atom2), m);




            var delta3 = X.Subtract(h).Multiply(-1).ElementwiseMultiply(Sigmoid_Prime(z3));

            var sp = sparsity_delta.Multiply(beta);

            var delta2 = (W2.Transpose().Multiply(delta3).Add(sp)).ElementwiseMultiply(Sigmoid_Prime(z2));

            var weDec1 = Matrix.Multiply(lambda, W1);
            var W1grad = delta2.Multiply(X.Transpose()).Divide(m).Add(weDec1);

            var weDec2 = Matrix.Multiply(lambda, W2);
            var W2grad = delta3.Multiply(a2.Transpose()).Divide(m).Add(weDec2);
            var b1Grad = delta2.Sum(1).Divide(m);
            var b2Grad = delta3.Sum(1).Divide(m);


            var dif = h.Subtract(X);
            var cost = Matrix.Sum(Matrix.ElementwiseMultiply(dif, dif)).Sum() / (2 * m);
            var l1 = (lambda / 2);
            var l2 = Matrix.Sum(Matrix.ElementwiseMultiply(W1, W1)).Sum();
            var l3 = Matrix.Sum(Matrix.ElementwiseMultiply(W2, W2)).Sum();
            var l4 = l2 + l3;
            var mu = l4 * l1;
            cost = cost + (mu);
            var d = KL_divergence(rho, rho_hat).Sum() * beta;
            cost += (d);

            if (double.IsInfinity(cost))
            {
                throw new Exception("cost is infinity");
            }
            return new Tuple<double[][], double[][], double[], double[], double>(W1grad, W2grad, b1Grad, b2Grad, cost);
        }
         
        private static double[][] repmat(double[] b1, int m)
        {
            double[][] r = new double[m][];
            for (int i = 0; i < m; i++)
            {
                r[i] = b1;
            }


            return r.Transpose();
        }

        private static double[][] Sigmoid(double[][] z2)
        {


            double[][] s = new double[z2.GetLength(0)][];
            for (int i = 0; i < z2.GetLength(0); i++)
            {
                s[i] = new double[z2[0].Length];
                for (int j = 0; j < z2[0].Length; j++)
                {
                    s[i][j] = 1 / (1 + Math.Exp(-z2[i][j]));

                }
            }

            return s;
        }

        private static double[][] Sigmoid_Prime(double[][] z2)
        {


            double[][] s = new double[z2.GetLength(0)][];
            for (int i = 0; i < z2.GetLength(0); i++)
            {
                s[i] = new double[z2[0].Length];
                for (int j = 0; j < z2[0].Length; j++)
                {
                    var sp = 1 / (1 + Math.Exp(-z2[i][j]));
                    s[i][j] = sp * (1 - sp);
                }
            }

            return s;
        }

        private static double KL_divergence(double x, double y)
        {


            double s = 0;


            s = x * Math.Log(x / y) + (1 - x) * Math.Log((1 - x) / (1 - y));

            return s;
        }

        private static double[] KL_divergence(double[] xV, double[] yV)
        {
            double[] s = new double[xV.Length];

            for (int i = 0; i < xV.Length; i++)
            {

                var x = xV[i];
                var y = yV[i];
                s[i] = x * Math.Log(x / y) + (1 - x) * Math.Log((1 - x) / (1 - y));

            }

            return s;
        }
         
        public static  double[][]  ExtractFeatures(double[][] X, AutoEncoderData model)
        {
            int m = X[0].Length;


            double[][] bRempat = repmat(model.b1, m);
            double[][] b2Rempat = repmat(model.b2, m);

            var z2 = model.W1.Multiply(X).Add(bRempat);
            var a2 = Sigmoid(z2); 

            return a2;
        }
  
    }


    public class CostEventArg : EventArgs
    {
        public double Cost { get; set; }

    }


}
