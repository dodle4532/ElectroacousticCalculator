using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Class
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double P_Vt { get; set; }
        public double[] P_01 { get; set; }
        public double[] SHDN_v { get; set; }
        public double[] SHDN_g { get; set; }
    }
}
