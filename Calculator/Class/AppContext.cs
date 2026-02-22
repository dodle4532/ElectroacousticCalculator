using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Class
{
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    public class AppContext : DbContext
    {
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Calculation> Calculations { get; set; }
        public DbSet<NoiseLevel> NoiseLevels { get; set; }
        public DbSet<Surface> Surfaces { get; set; }
        public DbSet<ApprSoundAbsorp> ApprSoundAbsorps { get; set; }
        public DbSet<AcousticConstant> AcousticConstants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}



