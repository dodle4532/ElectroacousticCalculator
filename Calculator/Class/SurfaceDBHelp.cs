using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    public class SurfaceDBHelp
    {
        AppContext db = new AppContext();

        public List<Surface> GetAllSurfaces()
        {
            db.Database.EnsureCreated();
            db.ChangeTracker.Clear();
            db.Surfaces.Load();
            return db.Surfaces.ToList();
        }
        public void Add(Surface surface)
        {
            db.Database.EnsureCreated();
            db.Surfaces.Load();
            db.Surfaces.Add(surface);
            db.SaveChanges();
        }
        public void Remove(Surface surface)
        {
            using (var db = new AppContext()) // Новый контекст
            {
                db.Database.EnsureCreated();
                db.Surfaces.Remove(surface);
                db.SaveChanges();
            }
        }

        public void Update(int id, Surface surface)
        {
            db.Database.EnsureCreated();
            db.Surfaces.Load();
            var existingSurface = db.Surfaces.FirstOrDefault(s => s.Id == id);

            if (existingSurface != null)
            {
                existingSurface.Name = surface.Name;
                existingSurface.Ush = surface.Ush;
                existingSurface.F = surface.F;
                db.SaveChanges();
            }
        }
    }
}
