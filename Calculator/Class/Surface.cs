using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    /// <summary>
    /// Класс для описания типов поверхностей 
    /// </summary>
    public class Surface
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Ush { get; set; }
        public List<double> F { get; set; }
    }
}
