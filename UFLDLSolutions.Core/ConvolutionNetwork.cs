using UFLDLSolutions.Core.Tools;
using UFLDLSolutions.MathHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UFLDLSolutions.Core
{
    public class CostAndAcc : EventArgs
    {
        public double Cost { get; set; }
        public double Acc { get; set; }
    }
    public class ConvolutionNetwork
    {
        double[][] testData;
        double[] testlabel;
        public event EventHandler IterationDone;
        public void Run()
        {
            int imageDim = 28;
            int numClasses = 10;  // Number of classes (MNIST images fall into 10 classes)
            int filterDim = 9;    //Filter size for conv layer
            int numFilters = 10;  // Number of filters for conv layer
            int poolDim = 2;      // Pooling dimension, (should divide imageDim-filterDim+1)
            var load = ImagesTools.MnistTrainingData();

            var loadTest = ImagesTools.MnistTestData();
            testData = loadTest.Item1;
            testlabel = loadTest.Item2;
            var images = load.Item1;//Matrix.Whitening(load.Item1, 0.1);
            var labels = load.Item2;
            var outDim = imageDim - filterDim + 1;
            //Wc = 1e-1*randn(filterDim,filterDim,numFilters);
            List<double[][]> Wc = new List<double[][]>();
            for (int i = 0; i < numFilters; i++)
            {
                var w = Matrix.RandomN(filterDim, filterDim, 0, 1).Multiply(1e-1);
                Wc.Add(w);
            }
            outDim = outDim / poolDim;
            var hiddenSize = (int)Math.Pow(outDim, 2) * numFilters;
            var r = Math.Sqrt(6) / Math.Sqrt(numClasses + hiddenSize + 1);

            var Wd = Matrix.Random(numClasses, hiddenSize, -r, r).ToArray();
            var bc = Matrix.Zeros(numFilters);
            var bd = Matrix.Zeros(numClasses); 
            int convDim = imageDim - filterDim + 1; // dimension of convolved output
            int outputDim = (convDim) / poolDim; // dimension of subsampled output
            //activations = zeros(convDim, convDim, numFilters, numImages);
            List<double[][]> WcCash = new List<double[][]>();
            for (int i = 0; i < numFilters; i++)
            {
                var w = Matrix.MatrixCreate(filterDim, filterDim);
                WcCash.Add(w);
            }
            var WdCash = Matrix.MatrixCreate(numClasses, hiddenSize);
            var bcCash = Matrix.Zeros(numFilters);
            var bdCash = Matrix.Zeros(numClasses);
             
            minFuncSGD(Wc, Wd, bc, bd, images, labels, numClasses, filterDim, numFilters, poolDim);
  


        }

        private Tuple<List<double[][]>, double[][], double[], double[], double> ConvGrade(int numClasses, int filterDim, int numFilters, int poolDim, double[][] images, double[] labels, List<double[][]> Wc, double[][] Wd, double[] bc, double[] bd, double weightDecay, int convDim, int outputDim)
        {
            int datacount = labels.Length;
            List<List<double[][]>> activations = new List<List<double[][]>>();
            List<List<double[][]>> activationsPooled = new List<List<double[][]>>();
            var meanPoolingFilter = Matrix.Ones(poolDim, poolDim);
            List<double[][]> Wc_rotated = new List<double[][]>();
            var areaOfPoolingFilter = Math.Pow(poolDim, 2);
            meanPoolingFilter = meanPoolingFilter.Divide(areaOfPoolingFilter);
            double[][] reshaped = Matrix.MatrixCreate(1000, datacount);

            for (int i = 0; i < numFilters; i++)
            {
                Wc_rotated.Add(Wc[i].Rot90(2));
            }

            for (int i = 0; i < datacount; i++)
            {
                FeedForword(numFilters, poolDim, images, bc, convDim, outputDim, activations, activationsPooled, meanPoolingFilter, Wc_rotated, reshaped, i);
            }
            //var source = Enumerable.Range(0, datacount);
            //var pquery = from num in source.AsParallel()
            //             select num;
            //pquery.ForAll((i) => FeedForword(numFilters, poolDim, images, bc, convDim, outputDim, activations, activationsPooled, meanPoolingFilter, Wc_rotated, reshaped, i));

            var probs = Matrix.Zeros(numClasses, datacount);

            var bdr = repmat(bd, datacount);
            var activationsSoftmax = Wd.Multiply(reshaped).Add(bdr);
            activationsSoftmax = activationsSoftmax.Subtract(activationsSoftmax.Max());
            activationsSoftmax = activationsSoftmax.Exp();
            var sumexp = activationsSoftmax.Sum(0);
            probs = activationsSoftmax.ElementwiseDivide(sumexp, 1);




            var ones = Matrix.Ones(datacount);
            var ran = Matrix.RangeArray(datacount);
            var indicator = Matrix.CreateCSRMatrix(ones, labels, ran);



            double[] yD = new double[datacount];
            for (int i = 0; i < datacount; i++)
            {
                var col = probs.GetColumn(i);
                var ma = col.Max();
                var ind = col.ToList().IndexOf(ma);
                yD[i] = ind;


            }


            double[] bo = new double[datacount];

            for (int i = 0; i < datacount; i++)
            {
                if (yD[i] == labels[i])
                {
                    bo[i] = 1;
                }
            }



            //     var acc = ConvPredict(numClasses, filterDim, numFilters, poolDim, testData.Transpose(), testlabel, Wc, Wd, bc, bd, 0, convDim, outputDim);
            var acc = bo.Sum() * 100 / datacount;
            var end = DateTime.Now;



            var div = (-1d / Convert.ToDouble(datacount));
            var probLog = probs.Log().ElementwiseMultiply(indicator);
            var sums = indicator.ElementwiseMultiply(probLog);
            var cost = div * probLog.Sum().Sum();
            cost = cost + weightDecay;


            if (IterationDone != null)
            {
                IterationDone(Wc, new CostAndAcc() { Acc = acc, Cost = cost });
            }



            var errorsSoftmax = probs.Subtract(indicator);
            errorsSoftmax = errorsSoftmax.Divide(datacount);


            var errorsPooled = Wd.Transpose().Multiply(errorsSoftmax).Transpose();




            var unpoolingFilter = Matrix.Ones(poolDim, poolDim);
            var poolArea = Math.Pow(poolDim, 2);
            unpoolingFilter = unpoolingFilter.Divide(poolArea);
            List<List<double[][]>> errorsPooling = new List<List<double[][]>>();
            List<List<double[][]>> errorsConvolution = new List<List<double[][]>>();


            for (int i = 0; i < datacount; i++)
            {
                Backprop(numFilters, outputDim, activations, errorsPooled, unpoolingFilter, errorsPooling, errorsConvolution, i);
            }




            double[][] Wd_grad = errorsSoftmax.Multiply(reshaped.Transpose());
            Debug.WriteLine(Wd_grad.Length);
            Wd_grad = Wd_grad.Add(weightDecay.Multiply(Wd));
            var bd_grad = errorsSoftmax.Sum(2);


            var Wc_grad = new List<double[][]>();

            var bc_grad = Matrix.Zeros(numFilters);

            //var source = Enumerable.Range(0, numFilters);
            //var pquery = from num in source.AsParallel()
            //             select num;
            //pquery.ForAll((i) => ;)
            for (int i = 0; i < numFilters; i++)
            {
CalculateGrade(filterDim, images, Wc, datacount, errorsPooling, errorsConvolution, Wc_grad, bc_grad, i)
                ;
            }
            //for (int i = 0; i < numFilters; i++)
            //{

            //    for (int imagenum = 0; imagenum < datacount; imagenum++)
            //    { 
              
            //    }
            //}

            return new Tuple<List<double[][]>, double[][], double[], double[], double>(Wc_grad, Wd_grad, bc_grad, bd_grad, cost);
        }

        private static void CalculateGrade(int filterDim, double[][] images, List<double[][]> Wc, int datacount, List<List<double[][]>> errorsPooling, List<List<double[][]>> errorsConvolution, List<double[][]> Wc_grad, double[] bc_grad, int i)
        {
            var Wc_gradFilter = Matrix.MatrixCreate(filterDim, filterDim);
            foreach (var item in errorsPooling)
            {
                var d = item[i].Sum().Sum();
                bc_grad[i] += d;
            }

            for (int im = 0; im < datacount; im++)
            {
                var e = errorsConvolution[im][i];
                ;

                double[][] image = images[im].Reshape(28, 28);

                Wc_gradFilter = Wc_gradFilter.Add(image.Conv2(e.Rot90(2), ConvType.Valid));
            }



            Wc_grad.Add(Wc_gradFilter.Add(Wc_gradFilter.Multiply(Wc[i])));
        }

        private void FeedForword(int numFilters, int poolDim, double[][] images, double[] bc, int convDim, int outputDim,
            List<List<double[][]>> activations, List<List<double[][]>> activationsPooled, double[][] meanPoolingFilter,
            List<double[][]> Wc_rotated, double[][] reshaped, int i)
        {
            List<double[][]> imageActivation = new List<double[][]>();
            List<double[][]> imagePooled = new List<double[][]>();

            int shapeIndex = 0;
            for (int j = 0; j < numFilters; j++)
            {
                var m = Matrix.MatrixCreate(convDim, convDim);
                var m2 = Matrix.MatrixCreate(outputDim, outputDim);


                double[][] image = images[i].Reshape(28, 28);
                var filteredImage = Sigmoid(image.Conv2(Wc_rotated[j], ConvType.Valid).Add(bc[j]));
                m = m.Add(filteredImage);

                var pooledImage = filteredImage.Conv2(meanPoolingFilter, ConvType.Valid);
                double[][] selectedIndices = GetSelectedIndices(pooledImage, 0, poolDim, pooledImage.Length);
                m2 = m2.Add(selectedIndices);

                for (int x = 0; x < m2.Length; x++)
                {
                    for (int y = 0; y < m2[0].Length; y++)
                    {
                        reshaped[shapeIndex][i] = m2[x][y];
                        shapeIndex++;
                    }
                }

                imageActivation.Add(m);
                imagePooled.Add(m2);
            }

            activations.Add(imageActivation);
            activationsPooled.Add(imagePooled);
        }
        private void Backprop(int numFilters, int outputDim, List<List<double[][]>> activations, double[][] errorsPooled, double[][] unpoolingFilter, List<List<double[][]>> errorsPooling, List<List<double[][]>> errorsConvolution, int i)
        {
            List<double[][]> f2 = new List<double[][]>();
            List<double[][]> f3 = new List<double[][]>();
            int channelStart = 0;
            for (int j = 1; j <= numFilters; j++)
            {
                var all = errorsPooled[i];
                var sub = GetSubImage(all, channelStart, j * outputDim * outputDim)
                    .Reshape(outputDim, outputDim);

                channelStart += outputDim * outputDim;
                var m = sub;
                var korn = Matrix.Korn(m, unpoolingFilter);
                f2.Add(korn);
                var c = activations[i][j - 1];
                var res = korn.ElementwiseMultiply(c).ElementwiseMultiply(Matrix.Subtract(1, c));
                f3.Add(res);

            }
            errorsPooling.Add(f2);
            errorsConvolution.Add(f3);
        }
        
        private double ConvPredict(int numClasses, int filterDim, int numFilters, int poolDim, double[][] images, double[] labels, List<double[][]> Wc, double[][] Wd, double[] bc, double[] bd, double weightDecay, int convDim, int outputDim)
        {
            List<List<double[][]>> activations = new List<List<double[][]>>();
            for (int i = 0; i < 10000; i++)
            {
                List<double[][]> f = new List<double[][]>();
                for (int j = 0; j < numFilters; j++)
                {
                    var m = Matrix.MatrixCreate(convDim, convDim);
                    f.Add(m);
                }
                activations.Add(f);
            }

            List<List<double[][]>> activationsPooled = new List<List<double[][]>>();
            for (int i = 0; i < 10000; i++)
            {
                List<double[][]> f = new List<double[][]>();
                for (int j = 0; j < numFilters; j++)
                {
                    var m = Matrix.MatrixCreate(outputDim, outputDim);
                    f.Add(m);
                }
                activationsPooled.Add(f);
            }


            var meanPoolingFilter = Matrix.Ones(poolDim, poolDim);

            List<double[][]> Wc_rotated = new List<double[][]>();
            for (int i = 0; i < numFilters; i++)
            {
                Wc_rotated.Add(Wc[i].Rot90(2));
            }
            var areaOfPoolingFilter = Math.Pow(poolDim, 2);
            meanPoolingFilter = meanPoolingFilter.Divide(areaOfPoolingFilter);

            for (int i = 0; i < images.Length; i++)
            {
                for (int f = 0; f < numFilters; f++)
                {
                    //   for (int channel = 1; channel < 4; channel++)
                    //    {
                    double[][] image = images[i].Reshape(28, 28);
                    var filteredImage = Sigmoid(image.Conv2(Wc_rotated[f], ConvType.Valid).Add(bc[f]));
                    activations[i][f] = activations[i][f].Add(filteredImage);

                    var pooledImage = filteredImage.Conv2(meanPoolingFilter, ConvType.Valid);
                    double[][] selectedIndices = GetSelectedIndices(pooledImage, 0, poolDim, pooledImage.Length);
                    activationsPooled[i][f] = activationsPooled[i][f].Add(selectedIndices);
                    //}
                }
            }

            double[][] reshaped = Matrix.MatrixCreate(1000, 10000);
            for (int i = 0; i < activationsPooled.Count; i++)
            {
                int shapeIndex = 0;

                for (int j = 0; j < activationsPooled[i].Count; j++)
                {
                    //activationsPooled[i][j] = activationsPooled[i][j].Divide(3);
                    for (int x = 0; x < activationsPooled[i][j].Length; x++)
                    {
                        for (int y = 0; y < activationsPooled[i][j][0].Length; y++)
                        {
                            reshaped[shapeIndex][i] = activationsPooled[i][j][x][y];
                            shapeIndex++;
                        }
                    }
                }
            }


            var probs = Matrix.Zeros(numClasses, 10000);

            var bdr = repmat(bd, 10000);
            var activationsSoftmax = Wd.Multiply(reshaped).Add(bdr);
            activationsSoftmax = activationsSoftmax.Subtract(activationsSoftmax.Max());
            activationsSoftmax = activationsSoftmax.Exp();
            var sumexp = activationsSoftmax.Sum(0);
            probs = activationsSoftmax.ElementwiseDivide(sumexp, 1);




            var ones = Matrix.Ones(10000);
            var ran = Matrix.RangeArray(10000);
            var indicator = Matrix.CreateCSRMatrix(ones, labels, ran);



            double[] yD = new double[10000];
            for (int i = 0; i < 10000; i++)
            {
                var col = probs.GetColumn(i);
                var ma = col.Max();
                var ind = col.ToList().IndexOf(ma);
                yD[i] = ind;


            }


            double[] bo = new double[10000];

            for (int i = 0; i < 10000; i++)
            {
                if (yD[i] == labels[i])
                {
                    bo[i] = 1;
                }
            }



            //     var acc = ConvPredict(numClasses, filterDim, numFilters, poolDim, testData.Transpose(), testlabel, Wc, Wd, bc, bd, 0, convDim, outputDim);
            var acc = bo.Sum() * 100 / 10000;
            return acc;


        }


        private void minFuncSGD(List<double[][]> Wc, double[][] Wd, double[] bc, double[] bd,
            double[][] images, double[] labels, int numClasses, int filterDim, int numFilters, int poolDim)
        {



            int imageDim = 28;
            double weightDecay = 1e-3;
            int convDim = imageDim - filterDim + 1; // dimension of convolved output
            int outputDim = (convDim) / poolDim; // dimension of subsampled output
            int epochs = 3;
            int minibatch = 256;
            double alpha = 1e-1;
            double momentum = 0.95;
            int numSamples = labels.Length;
            double mom = 0.5;
            int momIncrease = 20;
            var outDim = (convDim) / poolDim;
            var hiddenSize = (int)Math.Pow(outDim, 2) * numFilters;




            List<double[][]> Wcvelocity = new List<double[][]>();
            for (int i = 0; i < numFilters; i++)
            {
                var w = Matrix.MatrixCreate(filterDim, filterDim);
                Wcvelocity.Add(w);
            }
            var Wdvelocity = Matrix.MatrixCreate(numClasses, hiddenSize);
            var bcvelocity = Matrix.Zeros(numFilters);
            var bdvelocity = Matrix.Zeros(numClasses);




            int iteration = 0;

            for (int e = 0; e < epochs; e++)
            {

                var rp = randperm(labels.Length);

                for (int s = 0; s < 60000; s += minibatch)
                {
                    bool escap = false;
                    if (s + 256 > 60000)
                    {
                        s = 60000 - 256 - 1;
                        escap = true;
                    }
                    iteration = iteration + 1;
                    if (iteration == momIncrease)
                    {
                        mom = momentum;
                    }

                    double[][] batchData = null;
                    double[] batchlabel = new double[minibatch];

                    int[] indexis = new int[minibatch];
                    int scounter = 0;
                    for (int i = s; i < s + minibatch - 1; i++)
                    {
                        indexis[scounter] = rp[i];
                        batchlabel[scounter] = labels[rp[i]];
                        scounter++;
                    }

                    batchData = images.GetColumns(indexis).Transpose();

                    var batchgrades = ConvGrade(numClasses, filterDim, numFilters, poolDim, batchData, batchlabel, Wc, Wd, bc, bd, weightDecay, convDim, outputDim);


                    for (int i = 0; i < Wcvelocity.Count; i++)
                    {
                        Wcvelocity[i] = Wcvelocity[i].Multiply(mom).Add(alpha.
                            Multiply(batchgrades.Item1[i]));
                    }
                    Wdvelocity = Wdvelocity.Multiply(mom).Add(alpha.
                        Multiply(batchgrades.Item2));

                    bcvelocity = bcvelocity.Multiply(mom).Add(alpha.
                        Multiply(batchgrades.Item3));

                    bdvelocity = bdvelocity.Multiply(mom).Add(alpha.
                        Multiply(batchgrades.Item4));

                    for (int i = 0; i < Wc.Count; i++)
                    {
                        Wc[i] = Wc[i].Subtract(Wcvelocity[i]);
                    }
                    Wd = Wd.Subtract(Wdvelocity);
                    bc = bc.Subtract(bcvelocity);
                    bd = bd.Subtract(bdvelocity);

                    if (escap)
                    {
                        break;
                    }
                }


                alpha = alpha / 2.0;

            }

            var acc = ConvPredict(numClasses, filterDim, numFilters, poolDim, testData.Transpose(), testlabel, Wc, Wd, bc, bd, weightDecay, convDim, outDim);
            // var acc2 = ConvPredict(numClasses, filterDim, numFilters, poolDim, testData.Transpose(), testlabel, Wc, Wd, bc, bd, 0, convDim, outDim);
            MessageBox.Show(acc.ToString());
            // MessageBox.Show(acc2.ToString());
        }




        //randperm
        private static Random randpermRand = new Random(1);
        private static int[] randperm(int count)
        {
            int[] res = new int[count];

            for (int i = 0; i < count; i++)
            {
                res[i] = randpermRand.Next(0, count - 1);

            }

            return res;
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

        private double[][] GetSelectedIndices(double[][] pooledImage, int p1, int poolDim, int p2)
        {
            List<int> indeces = new List<int>();

            for (int i = p1; i <= p2; i += poolDim)
            {
                indeces.Add(i);
            }
            double[][] res = Matrix.MatrixCreate(indeces.Count, indeces.Count);

            for (int i = 0; i < res.Length; i++)
            {
                for (int j = 0; j < res[0].Length; j++)
                {
                    res[i][j] = pooledImage[indeces[j]][indeces[i]];
                }
            }
            return res;

        }
        private static double[][] Sigmoid(double[][] z2)
        {


            double[][] s = new double[z2.GetLength(0)][];
            for (int i = 0; i < z2.GetLength(0); i++)
            {
                s[i] = new double[z2[0].Length];
                for (int j = 0; j < z2[0].Length; j++)
                {
                    s[i][j] =   1 / (1 + Math.Exp(-z2[i][j]));

                }
            }

            return s;
        }

        private double[] GetSubImage(double[] p1, int channelStart, int p2)
        {
            double[] res = new double[p2 - channelStart];
            int innerCounter = 0;
            for (int i = channelStart; i < p2; i++)
            {
                res[innerCounter] = p1[i];
                innerCounter++;
            }
            return res;
        }
    }
}
