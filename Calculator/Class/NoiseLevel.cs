using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    /// <summary>
    /// Класс для описания уровней шумов
    /// </summary>
    public class NoiseLevel
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int Ush { get; set; }
        public string Sp { get; set; }
    }
}
