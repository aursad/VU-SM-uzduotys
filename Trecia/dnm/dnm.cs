using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreciaUzduotis
{
    public class dnm
    {
        public double[,] A;
        public double[,] z;
        public double[,] f;
        public double paklaida;
        public List<double[,]> x;
        int k = 0;
        double skaliarineSandauga;

        public dnm(double[,] A, double[,] f, List<double[,]> x, double paklaida)
        {
            this.A = A;
            this.f = f;
            this.x = x;
            this.paklaida = paklaida;
        }
        public void Program()
        {
            var z = Subt(Mult(A, x[k]), f);
            
            while (true)
            {
                double zSum = Sum(Mult(z, z));

                var r = Mult(A, z);
                var t = zSum / Sum(Mult(r, z));

                var ff = Mult(z, t);
                var xk = Subt(x[k], ff);
                z = Subt(z, Mult(r, t));

                skaliarineSandauga = Sum(Mult(z, z));
                if (skaliarineSandauga < (paklaida * paklaida))
                {
                    break;
                }
                else
                {
                    x.Add(xk);
                    k += 1;
                }
            }

        }

        public double[,] Mult(double[,] A, double[,] B)
        {
            double[,] c = null;
            // Jei matricos Nx1 ir Nx1
            if (A.GetLength(0) == B.GetLength(0) && A.GetLength(1) == B.GetLength(1))
            {
                double[,] temp = new double[B.GetLength(1), B.GetLength(0)];
                for(int i=0;i<B.GetLength(0);i++)
                {
                    for(int n = 0; n<B.GetLength(1);n++)
                    {
                        temp[n, i] = B[i, n];
                    }
                }
                B = temp;
            }
            if (A.GetLength(1) == B.GetLength(0))
            {
                c = new double[A.GetLength(0), B.GetLength(1)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < A.GetLength(1); k++)
                            c[i, j] = c[i, j] + A[i, k] * B[k, j];
                    }
                }
            }
            else
            {
                throw new FormatException("Nelygus matricų eilučių ir stulpelių skaičius!");
            }

            return c;
        }
        public double[,] Mult(double[,] A, double x)
        {
            int rA = A.GetLength(0);//3
            int cA = A.GetLength(1);//3

            for (int i = 0; i < rA; i++)
            {
                for (int n = 0; n < cA; n++)
                {
                    A[i, n] *= x;
                }
            }

            return A;
        }
        public double[,] Subt(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);

            double[,] Temp = new double[rA, cA];

            for (int i = 0; i < rA; i++)
            {
                for (int n = 0; n < cA; n++)
                {
                    Temp[i, n] = A[i, n] - B[i, n];
                }
            }

            return Temp;
        }
        public double Sum(double[,] A)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            double sum = 0;

            for (int i = 0; i < rA; i++)
            {
                for (int n = 0; n < cA; n++)
                {
                    sum += A[i, n];
                }
            }

            return sum;
        }
        public double norm(double[,] A)
        {
            int lenght = A.GetLength(0);
            double sum = 0;
            for (int n = 0; n < lenght; n++)
            {
                for (int i = 0; i < A.GetLength(1); i++)
                {
                    sum += A[n, i] * A[n, i];
                }
            }
            return Math.Sqrt(sum);
        }
        public void PrintAnswer()
        {
            Console.Out.WriteLine(String.Format("({0},{1},{2},{3})",
                        x[k][0, 0], x[k][1, 0], x[k][2, 0], x[k][3, 0]));
            Console.Out.WriteLine("Iterija: " + k);
            Console.Out.WriteLine(String.Format("Netiklis z{0} = {1}",
                k, Math.Sqrt(skaliarineSandauga)));
            Console.Out.WriteLine(String.Format("x = ({0},{1},{2},{3})",
                Math.Round(x[k][0, 0]), Math.Round(x[k][1, 0]), Math.Round(x[k][2, 0]),
                Math.Round(x[k][3, 0])));
            Console.Out.WriteLine(String.Format("Paklaida: {0}",
                norm(Subt(x[k], x[k - 1]))));
        }
    }
}
