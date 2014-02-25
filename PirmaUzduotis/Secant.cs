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
        /// <summary>
        /// First initial approach
        /// </summary>
        private double x0;
        /// <summary>
        /// Second initial approach
        /// </summary>
        private double x1;
        /// <summary>
        /// Allowance for counting
        /// </summary>
        public double allowance { set; get; }


        public List<Answer> answers;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x0">First initial approach</param>
        /// <param name="x1">Second initial approach</param>
        /// <param name="allowance">Allowance</param>
        public Secant(double x0, double x1, double allowance)
        {
            this.allowance = allowance;
            this.x0 = x0;
            this.x1 = x1;
            answers = new List<Answer>();
        }
        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="x"></param>
        /// <returns>Function answer</returns>
        public double f(double x)
        {
            double result = x*x*x - x - 3;
            //double result = 3 * Math.Exp(-3 * x);
            return result;
        }
        /// <summary>
        /// New approach
        /// </summary>
        /// <param name="i">Iteration number</param>
        public void newSecant(int i)
        {
            double x2 = x1 - ((f(x1) * (x1 - x0)) / (f(x1) - f(x0)));
            x0 = x1;
            x1 = x2;
            answers.Add(new Answer() { i = i, xi = x0, fx = x1, allowance = Math.Abs(this.f(x1)) });
        }
        /// <summary>
        /// Checking the accuracy
        /// </summary>
        /// <returns>boolean is accuracy</returns>
        public bool Equalation()
        {
            double left = Math.Abs(x1 - x0);
            
            if (answers.LastOrDefault().allowance < allowance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Return answer
        /// </summary>
        /// <returns>Answer</returns>
        private double answer()
        {
            return x1;
        }
        /// <summary>
        /// Print information to file
        /// </summary>
        public void Print()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(String.Format("Tikslusis sprendinys: {0} ", this.answer()));
            sb.AppendLine(String.Format("su paklaida           {0}", this.allowance));
            sb.AppendLine();
            foreach (Answer answer in answers)
            {
                sb.AppendLine(String.Format("i = {0,-4} xi = {1,-20} f(x) = {2,-20} Paklaida = {3,-20}",answer.i,answer.xi, answer.fx, answer.allowance));
            }
            sb.AppendLine();
            // create a writer and open the file
            TextWriter tw = new StreamWriter("kirstiniu-atsakymas.txt");

            // write a line of text to the file
            tw.WriteLine(sb.ToString());

            // close the stream
            tw.Close();
        }
    }
}
