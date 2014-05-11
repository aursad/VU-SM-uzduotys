using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreciaUzduotis.aim
{
    public class aim
    {
        public int iteracija = 0;
        public double[,] A { get; set; }
        public double[,] xo { get; set; }
        public double lambda { get; set; }
        public double tikslumas { get; set; }
        public double[,] E { get; set; }

        private double[,] y1, xn, Ax = new double[,] { };
        private double lambdan = 0;

        public aim(double[,] A, double[,] xo, double lambda, double tikslumas, double[,] e)
        {
            this.A = A;
            this.xo = xo;
            this.lambda = lambda;
            this.tikslumas = tikslumas;
            this.E = e;
        }

        public void Program()
        {
            if (isValid(A) == false)
            {
                throw new FormatException("Klaida! Matrica nėra simetrinė!");
            }
            while (true)
            {
                y1 = Subt(this.A, Mult(this.E, this.lambda));
                y1 = Dev(y1, this.xo);
                xn = Dev(y1, norm(y1));
                Ax = MultTest(A, xn);
                lambdan = Sum(Mult(Ax, xn)) / Sum(Mult(xn, xn));

                iteracija++;
                double TempNorm = norm(Subt(xn, xo));
                double ttt = Math.Abs(lambdan - this.lambda);
                if (TempNorm-1 <= this.tikslumas || ttt <= this.tikslumas)
                {
                    break;
                }
                if (TempNorm > 1.7 && TempNorm < 2.3)
                {
                    xn = Mult(xn, -1);
                }

                this.xo = CC(xn);
                this.lambda = Math.Abs(lambdan - this.lambda);
            }
        }
        public double[,] Mult(double[,] A, double[,] B)
        {
            double[,] c = null;
            // Jei matricos Nx1 ir Nx1
            if (A.GetLength(0) == B.GetLength(0) && A.GetLength(1) == B.GetLength(1))
            {
                double[,] temp = new double[B.GetLength(1), B.GetLength(0)];
                for (int i = 0; i < B.GetLength(0); i++)
                {
                    for (int n = 0; n < B.GetLength(1); n++)
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
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);

            for (int i = 0; i < rA; i++)
            {
                for (int n = 0; n < cA; n++)
                {
                    A[i, n] *= x;
                }
            }

            return A;
        }
        public double[,] Dev(double[,] A, double[,] B)
        {
            double[,] c = null;
            // Jei matricos Nx1 ir Nx1
            if (A.GetLength(0) == B.GetLength(0) && A.GetLength(1) == B.GetLength(1))
            {
                double[,] temp = new double[B.GetLength(1), B.GetLength(0)];
                for (int i = 0; i < B.GetLength(0); i++)
                {
                    for (int n = 0; n < B.GetLength(1); n++)
                    {
                        temp[n, i] = B[i, n];
                    }
                }
                B = temp;
            }
            if (A.GetLength(1) == B.GetLength(0))
            {

                c = new double[A.GetLength(0), B.GetLength(0)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        int y = (B.GetLength(1) == 1) ? 0 : j;
                        if (A[i, j] == 0 || B[i, y] == 0)
                        {
                            c[i, j] = 0;
                        }
                        else
                        {
                            c[i, j] = A[i, j] / B[i, y];
                        }
                    }
                }
            }
            else
            {
                throw new FormatException("Nelygus matricų eilučių ir stulpelių skaičius!");
            }

            return c;
        }
        public double[,] Dev(double[,] A, double x)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);

            for (int i = 0; i < rA; i++)
            {
                for (int n = 0; n < cA; n++)
                {
                    A[i, n] /= x;
                }
            }

            return A;
        }
        public double[,] MultTest(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);

            for (int i = 0; i < rA; i++)
            {
                for (int n = 0; n < cA; n++)
                {
                    A[i, n] *= B[i, n];
                }
            }

            return A;
        }
        public double[,] Subt(double[,] A, double[,] B)
        {
            double[,] c = null;
            // Jei matricos Nx1 ir Nx1
            if (A.GetLength(0) == B.GetLength(0) && A.GetLength(1) == B.GetLength(1))
            {
                double[,] temp = new double[B.GetLength(1), B.GetLength(0)];
                for (int i = 0; i < B.GetLength(0); i++)
                {
                    for (int n = 0; n < B.GetLength(1); n++)
                    {
                        temp[n, i] = B[i, n];
                    }
                }
                B = temp;
            }
            if (A.GetLength(1) == B.GetLength(0))
            {

                c = new double[A.GetLength(0), B.GetLength(0)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        int y = (B.GetLength(1) == 1) ? 0 : j;
                        c[i, j] = A[i, j] - B[i, y];
                    }
                }
            }
            else
            {
                throw new FormatException("Nelygus matricų eilučių ir stulpelių skaičius!");
            }

            return c;
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
        public double[,] CC(double[,] A)
        {
            int lenght = A.GetLength(0);
            double[,] Temp = new double[lenght, 1];
            for (int n = 0; n < lenght; n++)
            {
                for (int i = 0; i < 1; i++)
                {
                    Temp[n, i] = A[i, n];
                }
            }
            return Temp;
        }
        public bool isValid(double[,] A)
        {
            int m = 1;
            for (int i = 1; i < A.GetLength(0); i++)
            {
                int n = m;
                while (n < A.GetLength(1))
                {
                    if (A[i, n] != A[n, i])
                    {
                        return false;
                    }
                    n++;
                }
                m++;
            }
            return true;
        }
    }
}
