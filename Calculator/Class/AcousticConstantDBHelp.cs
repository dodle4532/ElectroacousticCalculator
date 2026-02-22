using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    public class AcousticConstantDBHelp
    {
        AppContext db = new AppContext();

        public List<AcousticConstant> GetAllAcousticConstants()
        {
            db.Database.EnsureCreated();
            db.ChangeTracker.Clear();
            db.AcousticConstants.Load();
            return db.AcousticConstants.ToList();
        }
        public void Add(AcousticConstant constant)
        {
            db.Database.EnsureCreated();
            db.AcousticConstants.Load();
            db.AcousticConstants.Add(constant);
            db.SaveChanges();
        }
        public void Remove(AcousticConstant constant)
        {
            using (var db = new AppContext()) // Новый контекст
            {
                db.Database.EnsureCreated();
                db.AcousticConstants.Remove(constant);
                db.SaveChanges();
            }
        }

        public void Update(int id, AcousticConstant constant)
        {
            db.Database.EnsureCreated();
            db.AcousticConstants.Load();
            var existingAcousticConstant = db.AcousticConstants.FirstOrDefault(s => s.Id == id);

            if (existingAcousticConstant != null)
            {
                existingAcousticConstant.A1 = constant.A1;
                existingAcousticConstant.Name = constant.Name;
                db.SaveChanges();
            }
        }
    }
}
