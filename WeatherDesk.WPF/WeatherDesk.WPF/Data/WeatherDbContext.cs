//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WeatherDesk.WPF.Models;

//namespace WeatherDesk.WPF.Data
//{

//    public class WeatherDbContext : DbContext
//    {
//        public DbSet<WeatherForecast> Forecasts { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlite("Data Source=weatherdesk.db");
//        }
//    }
//}


using Microsoft.EntityFrameworkCore;
using System;
using WeatherDesk.WPF.Models;

namespace WeatherDesk.WPF.Data
{
    public class WeatherDbContext : DbContext
    {
        // טבלת תחזיות מזג האוויר הקיימת שלך
        public DbSet<WeatherForecast> Forecasts { get; set; }

        // טבלת היסטוריה של ערים
        public DbSet<CityHistory> CityHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = @"C:\Users\אסתר\Desktop\WeatherDesk.WPF\WeatherDesk.WPF\weatherdesk.db";
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // SQLite מקומי – שמור בקובץ weatherdesk.db
        //    optionsBuilder.UseSqlite("Data Source=weatherdesk.db");
        //}
    }

    // ✅ מודל לשמירת היסטוריית ערים ומועדפים
    public class CityHistory
    {
        public int Id { get; set; }

        // שם העיר
        public string CityName { get; set; }

        // זמן עדכון אחרון
        public DateTime LastUpdated { get; set; }

        // האם העיר היא מועדפת
        public bool IsFavorite { get; set; }
    }
}
