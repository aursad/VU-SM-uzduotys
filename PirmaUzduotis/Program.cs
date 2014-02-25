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
            //Paprastųjų iteracijų metodas
            Iteration sp = new Iteration(0.000001, 0.1);
            //Iteration sp = new Iteration(0.000001, 0.00000829);
            int n = 1;
            sp.Function();
            while (sp.Equalation() != true && n <= 1000)
            {
                sp.Function();
                n++;
            }
            sp.Print();

            //Kirstinių metodas
            //Secant secant = new Secant(4, 5.5, 0.0000000001);
            Secant secant = new Secant(2.0, 1.0, 0.0000000001);
            int i = 0;
            secant.newSecant(i);
            i++;
            while (secant.Equalation() != true)
            {
                secant.newSecant(i);
                i++;
            }
            secant.newSecant(i);
            secant.Print();
        }
    }
}
