using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFLDLSolutions.MathHelper;
using System.Drawing;

namespace UFLDLSolutions.Core.Tools
{
    public static  class ImagesTools
    {

        private static Random rng = new Random(); 
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public class TrainingImage
        {
            public int Width;  
            public int Height;  
            public byte[][] Pixels;  
            public byte Label; 
            public TrainingImage(int width, int height,
              byte[][] pixels, byte label)
            {
                this.Width = width; this.Height = height;
                this.Pixels = new byte[height][];

                for (int i = 0; i < this.Pixels.Length; ++i)
                { 
                    this.Pixels[i] = new byte[width];
                    for (int j = 0; j < width; ++j)
                        this.Pixels[i][j] = pixels[i][j];
                }
                 
                this.Label = label;
            }
        }

        public static TrainingImage[] LoadMNISTData(string pixelFile, string labelFile, int numImages = 60000)
        { 
            TrainingImage[] result = new TrainingImage[numImages];

            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];

            FileStream ifsPixels = new FileStream(pixelFile, FileMode.Open);
            FileStream ifsLabels = new FileStream(labelFile, FileMode.Open);

            BinaryReader brImages = new BinaryReader(ifsPixels);
            BinaryReader brLabels = new BinaryReader(ifsLabels);

            int magic1 = brImages.ReadInt32(); // read and discard
            int imageCount = brImages.ReadInt32();
            int numRows = brImages.ReadInt32();
            int numCols = brImages.ReadInt32();

            int magic2 = brLabels.ReadInt32();
            int numLabels = brLabels.ReadInt32();
             
            for (int di = 0; di < numImages; ++di)
            {
                for (int i = 0; i < 28; ++i)  
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                    }
                }

