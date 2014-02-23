using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirmaUzduotis
{
    class Program
    {
        static void Main(string[] args)
        {
            Secant secant = new Secant(3.0, 1.0, 0.0000000001);
            int i = 0;
            while (secant.Equalation() != true)
            {
                secant.newSecant(i);
                i++;
            }
            secant.Print();

            Iteration sp = new Iteration(0.01, 0.448);
            int n = 1;
            sp.Function();
            while (sp.Equalation() != true)
            {
                sp.Function();
                n++;
            }
            sp.Print();
        }
    }
}
