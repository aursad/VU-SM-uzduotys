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
    class Iteration : IIteration
    {
        private double xn = 0.0;
        private double xn1 = 0.0;
        public List<Answer> answers;

        public Iteration(double allowance, double defineMax)
        {
            this.allowance = allowance;
            this.definedMax = defineMax;
            answers = new List<Answer>() {
                new Answer { i = 0, xi = xn, fx = xn1, allowance = 0}
            };
        }
        /// <summary>
        /// Paklaida
        /// </summary>
        public double allowance { set; get; }
        public double definedMax { set; get; }

        public void Function()
        {
            //double result = 3 * Math.Exp(-3 * this.xn);
            double result = 0.2 * Math.Atan(this.xn + 1);
            this.xn1 = result;
        }

        public bool Equalation()
        {
            double right = ((1 - this.definedMax) / this.definedMax) * this.allowance;
            double left = Math.Abs(this.xn1 - this.xn);
            answers.Add(new Answer() { i = answers.Last().i + 1, xi = this.xn1, fx = this.xn, allowance = left });

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
            foreach (Answer answer in answers)
            {
                sb.AppendLine(String.Format("i = {0,-4} xi = {1,-22} f(x) = {2,-22} Paklaida = {3,-22}", answer.i, answer.xi, answer.fx, answer.allowance));
            }
            sb.AppendLine();
            sb.AppendLine(String.Format("Tikslusis sprendinys: {0}", result));
            sb.AppendLine(String.Format("paklaida              {0}", paklaida));

            // create a writer and open the file
            TextWriter tw = new StreamWriter("iteraciju-atsakymas.txt");

            // write a line of text to the file
            tw.WriteLine(sb.ToString());

            // close the stream
            tw.Close();
        }
    }
}
