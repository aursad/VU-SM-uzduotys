using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraUzduotis
{
    class Program
    {
        static void Main(string[] args)
        {
            MoveMethod mm = new MoveMethod();
            mm.ReadFromFile("test1.txt");
            Console.ReadKey();
        }
    }
}
