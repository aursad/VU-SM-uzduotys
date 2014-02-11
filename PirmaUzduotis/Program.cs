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
            Iteration sp = new Iteration(0.000001, 0.1);
            int i = 1;
            while(sp.Equalation() != true)
            {
                sp.Function(sp.getLastXn());
                i++;
            }
            sp.Print();
        }
    }
}
