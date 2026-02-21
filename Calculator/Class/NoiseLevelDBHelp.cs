using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    public class NoiseLevelDBHelp
    {
        AppContext db = new AppContext();

        public List<NoiseLevel> GetAllNoiseLevels()
        {
            db.Database.EnsureCreated();
            db.ChangeTracker.Clear();
            db.NoiseLevels.Load();
            return db.NoiseLevels.ToList();
        }
        public void Add(NoiseLevel noiseLevel)
        {
            db.Database.EnsureCreated();
            db.NoiseLevels.Load();
            db.NoiseLevels.Add(noiseLevel);
            db.SaveChanges();
        }
        public void Remove(NoiseLevel noiseLevel)
        {
            using (var db = new AppContext()) // Новый контекст
            {
                db.Database.EnsureCreated();
                db.NoiseLevels.Remove(noiseLevel);
                db.SaveChanges();
            }
        }

        public void Update(int id, NoiseLevel noiseLevel)
        {
            db.Database.EnsureCreated();
            db.NoiseLevels.Load();
            var existingNoiseLevel = db.NoiseLevels.FirstOrDefault(s => s.Id == id);

            if (existingNoiseLevel != null)
            {
                existingNoiseLevel.Ush = noiseLevel.Ush;
                existingNoiseLevel.Name = noiseLevel.Name;
                db.SaveChanges();
            }
        }
    }
}
