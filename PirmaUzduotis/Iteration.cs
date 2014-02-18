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
        public Iteration(double allowance, double defineMax)
        {
            this.allowance = allowance;
            this.definedMax = defineMax;
            this.answers = new List<Answer>()
                {
                    new Answer() { Id = 0, xn = 0.0, xn1 = 0.0}
                };
        }
        /// <summary>
        /// Atsakymų sąrašas
        /// </summary>
        private List<Answer> answers;
        /// <summary>
        /// Paklaida
        /// </summary>
        public double allowance { set; get; }
        public double definedMax { set; get; }

        public void Function(double x)
        {
            double result;
            result = 0.2*Math.Atan(x+1);

            double resultBefore = Math.Abs(result - answers.Last().xn);

            answers.Add(new Answer() { Id = answers.Count(), xn = result, xn1 = resultBefore });
        }

        public bool Equalation()
        {
            double right = ((1 - this.definedMax) / this.definedMax) * this.allowance;
            double left = answers.Last().xn1;

            if (answers.Count() == 1) { return false; }
            if (left <= right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Answer> List()
        {
            return answers;
        }
        public double getLastXn1()
        {
            return answers.Last().xn1;
        }
        public double getLastXn()
        {
            return answers.Last().xn;
        }
        public void Print()
        {
            StringBuilder sb = new StringBuilder();
            double result = this.getLastXn() - this.getLastXn1();
            double paklaida = result - this.getLastXn();


            sb.AppendLine("Paklaida: " + this.allowance.ToString());
            sb.AppendLine("Apibrėžta: " + this.definedMax.ToString());
            sb.AppendLine();
            sb.AppendLine("Iter.i |       xi               |       |xi - x(i-1)    |");
            foreach (Answer answer in answers)
            {
                sb.AppendLine(answer.Id + " | " + answer.xn + " | " + answer.xn1);
            }
            sb.AppendLine();
            sb.AppendLine("Tikslusis sprendinys: " + result);
            sb.AppendLine("paklaida " + paklaida);

            // create a writer and open the file
            TextWriter tw = new StreamWriter("iteraciju-atsakymas.txt");

            // write a line of text to the file
            tw.WriteLine(sb.ToString());

            // close the stream
            tw.Close();
        }
    }
}
