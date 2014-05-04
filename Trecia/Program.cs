using System;
using System.Threading;

namespace Trecia
{
    class Program
    {
        static void Main(string[] args)
        {
            // m m m | a
            // m m m | a
            // m m m | a

            //{{1.812, 0.202, -0.599, 0.432}, {0.202, 6.042, 0.202, -0.599}, {-0.599, 0.202, 6.042, 0.202}, {0.432, -0.599, 0.202, 6.042}}

            // Matrix coefficients
            double[,] matrix = new double[4, 4] { { 1.812, 0.202, -0.599, 0.432 }, { 0.202, 6.042, 0.202, -0.599 }, { -0.599, 0.202, 6.042, 0.202 }, { 0.432, -0.599, 0.202, 6.042 } };
            //{6.64727, 6.19459, 5.43449, 1.66165}
            double[] additional = new double[4] { 6.64727, 6.19459, 5.43449, 1.66165 };

            Seidel i = new Seidel(matrix, additional, 0.0001);

            Thread Z = new Thread(new ThreadStart(i.calculateMatrix));

            Z.Start();
            Z.Join();


            Console.WriteLine("\n Seidel method:");
            showMatrix(i.ResultMatrix);

            Console.ReadKey();
        }

        static double[,] setVal(double[,] x)
        {
            Console.WriteLine("\n Matrix cofficients:");
            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write("Enter value of {0}{1}: ", i, j);
                    x[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }

            return x;
        }
        static double[] setVal(double[] x)
        {
            Console.WriteLine("\n Addtional matrix values:");

            for (int i = 0; i < x.Length; i++)
            {
                Console.Write("Enter value of {0}: ", i);
                x[i] = Convert.ToDouble(Console.ReadLine());
            }

            return x;
        }

        static void showMatrix(double[,] x)
        {
            Console.WriteLine("\n Result:");

            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    Console.Write(" {0} ", x[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void showMatrix(double[] x)
        {
            Console.WriteLine("\n Result:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine(" {0} ", x[i]);
            }
        }
    }


    abstract class SimpleIterations
    {
        public abstract void calculateMatrix();
    }


    /// <summary>
    /// Class Seidel
    /// </summary>
    class Seidel : SimpleIterations
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