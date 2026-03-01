using Calculator.Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.Class
{
    public class RoomDBHelp
    {
        AppContext db = new AppContext();

        public List<Room> GetAllRooms()
        {
            db.Database.EnsureCreated();
            db.ChangeTracker.Clear();
            db.Rooms.Load();
            return db.Rooms.ToList();
        }
        public Room GetRoom(int id)
        {
            var list = GetAllRooms();
            return list.Where(x => x.Id == id).FirstOrDefault();
        }
        public void Add(Room room)
        {
            db.Database.EnsureCreated();
            db.Rooms.Load();
            db.Rooms.Add(room);
            db.SaveChanges();
        }
        public void Remove(Room room)
        {
            using (var db = new AppContext()) // Новый контекст
            {
                db.Database.EnsureCreated();
                db.Rooms.Remove(room);
                db.SaveChanges();
            }
        }

        public void Update(int id, Room room)
        {
            db.Database.EnsureCreated();
            db.Rooms.Load();
            var existingRoom = db.Rooms.FirstOrDefault(s => s.Id == id);

            if (existingRoom != null)
            {
                existingRoom.USH_p = room.USH_p;
                existingRoom.a = room.a;
                existingRoom.b_ = room.b_;
                existingRoom.h_ = room.h_;
                existingRoom.N = room.N;
                existingRoom.a1 = room.a1;
                existingRoom.t_r = room.t_r;
                existingRoom.AccConstId = room.AccConstId;
                existingRoom.ApprId = room.ApprId;
                existingRoom.Name = room.Name;
                existingRoom.Surfaces = room.Surfaces;
                existingRoom.NoiseLevel = room.NoiseLevel;
                db.SaveChanges();
            }
        }
    }
}
