using System;
using System.Collections.Generic;
using System.Text;
using Calculator.Class;

namespace Calculator.Class
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double USH_p { get; set; }
        public double a { get; set; }
        public double b_ { get; set; }
        public double h_ { get; set; }
        public double N { get; set; }
        public double a1 { get; set; }
        public List<int> Surfaces { get; set; }
        public int ApprId { get; set; }
        public int AccConstId { get; set; }
        public double t_r { get; set; }
        public int NoiseLevel { get; set; }
    }
}
