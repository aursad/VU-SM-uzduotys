using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirmaUzduotis.Interface
{
    interface IIteration
    {
        /// <summary>
        /// Paklaida
        /// </summary>
        double allowance { set; get; }
        /// <summary>
        /// Didžiausia išvestinės reikšmė apibrėžtame intervale
        /// </summary>
        double definedMax { set; get; }

        /// <summary>
        /// Generuojama funkcija sprendimui
        /// </summary>
        void Function(double x);

    }
}
