using PirmaUzduotis.Interface;
using PirmaUzduotis.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirmaUzduotis
{
    class Secant : ISecant
    {
        private double x0;
        private double x1;
       public double allowance { set; get; }

      public Secant(double x0, double x1, double allowance)
        {
            this.allowance = allowance;
            this.x0 = x0;
            this.x1 = x1;
        }

        public double f(double x)
        {
            //double result = x*x*x - x - 3;
            double result = 3 * Math.Exp(-3 * x);
            return result;
        }
        public void newSecant(int i)
        {
            double x2 = x1-((f(x1)*(x1-x0))/(f(x1)-f(x0)));
            x0 = x1;
            x1 = x2;
        }

        public bool Equalation()
        {
            double left = Math.Abs(x1 - x0);
            
            if(left < allowance)
            {
                return true;
            } else
            {
                return false;
            }
        }
        private double answer()
        {
            return x1;
        }
        public void Print()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(String.Format("Tikslusis sprendinys: {0} ", this.answer()));
            sb.AppendLine(String.Format("su paklaida {0}", this.allowance));

            // create a writer and open the file
            TextWriter tw = new StreamWriter("kirstiniu-atsakymas.txt");

            // write a line of text to the file
            tw.WriteLine(sb.ToString());

            // close the stream
            tw.Close();
        }
    }
}
