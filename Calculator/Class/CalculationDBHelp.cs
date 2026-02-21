using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Class
{
    /// <summary>
    /// Класс для работы с БД Рассчетов
    /// </summary>
    public class CalculationDBHelp
    {
        AppContext db = new AppContext();

        public List<Calculation> GetAllCalculations()
        {
            db.Database.EnsureCreated();
            db.ChangeTracker.Clear();
            db.Calculations.Load();
            return db.Calculations.ToList();
        }
        public void Add(Calculation calculation)
        {
            db.Database.EnsureCreated();
            db.Calculations.Load();
            db.Calculations.Add(calculation);
            db.SaveChanges();
        }
        public void Remove(Calculation calculation)
        {
            using (var db = new AppContext()) // Новый контекст
            {
                db.Database.EnsureCreated();
                db.Calculations.Remove(calculation);
                db.SaveChanges();
            }
        }

        public void Update(int id, Calculation calculation)
        {
            db.Database.EnsureCreated();
            db.Calculations.Load();
            var existingCalculation = db.Calculations.FirstOrDefault(s => s.Id == id);

            if (existingCalculation != null)
            {
                existingCalculation.H = calculation.H;
                existingCalculation.UH = calculation.UH;
                existingCalculation.U_vh = calculation.U_vh;
                existingCalculation.delta = calculation.delta;
                existingCalculation.USH_p = calculation.USH_p;
                existingCalculation.a = calculation.a;
                existingCalculation.b_ = calculation.b_;
                existingCalculation.h_ = calculation.h_;
                existingCalculation.N = calculation.N;
                existingCalculation.a1 = calculation.a1;
                existingCalculation.a2 = calculation.a2;
                existingCalculation.V = calculation.V;
                existingCalculation.S = calculation.S;
                existingCalculation.a_ekv = calculation.a_ekv;
                existingCalculation.S_sr = calculation.S_sr;
                existingCalculation.B = calculation.B;
                existingCalculation.t_r = calculation.t_r;
                existingCalculation.SpeakerId = calculation.SpeakerId;
                existingCalculation.SpeakerType = calculation.SpeakerType;
                db.SaveChanges();
            }
        }
    }
}
