using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Class
{
    /// <summary>
    /// Рассчёт
    /// </summary>
    public class Calculation
    {
        public int Id { get; set; }
        public double H { get; set; }
        public double UH { get; set; }
        public double U_vh { get; set; }
        public double delta { get; set; }
        public double USH_p { get; set; }
        public double a { get; set; }
        public double b_ { get; set; }
        public double h_ { get; set; }
        public double N { get; set; }
        public double a1 { get; set; }
        public double a2 { get; set; }
        public double V { get; set; }
        public double S { get; set; }
        public double a_ekv { get; set; }
        public double S_sr { get; set; }
        public double B { get; set; }
        public double t_r { get; set; }
        public int SpeakerId { get; set; }
        public DataTypes.SpeakerType SpeakerType { get; set; }
        public string Name { get; set; }
        public int NoiseLevel { get; set; }
        /// <summary>
        /// 6 id выбранных поверхностей
        /// </summary>
        public List<int> Surfaces { get; set; }
        public int ApprId { get; set; }
        public int AccConstId { get; set; }
    }
}
