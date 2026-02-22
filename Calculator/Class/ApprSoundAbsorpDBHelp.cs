using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    public class ApprSoundAbsorpDBHelp
    {
        AppContext db = new AppContext();

        public List<ApprSoundAbsorp> GetAllApprSoundAbsorps()
        {
            db.Database.EnsureCreated();
            db.ChangeTracker.Clear();
            db.ApprSoundAbsorps.Load();
            return db.ApprSoundAbsorps.ToList();
        }
        public void Add(ApprSoundAbsorp appr)
        {
            db.Database.EnsureCreated();
            db.ApprSoundAbsorps.Load();
            db.ApprSoundAbsorps.Add(appr);
            db.SaveChanges();
        }
        public void Remove(ApprSoundAbsorp appr)
        {
            using (var db = new AppContext()) // Новый контекст
            {
                db.Database.EnsureCreated();
                db.ApprSoundAbsorps.Remove(appr);
                db.SaveChanges();
            }
        }

        public void Update(int id, ApprSoundAbsorp appr)
        {
            db.Database.EnsureCreated();
            db.ApprSoundAbsorps.Load();
            var existingApprSoundAbsorp = db.ApprSoundAbsorps.FirstOrDefault(s => s.Id == id);

            if (existingApprSoundAbsorp != null)
            {
                existingApprSoundAbsorp.A1 = appr.A1;
                existingApprSoundAbsorp.Name = appr.Name;
                db.SaveChanges();
            }
        }
    }
}
