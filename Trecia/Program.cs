using System;
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
                { 0.202, 6.042, 0.202, -0.599 }, 
                { -0.599, 0.202, 6.042, 0.202 }, 
                { 0.432, -0.599, 0.202, 6.042 } 
            };

            double[] additional = new double[4] { 6.64727, 6.19459, 5.43449, 1.66165 };

            Seidel i = new Seidel(matrix, additional, 0.0001);
            i.calculateMatrix();
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
}