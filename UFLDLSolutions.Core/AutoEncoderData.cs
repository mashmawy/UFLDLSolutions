using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFLDLSolutions 
{
    [Serializable]
    public class AutoEncoderData
    {
        public bool TiedWeight; 
        public int HiddenCount;
        public int VisibleCount;
        public int Iteration;
        public int BatchNumber;
        public double[] b1;
        public double[] b2;
        public double[][] Features;
        public double[][] W1;
        public double[][] w2; 
        public double[] b1cash;
        public double[] b2cash;
        public double[][] W1cash;
        public double[][] W2cash; 
        public double LearningRate;
        public double Momentum;
        public double Lambda;
        public double Beta;
        public double[] Rho;
        public double Cost;

    }
}
