using PirmaUzduotis.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirmaUzduotis
{
    class Iteration : IIteration
    {
        private double xn = 0.0;
        private double xn1 = 0.0;

        public Iteration(double allowance, double defineMax)
        {
            this.allowance = allowance;
            this.definedMax = defineMax;
        }
        /// <summary>
        /// Paklaida
        /// </summary>
        public double allowance { set; get; }
        public double definedMax { set; get; }

        public void Function()
        {
            double result = 3 * Math.Exp(-3 * this.xn);
            //double result = 0.2 * Math.Atan(this.xn + 1);
            this.xn1 = result;
        }

        public bool Equalation()
        {
            double right = ((1 - this.definedMax) / this.definedMax) * this.allowance;
            double left = Math.Abs(this.xn1 - this.xn);

            if (left <= right)
            {
                return true;
            }
            else
            {
                this.xn = this.xn1;
                this.xn1 = 0.0;
                return false;
            }
        }
        public void Print()
        {
            StringBuilder sb = new StringBuilder();
            double paklaida = Math.Abs(this.xn1 - this.xn);
            double result = this.xn1 + paklaida;


            sb.AppendLine(String.Format("Paklaida : {0}", this.allowance));
            sb.AppendLine(String.Format("Apibrėžta: {0}", this.definedMax));
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine(String.Format("Tikslusis sprendinys: {0}", result));
            sb.AppendLine(String.Format("paklaida {0}", paklaida));

            // create a writer and open the file
            TextWriter tw = new StreamWriter("iteraciju-atsakymas.txt");

            // write a line of text to the file
            tw.WriteLine(sb.ToString());

            // close the stream
            tw.Close();
        }
    }
}
