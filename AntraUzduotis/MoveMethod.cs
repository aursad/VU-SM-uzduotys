using AntraUzduotis.Interface;
using AntraUzduotis.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraUzduotis
{
    public class MoveMethod : IMoveMethod
    {
        private Matrix matrix;
        private List<double> x;

        public MoveMethod()
        {
            this.matrix = new Matrix();
            this.x = new List<double>();
        }
        /// <summary>
        /// Read information for matrix from file
        /// </summary>
        /// <param name="fileName">File name</param>
        public void ReadFromFile(string fileName)
        {
            string[] lines = File.ReadAllLines(String.Format(@"E:/GitHub/SkaitiniaiMetodai/AntraUzduotis/data/{0}", fileName));
            this.matrix.n = Convert.ToInt32(lines.First().ToString());
            int i = 0;

            foreach (var line in lines.Skip(1))
            {
                string[] numbers = line.Split(' ');
                List<double> numberList = new List<double>();
                double result;
                foreach (string value in numbers)
                {
                    try
                    {
                        result = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
                        numberList.Add(result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception {0}", ex);
                    }
                }
                matrix.line.Add(new Line() { id = i, values = numberList });
                i++;
            }
        }

        public void Start()
        {
            int n = this.matrix.n;
            int now = 1;
            int before = 0;

            Line lineOne = this.matrix.line[0];
            this.matrix.line[0].c = lineOne.values[now] * -1 / lineOne.values[before];
            this.matrix.line[0].d = lineOne.values[n] / lineOne.values[before];
            now++; before++;

            for (int i = 1; i <= n-1; i++)
            {
                Line line = this.matrix.line[i];
                if(i >= 2 && i <= n-2)
                {
                    if(Math.Abs(line.values[now]) >= (Math.Abs(line.values[before]) + Math.Abs(line.values[now+1])))
                    {
                        throw new System.ArgumentException("Error with matrix line!");
                    }
                }
                if (i != n-1)
                {
                    this.matrix.line[i].c = (line.values[now] * -1) / (line.values[now] * this.matrix.line[i - 1].c + line.values[before]);
                }
                this.matrix.line[i].d = (line.values[n] - (line.values[now] * this.matrix.line[i - 1].d)) / (line.values[now] * this.matrix.line[i - 1].c + line.values[before]);
                now++; before++;
            }

        }
        public void Reverse()
        {
            int n = this.matrix.n - 1;
            double x = this.matrix.line[n].d;
            this.x.Add(x);

            for(int i = n-1; i >= 0; i--)
            {
                x = this.matrix.line[i].c * x + this.matrix.line[i].d;
                this.x.Add(x);
            }
            this.x.Reverse();
        }
        /// <summary>
        /// Print information to file
        /// </summary>
        public void Print()
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (double answer in this.x)
            {
                sb.AppendLine(String.Format("x{0} = {1,-4}", i, answer));
                i++;
            }
            sb.AppendLine();
            // create a writer and open the file
            string fileName = String.Format("moveMethod_{0}.txt", DateTime.Now.Ticks);
            string fullPath = Path.GetFullPath(fileName);

            TextWriter tw = new StreamWriter(fullPath);

            // write a line of text to the file
            tw.WriteLine(sb.ToString());

            // close the stream
            tw.Close();
        }
    }
}
