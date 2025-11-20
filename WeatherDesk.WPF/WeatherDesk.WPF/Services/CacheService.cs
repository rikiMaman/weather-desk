using System;
using System.Collections.Generic;
using System.Linq;
using WeatherDesk.WPF.Data;
using WeatherDesk.WPF.Models;

namespace WeatherDesk.WPF.Services
{
    public class CacheService
    {
        private readonly WeatherDbContext _context = new();

        private static readonly Dictionary<string, (DateTime timestamp, List<WeatherForecast> data)> _cache = new();

        public void SaveToCache(string city, List<WeatherForecast> forecasts)
        {
            _cache[city] = (DateTime.Now, forecasts);
        }

        public List<WeatherForecast>? TryGetFromCache(string city)
        {
            if (_cache.TryGetValue(city, out var entry))
            {
                if ((DateTime.Now - entry.timestamp).TotalHours < 4)
                    return entry.data;
            }
            return null;
        }

        public void AddCityToHistory(string city)
        {
            var existing = _context.CityHistories.FirstOrDefault(c => c.CityName == city);
            if (existing != null)
            {
                existing.LastUpdated = DateTime.Now;
            }
            else
            {
                if (_context.CityHistories.Count() >= 25)
                {
                    var oldest = _context.CityHistories.OrderBy(c => c.LastUpdated).First();
                    _context.CityHistories.Remove(oldest);
                }

                _context.CityHistories.Add(new CityHistory
                {
                    CityName = city,
                    LastUpdated = DateTime.Now,
                    IsFavorite = false
                });
            }
            _context.SaveChanges();
        }

        public List<CityHistory> GetHistory() =>
            _context.CityHistories.OrderByDescending(c => c.LastUpdated).ToList();

        public void ToggleFavorite(string city)
        {
            var cityRec = _context.CityHistories.FirstOrDefault(c => c.CityName == city);
            if (cityRec != null)
            {
                if (!cityRec.IsFavorite && _context.CityHistories.Count(c => c.IsFavorite) >= 8)
                    return;

                cityRec.IsFavorite = !cityRec.IsFavorite;
                _context.SaveChanges();
            }
        }

        public List<CityHistory> GetFavorites() =>
            _context.CityHistories.Where(c => c.IsFavorite)
            .OrderByDescending(c => c.LastUpdated)
            .Take(8)
            .ToList();
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WeatherDesk.WPF.Data;
//using WeatherDesk.WPF.Models;
//using WeatherDesk.WPF.Services;

//namespace WeatherDesk.WPF.Services
//{

//    public class CacheService
//    {
//        public void SaveForecast(WeatherForecast forecast)
//        {
//            using var db = new WeatherDbContext();
//            db.Database.EnsureCreated();
//            db.Forecasts.Add(forecast);
//            db.SaveChanges();
//        }

//        public WeatherForecast? GetLastForecast()
//        {
//            using var db = new WeatherDbContext();
//            return db.Forecasts.OrderByDescending(f => f.Id).FirstOrDefault();
//        }
//    }
//}





//using System.Linq;
//using WeatherDesk.WPF.Data;
//using WeatherDesk.WPF.Models;

//namespace WeatherDesk.WPF.Services
//{
//    public class CacheService
//    {
//        public void SaveToCache(WeatherForecast forecast)
//        {
//            using var db = new WeatherDbContext();
//            db.Database.EnsureCreated();
//            db.Forecasts.Add(forecast);
//            db.SaveChanges();
//        }

//        public bool TryGetFromCache(out WeatherForecast forecast)
//        {
//            using var db = new WeatherDbContext();
//            forecast = db.Forecasts
//                         .OrderByDescending(f => f.Id)
//                         .FirstOrDefault();

//            return forecast != null;
//        }

//        // גרסה פשוטה אם רוצים שליפה בלי out
//        public WeatherForecast GetLastForecast()
//        {
//            using var db = new WeatherDbContext();
//            return db.Forecasts
//                     .OrderByDescending(f => f.Id)
//                     .FirstOrDefault();
//        }
//    }
//}



//using System.Linq;
//using WeatherDesk.WPF.Data;
//using WeatherDesk.WPF.Models;

//namespace WeatherDesk.WPF.Services
//{
//    public class CacheService
//    {
//        public void SaveForecast(WeatherForecast forecast)
//        {
//            using var db = new WeatherDbContext();
//            db.Database.EnsureCreated();
//            db.Forecasts.Add(forecast);
//            db.SaveChanges();
//        }

//        public WeatherForecast? GetLastForecast()
//        {
//            using var db = new WeatherDbContext();
//            return db.Forecasts.OrderByDescending(f => f.Id).FirstOrDefault();
//        }
//    }
//}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using WeatherDesk.WPF.Data;
//using WeatherDesk.WPF.Models;

//namespace WeatherDesk.WPF.Services
//{
//    public class CacheService
//    {
//        private readonly WeatherDbContext _context = new();

//        // ✅ מטמון בזיכרון ל־4 שעות
//        private static readonly Dictionary<string, (DateTime timestamp, List<WeatherForecast> data)> _cache = new();

//        // שמירה במטמון
//        public void SaveToCache(string city, List<WeatherForecast> forecasts)
//        {
//            _cache[city] = (DateTime.Now, forecasts);
//        }

//        // ניסיון לשלוף ממטמון אם לא עברו 4 שעות
//        public List<WeatherForecast>? TryGetFromCache(string city)
//        {
//            if (_cache.TryGetValue(city, out var entry))
//            {
//                if ((DateTime.Now - entry.timestamp).TotalHours < 4)
//                    return entry.data;
//            }
//            return null;
//        }

//        // ✅ שמירת היסטוריית ערים (מקסימום 25)
//        public void AddCityToHistory(string city)
//        {
//            var existing = _context.CityHistories.FirstOrDefault(c => c.CityName == city);
//            if (existing != null)
//            {
//                existing.LastUpdated = DateTime.Now;
//            }
//            else
//            {
//                if (_context.CityHistories.Count() >= 25)
//                {
//                    var oldest = _context.CityHistories.OrderBy(c => c.LastUpdated).First();
//                    _context.CityHistories.Remove(oldest);
//                }

//                _context.CityHistories.Add(new CityHistory
//                {
//                    CityName = city,
//                    LastUpdated = DateTime.Now,
//                    IsFavorite = false
//                });
//            }
//            _context.SaveChanges();
//        }

//        // ✅ שליפת רשימת ההיסטוריה
//        public List<CityHistory> GetHistory()
//        {
//            return _context.CityHistories
//                .OrderByDescending(c => c.LastUpdated)
//                .ToList();
//        }

//        // ✅ הוספה או הסרה ממועדפים (עד 8)
//        public void ToggleFavorite(string city)
//        {
//            var cityRec = _context.CityHistories.FirstOrDefault(c => c.CityName == city);
//            if (cityRec != null)
//            {
//                if (!cityRec.IsFavorite && _context.CityHistories.Count(c => c.IsFavorite) >= 8)
//                    return; // לא ניתן להוסיף יותר מ־8

//                cityRec.IsFavorite = !cityRec.IsFavorite;
//                _context.SaveChanges();
//            }
//        }

//        public List<CityHistory> GetFavorites()
//        {
//            return _context.CityHistories
//                .Where(c => c.IsFavorite)
//                .OrderByDescending(c => c.LastUpdated)
//                .Take(8)
//                .ToList();
//        }
//    }
//}


