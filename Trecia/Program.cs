using System;
using System.Collections.Generic;
using System.Threading;

namespace TreciaUzduotis
{
    class Program
    {
        static void Main(string[] args)
        {
            // m m m | a
            // m m m | a
            // m m m | a

            // Matrix coefficients
            double[,] matrix = new double[4, 4] 
            { 
                { 1.812, 0.202, -0.599, 0.432 }, 
                { 0.202, 1.812, 0.202, -0.599 }, 
                { -0.599, 0.202, 1.812, 0.202 }, 
                { 0.432, -0.599, 0.202, 1.812 } 
            };

            double[] additional = new double[4] { 6.64727, 6.19459, 5.43449, 1.66165 };

            isMatrixValid(matrix);
            if (!isValid(matrix) || !isMatrixValid(matrix))
            {
                Console.Out.WriteLine("Matrica nėra validi!");
                Console.ReadKey();
            }
            Seidel i = new Seidel(matrix, additional, 0.0001);
            //TODO Patikrinti ar matrica teisinga pagal suma kaip skaidrese
            Thread Z = new Thread(new ThreadStart(i.calculateMatrix));

            Z.Start();
            Z.Join();


            Console.WriteLine("\n Seidel method:");
            showMatrix(i.ResultMatrix);

            Console.WriteLine();
            Console.WriteLine("\n Didziausio nuolydzio metodas:");

            double[,] b = new double[4, 1] { {1.941}, {-0.230}, {-1.941}, {0.230} };
            List<double[,]> x = new List<double[,]>()
            {
                new double[4,1] {{0}, {0}, {0}, {0}}
            };
            dnm didziausioNuolydzio = new dnm(matrix, b, x, 0.0001);
            didziausioNuolydzio.Program();
            didziausioNuolydzio.PrintAnswer();

            Console.ReadKey();
        }
        /// <summary>
        /// Simetriškai validus
        /// </summary>
        /// <param name="matrix">Matrica</param>
        /// <returns>true jei validus</returns>
        static bool isValid(double[,] matrix)
        {
            int m = 1;
            for (int i = 1; i < Math.Sqrt(matrix.Length); i++)
            {
                int n = m;
                while (n < Math.Sqrt(matrix.Length))
                {
                    if (matrix[i, n] != matrix[n, i])
                    {
                        return false;
                    }
                    n++;
                }
                m++;
            }
            return true;
        }
        static bool isMatrixValid(double[,] matrix)
        {
            for (int i = 2; i <= matrix.GetLength(0);i++)
            {
                var matrixCopy = CloneMatrix(matrix, i);
                if (det(matrixCopy) <= 0)
                {
                    return false;
                }
            }
            
            return true;
        }

        static double det(double[,] matrix)
        {
            int n = int.Parse(System.Math.Sqrt(matrix.Length).ToString());
            int nm1 = n - 1;
            int kp1;
            double p;
            double det = 1;
            for (int k = 0; k < nm1; k++)
            {
                kp1 = k + 1;
                for (int i = kp1; i < n; i++)
                {
                    p = matrix[i, k] / matrix[k, k];
                    for (int j = kp1; j < n; j++)
                        matrix[i, j] = matrix[i, j] - p * matrix[k, j];
                }
            }
            for (int i = 0; i < n; i++)
                det = det * matrix[i, i];
            return det;
        }
        static double[,] CloneMatrix(double[,] aMatrix, int n)
        {
            var newMatrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    newMatrix[i, j] = aMatrix[i, j];
                }
            }
            return newMatrix;

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
}