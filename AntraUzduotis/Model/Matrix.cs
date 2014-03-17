using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraUzduotis.Model
{
    public class Matrix
    {
        public int n;
        public List<Line> line = new List<Line>();
    }

    public class Line
    {
        public int id;
        public List<double> values;
        public double c;
        public double d;
    }
}