                byte lbl = brLabels.ReadByte(); 
                TrainingImage dImage = new TrainingImage(28, 28, pixels, lbl);
                result[di] = dImage;
            }  

            ifsPixels.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();

            return result;
        } 
        public static Tuple<double[][],double[]> MnistTrainingData()
        {
            if (!(
                File.Exists("train-images.idx3-ubyte") &&
                File.Exists("train-labels.idx1-ubyte") &&
                File.Exists("t10k-images.idx3-ubyte") &&
                File.Exists("t10k-labels.idx1-ubyte")  
                ))
            {

                throw new Exception("Training Data not found");

            }


            var rnd = new Random();
            var files = ImagesTools.LoadMNISTData("train-images.idx3-ubyte", "train-labels.idx1-ubyte").Take(60000);

            var M = files.Count();

            double[][] X =Matrix.MatrixCreate(files.FirstOrDefault().Width*files.FirstOrDefault().Height, M);
            double[] Y = new double[M];
            int currentRow = -1;
            for (int i = 0; i < 1; i++)
            { 
                foreach (var file in files)
                {
                    Random rand = new Random();
                    var barr1 = DigitImageToByteArray(file);
                     
                    currentRow++;
                    int cu = 0;
                    barr1 = barr1.Scale(0, 1);
                    barr1 = barr1.Subtract(barr1.Mean()); 
                    foreach (var b in barr1)
                    {

                        X[cu][currentRow] = b;

                        cu++;
                    }
                    Y[currentRow] = (double)file.Label;
                }

            }

            return new Tuple<double[][],double[]>(X,Y);

        }


        public static Tuple<double[][], double[]> MnistTestData()
        {

            Dictionary<double[], double> inputs = new Dictionary<double[], double>(); 
            var rnd = new Random();
            var images = ImagesTools.LoadMNISTData("t10k-images.idx3-ubyte", "t10k-labels.idx1-ubyte", 10000);

            var M = images.Count();

            double[][] X = Matrix.MatrixCreate(images.FirstOrDefault().Width * images.FirstOrDefault().Height, M);
            double[] Y = new double[M];
            int currentCol = -1;
            for (int i = 0; i < 1; i++)
            {
                var a = new double[] { 1, 0, 0, 0 };

                foreach (var img in images)
                {
                    Random rand = new Random();
                    var dbArr = DigitImageToByteArray(img);

                    currentCol++;
                    int currentRow = 0;
                    dbArr = dbArr.Scale(0, 1);
                    dbArr = dbArr.Subtract(dbArr.Mean()); 
                    foreach (var b in dbArr)
                    {

                        X[currentRow][currentCol] = b;

                        currentRow++;
                    }
                    Y[currentCol] = (double)img.Label;

                }

            }

            return new Tuple<double[][], double[]>(X, Y);
        }


        public static double[] DigitImageToByteArray(TrainingImage img)
        {
            List<double> d = new List<double>();
            for (int j = 0; j < img.Height; j++)
            {
                for (int i = 0; i < img.Width; i++)
                {
                    var sc = ((double)img.Pixels[i][j]) * 255d;
                    
                    d.Add(sc);
                }
            }
            return d.ToArray();
        }

        public static double[] GetRGBImagePatch (Bitmap img, int startX, int startY, int length)
        {
            List<double> d = new List<double>();

            for (int i = startX; i < startX + length; i++)
            {
                for (int j = startY; j < startY + length; j++)
                {

                    Color pixel = img.GetPixel(i, j);
                    var sc = (double)pixel.R / 255;
                    d.Add(sc);

                }

            }
            for (int i = startX; i < startX + length; i++)
            {
                for (int j = startY; j < startY + length; j++)
                {

                    Color pixel = img.GetPixel(i, j);
                    var sc = (double)pixel.G / 255;
                    d.Add(sc);

                }

            }
            for (int i = startX; i < startX + length; i++)
            {
                for (int j = startY; j < startY + length; j++)
                {

                    Color pixel = img.GetPixel(i, j);
                    var sc = (double)pixel.B / 255;
                    d.Add(sc);

                }

            }
            return d.ToArray();
        } 

        public static List<Bitmap> VisualizeRGBFeatures(double[][] w,int channelWidth,int channelHeight)
        {
            List<Bitmap> res = new List<Bitmap>(); 
            var me = w.Mean();
            w = w.Subtract(me); 
            var row = w.GetLength(0);
            var col = w[0].Length;
            int channelSize = channelWidth * channelHeight;
            for (int i = 0; i < row; 
                i++)
            {
                var f = w.GetRow(i); 
                f= f.Divide(w.Abs().Max());
                double[] red = new double[channelSize];
                double[] green = new double[channelSize];
                double[] blue = new double[channelSize];
                for (int g = 0; g < channelSize; g++)
                {
                    red[g] = f[g];
                }
                for (int g = 0; g < channelSize; g++)
                {
                    green[g] = f[g + channelSize];
                }
                for (int g = 0; g < channelSize; g++)
                {
                    blue[g] = f[g + channelSize + channelSize];
                } 
                red = red.Scale(0, 255);
                green = green.Scale(0, 255);
                blue = blue.Scale(0, 255);
                var redChannel = (red).Reshape(channelWidth, channelHeight).Transpose();
                var greenChannel = (green).Reshape(channelWidth, channelHeight).Transpose();
                var blueChannel = (blue).Reshape(channelWidth, channelHeight).Transpose();
                Bitmap source = new Bitmap(channelWidth, channelHeight);
                var r = new Random();
                for (int y = 1; y < channelWidth; y++)
                {
                    for (int x = 1; x < channelHeight; x++)
                    { 
                        double value = redChannel[x][ y];
                        byte b = (byte)value;  
                        var c = Color.FromArgb((byte)redChannel[x][y], (byte)greenChannel[x][y], (byte)blueChannel[x][y]); 
                        source.SetPixel(x, y, c); 
                    }
                }


                res.Add(source);
            }
            return res;
        }

        public static List<Bitmap> VisualizeGrayScaleFeatures(double[][] w)
        {
            List<Bitmap> res = new List<Bitmap>(); 
            var me = w.Mean();
            w = w.Subtract(me); 
            var row = w.GetLength(0);
            var col = w[0].Length;
            var som = Matrix.Sqrt(w.ElementwiseMultiply(w).Sum());
            var sz = (int)Math.Ceiling(Math.Sqrt(col)); 
            for (int i = 0; i < row; i++)
            {
                var f = w.GetRow(i); 
                var clim = w.GetRow(i).Abs().Max(); 
                f=f.Divide(w.Abs().Max());
                f = f.Scale(0, 255); 
                var image = (f).Reshape(sz, sz);
                Bitmap source = new Bitmap(sz, sz);
                var r = new Random();

                for (int y = 1; y < sz; y++)
                {
                    for (int x = 1; x < sz; x++)
                    { 
                        double value = image[x][ y];
                        byte b = (byte)value;   
                        var c = Color.FromArgb(b, b, b); 
                        source.SetPixel(x, y, c); 
                    }
                }


                res.Add(source);
            }
            return res;
        }

        public static  double[][]  PatchedColorImagesTrainingData(  int visible, string path)
        {  
            DirectoryInfo di = new DirectoryInfo(path); 
            var rnd = new Random();

            var files = di.GetFiles().ToList().OrderBy(item => rnd.Next());

            var M = files.Count() * 150;

            double[][] X = Matrix.MatrixCreate(visible, M); 
          
             
            List<double[]> all = new List<double[]>();
            foreach (var file in files)
            {
                Random rand = new Random(); 
                Bitmap im = new Bitmap(file.FullName); 
                for (int c = 0; c < 150; c++)
                {
                    int x = rand.Next(0, 91);
                    int y = rand.Next(0, 91);
                    var barr1 = GetRGBImagePatch(im, x, y, 8); 
                     
                    all.Add(barr1); 
                } 
            } 
            Shuffle(all);  
            int currentRow = -1;
            foreach (var barr1 in all)
            {
                currentRow++;
                int cu = 0;
                foreach (var item in barr1)
                {

                    X[cu][currentRow] = item;


                    cu++;
                }
            }
            return X;
        }

        public static Tuple<double[][], double[]> cifarTrainingDataBinary(int trainigCount = 10000)
        {

            double[][] X = Matrix.MatrixCreate(trainigCount, 3072);
            double[] Y = new double[trainigCount];

            byte[] samples = new byte[30730000];
            using (FileStream fileStreamTrainPatterns = System.IO.File.Open("data_batch_1.bin",
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileStreamTrainPatterns.Read(samples, 0, 30730000);
            }
            for (int index = 0; index < trainigCount; index++)
            {
                Y[index] = samples[3073 * index];
                double[] image = new double[3072];
                for (int i = 0; i < 3072; i++)
                {
                    image[i] = (Convert.ToDouble(samples[((3073 * index) + 1) + i]));;

                }

                X[index] = image;
            }

            return new Tuple<double[][], double[]>(X, Y);

        }

        public static Tuple<double[][],double[]> cifarTrainingData(int trainigCount=10000)
        {

            double[][] X = Matrix.MatrixCreate(trainigCount, 3072);
            double[] Y = new double[trainigCount];

            byte[] samples = new byte[30730000];
            using (FileStream fileStreamTrainPatterns = System.IO.File.Open("data_batch_1.bin",
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fileStreamTrainPatterns.Read(samples, 0, 30730000);
            }
            for (int index = 0; index < trainigCount; index++)
            {
                Y[index]  = samples[3073 * index];
                double[] image = new double[3072];
                for (int i = 0; i < 3072; i++)
                {
                    image[i] = (Convert.ToDouble(samples[((3073 * index) + 1) + i]) / 255.0);;
                    
                }
     
                X[index] = image;
            }

            return new Tuple<double[][],double[]>(X,Y);

        }

        public static double[][] RandomPatchsCifarTrainingData(double[][] data, int visible)
        {

            List<Bitmap> images = new List<Bitmap>();

            for (int i = 0; i < data.Length; i++)
            {
                double[] redflate = new double[1024];
                double[] greenflat = new double[1024];
                double[] blueflate = new double[1024];
                for (int j = 0; j < 1024; j++)
                {
                    redflate[j] = data[i][j];
                    greenflat[j] = data[i][j + 1024];
                    blueflate[j] = data[i][j + 1024 + 1024];

                }
                var red = redflate.Reshape(32, 32);
                var green = greenflat.Reshape(32, 32);
                var blue = blueflate.Reshape(32, 32);
                Bitmap b = new Bitmap(32, 32);
                for (int x = 0; x < 32; x++)
                {
                    for (int y   = 0; y < 32; y++)
                    {
                        Color color = Color.FromArgb((byte)red[x][y], (byte)green[x][y], (byte)blue[x][y]);
                        b.SetPixel(x, y, color);
                    }
                }
                images.Add(b);
            }

            var M = images.Count() * 100;

            double[][] X = Matrix.MatrixCreate(visible, M);


            List<double[]> all = new List<double[]>();
            foreach (var file in images)
            {
                Random rand = new Random();
                Bitmap im = file;
                for (int c = 0; c < 100; c++)
                {
                    int x = rand.Next(0, 23);
                    int y = rand.Next(0, 23);
                    var barr1 = GetRGBImagePatch(im, x, y, 8);

                    all.Add(barr1);
                }
            }
            Shuffle(all);
            int currentRow = -1;
            foreach (var barr1 in all)
            {
                currentRow++;
                int cu = 0;
                foreach (var item in barr1)
                {

                    X[cu][currentRow] = item;


                    cu++;
                }
            }
            return X;


        }
     
    }
}
