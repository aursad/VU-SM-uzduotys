using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreciaUzduotis
{
    /// <summary>
    /// Class Seidel
    /// </summary>
    public class Seidel : SimpleIterations
    {
        private double[] resultMatrix;
        public double[] ResultMatrix
        {
            get
            {
                if (resultMatrix != null)
                    return resultMatrix;
                else
                {
                    return new double[3] { 0, 0, 0 };
                }
            }
        }
        private double[,] matrix;
        private double[] addtional;
        private double accuracy;

        public double Accuracy
        {
            get
            {
                return accuracy;
            }
            set
            {
                if (value <= 0.0)
                    accuracy = 0.1;
                else
                    accuracy = value;
            }
        }

        public Seidel(double[,] Matrix, double[] FreeElements, double Accuracy)
        {
            this.matrix = Matrix;
            this.addtional = FreeElements;
            this.Accuracy = Accuracy;

        }

        public override void calculateMatrix()
        {
            double[,] a = new double[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1) - 1; j++)
                    a[i, j] = matrix[i, j];

            for (int i = 0; i < a.GetLength(0); i++)
                a[i, a.GetLength(1) - 1] = addtional[i];


            double[] previousValues = new double[matrix.GetLength(0)];
            for (int i = 0; i < previousValues.GetLength(0); i++)
            {
                previousValues[i] = 0.0;
            }

            while (true)
            {
                double[] currentValues = new double[a.GetLength(0)];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    currentValues[i] = a[i, a.GetLength(0)];

                    for (int j = 0; j < a.GetLength(0); j++)
                    {
                        if (j < i)
                        {
                            currentValues[i] -= a[i, j] * currentValues[j];
                        }

                        if (j > i)
                        {
                            currentValues[i] -= a[i, j] * previousValues[j];
                        }
                    }

                    currentValues[i] /= a[i, i];
                }
                double differency = 0.0;

                for (int i = 0; i < a.GetLength(0); i++)
                    differency += Math.Abs(currentValues[i] - previousValues[i]);

                if (differency < accuracy)
                    break;

                previousValues = currentValues;
            }

            resultMatrix = previousValues;
        }
    }
}
