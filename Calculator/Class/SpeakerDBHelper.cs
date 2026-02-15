using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Class
{
    /// <summary>
    /// Класс для работы с БД Громкоговорителей
    /// </summary>
    public class SpeakerDBHelper
    {
        AppContext db = new AppContext();

        public List<Speaker> GetAllSpeakers()
        {
            db.Database.EnsureCreated();
            db.Speakers.Load();
            return db.Speakers.ToList();
        }
        public void Add(Speaker speaker)
        {
            db.Database.EnsureCreated();
            db.Speakers.Load();
            db.Speakers.Add(speaker);
            db.SaveChanges();
        }
        public void Remove(Speaker speaker)
        {
            db.Database.EnsureCreated();
            db.Speakers.Load();
            db.Speakers.Remove(speaker);
            db.SaveChanges();
        }

        public void Update(int id, Speaker speaker)
        {
            db.Database.EnsureCreated();
            db.Speakers.Load();
            var existingSpeaker = db.Speakers.FirstOrDefault(s => s.Id == id);

            if (existingSpeaker != null)
            {
                existingSpeaker.Name = speaker.Name;
                existingSpeaker.P_01 = speaker.P_01;
                existingSpeaker.P_Vt = speaker.P_Vt;
                existingSpeaker.SHDN_g = speaker.SHDN_g;
                existingSpeaker.SHDN_v = speaker.SHDN_v;
                db.SaveChanges();
            }
        }
    }
}
