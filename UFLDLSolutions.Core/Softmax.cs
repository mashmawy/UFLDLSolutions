using UFLDLSolutions.MathHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UFLDLSolutions
{
    public class Softmax
    {

        public event EventHandler IterationDone;
        int counter = -1;

        public double[][] Run(int num_classes, int input_size, double lambda, double[][] data, double[] labels)
        {

            double[][] theta = Matrix.MatrixCreate(num_classes, input_size);
            Random r = new Random();
            for (int i = 0; i < theta.Length; i++)
            {
                for (int j = 0; j < theta[0].Length; j++)
                {
                    theta[i][j] = r.NextDouble() * 0.005;

                }
            }


            double[][] W1cash;
            int BatchNumber = 500;
            Random RandomGenerator = new Random();
            W1cash = Matrix.MatrixCreate(num_classes, input_size);
            double LearningRate = 0.1;
            double Momentum = 0.5;
            for (int epic = 0; epic < 5000; epic++)
            {



                double[][] batchData = null;
                double[] batchlabel = new double[BatchNumber];

                int[] indexis = new int[BatchNumber];
                for (int i = 0; i < BatchNumber; i++)
                {
                    int ind = RandomGenerator.Next(0, data[0].Length);
                    indexis[i] = ind;
                    batchlabel[i] = labels[ind];
                }
                batchData = data.GetColumns(indexis);

                //    W2 = W1.Transpose();
                var batchgrades = softmax_cost(theta, num_classes, input_size, lambda, batchData, batchData.Transpose(), batchlabel);

                double cost = batchgrades.Item2;





                W1cash = W1cash.Multiply(Momentum).Subtract(batchgrades.Item1.Multiply(LearningRate));

                if (epic > 500)
                {
                    Momentum = 0.99;
                }

                //v = mu * v - learning_rate * dx # integrate velocity

                theta = theta.Add(W1cash);

                if (IterationDone != null)
                {
                    IterationDone(theta, new CostEventArg() { Cost = cost });
                }


            }


            return theta;

        }



        int m = 0;
        double[][] indicator;
        double[][] prob_data;
        double[][] theta;
        public Tuple<double[][], double> softmax_cost(double[][] thetaInput, int num_classes, int input_size, double lambda, double[][] data, double[][] datat, double[] labels)
        {

            m = data[0].Length;
            theta = thetaInput;

            var theta_data = theta.Multiply(data);
            theta_data = theta_data.Subtract(theta_data.Max());
            var exp = theta_data.Exp();
            var sumexp = exp.Sum(0);
            prob_data = exp.ElementwiseDivide(sumexp, 1, true);
            var ones = Matrix.Ones(m);
            var ran = Matrix.RangeArray(m);
            indicator = Matrix.CreateCSRMatrix(ones, labels, ran);


            var div = (-1d / Convert.ToDouble(m));

            var problog = prob_data.Log();
            var sums = indicator.ElementwiseMultiply(problog);
            var costsum = div * sums.Sum().Sum();

            var thetasqr = theta.ElementwiseMultiply(theta);
            var thetasqrsum = thetasqr.Sum().Sum();
            var reg = (lambda / 2) * thetasqrsum;

            var cost = costsum + reg;






            var sub = indicator.Subtract(prob_data);
            var subdotdata = sub.Multiply(datat);
            var left = subdotdata.Multiply(div);

            var right = theta.Multiply(lambda);
            var gradeUnshape = left.Add(right);


            return new Tuple<double[][], double>(gradeUnshape, cost);

        }



        public double[][] softmax_predict(double[][] theta, double[][] data)
        {

            var op = theta.Multiply(data);
            var exp = op.Exp();
            var dominator = exp.Sum(0);
            var prod = exp.ElementwiseDivide(dominator, 1, true);
            return prod;
        }
    }
}
[Serializable]
public class SoftmaxModel
{
    public double[][] Model { get; set; }

}