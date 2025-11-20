//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//using WeatherDesk.WPF.Models;
//using WeatherDesk.WPF.Services;
//using WeatherDesk.WPF.Commands;


//namespace WeatherDesk.WPF.ViewModels
//{
//    public class MainViewModel : BaseViewModel
//    {
//        private readonly ApiService _api;
//        private readonly CacheService _cache;

//        public ObservableCollection<WeatherForecast> Forecasts { get; set; } = new();

//        private string _cityName;
//        public string CityName
//        {
//            get => _cityName;
//            set { _cityName = value; OnPropertyChanged(); }
//        }

//        public ICommand SearchCommand { get; }

//        public MainViewModel()
//        {
//            _api = new ApiService();
//            _cache = new CacheService();
//            SearchCommand = new RelayCommand(async _ => await SearchWeatherAsync());
//        }

//        private async Task SearchWeatherAsync()
//        {
//            if (string.IsNullOrWhiteSpace(CityName)) return;

//            if (!_cache.TryGetFromCache(CityName, out var weather))
//            {
//                weather = await _api.GetWeatherAsync(CityName);
//                _cache.SaveToCache(CityName, weather);
//            }

//            Forecasts.Clear();
//            foreach (var f in weather.Forecast)
//                Forecasts.Add(f);
//        }
//    }

//}


//using System;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using WeatherDesk.WPF.Models;
//using WeatherDesk.WPF.Services;
//using WeatherDesk.WPF.Commands;

//namespace WeatherDesk.WPF.ViewModels
//{
//    public class MainViewModel : BaseViewModel
//    {
//        private readonly ApiService _api;
//        private readonly CacheService _cache;

//        public ObservableCollection<WeatherForecast> Forecasts { get; set; } = new();
//        public ObservableCollection<string> RecentSearches { get; set; } = new();
//        public ObservableCollection<string> Favorites { get; set; } = new();

//        private string _cityName;
//        public string CityName
//        {
//            get => _cityName;
//            set { _cityName = value; OnPropertyChanged(); }
//        }

//        public ICommand SearchCommand { get; }
//        public ICommand AddToFavoritesCommand { get; }

//        public MainViewModel()
//        {
//            _api = new ApiService();
//            _cache = new CacheService();

//            SearchCommand = new RelayCommand(async _ => await SearchWeatherAsync());
//            AddToFavoritesCommand = new RelayCommand(_ => AddToFavorites());
//        }

//        private async Task SearchWeatherAsync()
//        {
//            if (string.IsNullOrWhiteSpace(CityName)) return;

//            // חיפוש תחזית
//            var weather = await _api.GetWeatherAsync(CityName);

//            // ניקוי הרשימה והצגה חדשה
//            Forecasts.Clear();
//            foreach (var f in weather.Forecast)
//                Forecasts.Add(f);

//            // שמירה במסד הנתונים
//            foreach (var f in weather.Forecast)
//            {
//                f.City = CityName;
//                _cache.SaveForecast(f);
//            }

//            // עדכון רשימת חיפושים אחרונים
//            UpdateRecentSearches(CityName);
//        }

//        private void UpdateRecentSearches(string city)
//        {
//            if (RecentSearches.Contains(city, StringComparer.OrdinalIgnoreCase))
//                return;

//            RecentSearches.Insert(0, city);
//            if (RecentSearches.Count > 25)
//                RecentSearches.RemoveAt(RecentSearches.Count - 1);
//        }

//        private void AddToFavorites()
//        {
//            if (string.IsNullOrWhiteSpace(CityName)) return;
//            if (!Favorites.Contains(CityName, StringComparer.OrdinalIgnoreCase))
//                Favorites.Add(CityName);
//        }
//    }
//}


//using System;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using WeatherDesk.WPF.Models;
//using WeatherDesk.WPF.Services;
//using WeatherDesk.WPF.Commands;

//namespace WeatherDesk.WPF.ViewModels
//{
//    public class MainViewModel : BaseViewModel
//    {
//        private readonly ApiService _api;
//        private readonly CacheService _cache;

//        public ObservableCollection<WeatherForecast> Forecasts { get; set; } = new();
//        public ObservableCollection<string> RecentSearches { get; set; } = new();
//        public ObservableCollection<string> Favorites { get; set; } = new();

//        private string _cityName;
//        public string CityName
//        {
//            get => _cityName;
//            set { _cityName = value; OnPropertyChanged(); }
//        }

//        private string _selectedFavorite;
//        public string SelectedFavorite
//        {
//            get => _selectedFavorite;
//            set
//            {
//                _selectedFavorite = value;
//                OnPropertyChanged();
//                if (!string.IsNullOrWhiteSpace(_selectedFavorite))
//                {
//                    CityName = _selectedFavorite;
//                    _ = SearchWeatherAsync(); // להפעיל חיפוש עבור הפריט הנבחר
//                }
//            }
//        }

//        private string _selectedRecent;
//        public string SelectedRecent
//        {
//            get => _selectedRecent;
//            set
//            {
//                _selectedRecent = value;
//                OnPropertyChanged();
//                if (!string.IsNullOrWhiteSpace(_selectedRecent))
//                {
//                    CityName = _selectedRecent;
//                    _ = SearchWeatherAsync();
//                }
//            }
//        }

//        public ICommand SearchCommand { get; }
//        public ICommand AddToFavoritesCommand { get; }
//        public ICommand RemoveFavoriteCommand { get; }
//        public ICommand ClearRecentCommand { get; }

//        public MainViewModel()
//        {
//            _api = new ApiService();
//            _cache = new CacheService();

//            SearchCommand = new RelayCommand(async _ => await SearchWeatherAsync());
//            AddToFavoritesCommand = new RelayCommand(_ => AddToFavorites());
//            RemoveFavoriteCommand = new RelayCommand(_ => RemoveSelectedFavorite());
//            ClearRecentCommand = new RelayCommand(_ => RecentSearches.Clear());
//        }

//        private async Task SearchWeatherAsync()
//        {
//            if (string.IsNullOrWhiteSpace(CityName)) return;

//            // ננסה מה-cache (זיכרון) קודם, לפי המימוש שלך
//            // אם אין — נבקש מה-API, ואז נשמור בזיכרון וב-DB
//            WeatherResponse weatherResponse = null;

//            //var cached = _cacheService.TryGetFromCache(CityName);

//            // אם ה-Cache שלך תומך ב-TryGetFromCache(string, out WeatherResponse)
//            if (_cache.TryGetFromCache(CityName, out var cached))
//            {
//                weatherResponse = cached;
//            }
//            else
//            {
//                weatherResponse = await _api.GetWeatherAsync(CityName);
//                if (weatherResponse != null)
//                    _cache.SaveToCache(CityName, weatherResponse);
//            }

//            if (weatherResponse?.Forecast != null)
//            {
//                Forecasts.Clear();
//                foreach (var f in weatherResponse.Forecast)
//                {
//                    // וודאי שהמודל שלך תומך בשדה City
//                    f.City = string.IsNullOrWhiteSpace(f.City) ? CityName : f.City;

//                    Forecasts.Add(f);

//                    // שמירה ל-SQLite (DB מקומי) של כל תחזית
//                    // ודאי ש־CacheService/WeatherDbContext תומכים ב-SaveForecast
//                    _cache.SaveForecast(f);
//                }

//                UpdateRecentSearches(CityName);
//            }
//        }

//        private void UpdateRecentSearches(string city)
//        {
//            if (string.IsNullOrWhiteSpace(city)) return;

//            // נמנע כפילויות (רגיש ללא רגישות לאותיות)
//            var existing = RecentSearches.FirstOrDefault(x => string.Equals(x, city, StringComparison.OrdinalIgnoreCase));
//            if (existing != null)
//            {
//                RecentSearches.Remove(existing);
//            }

//            RecentSearches.Insert(0, city);

//            // שמירה לגודל 25
//            while (RecentSearches.Count > 25)
//                RecentSearches.RemoveAt(RecentSearches.Count - 1);
//        }

//        private void AddToFavorites()
//        {
//            if (string.IsNullOrWhiteSpace(CityName)) return;

//            if (!Favorites.Any(x => string.Equals(x, CityName, StringComparison.OrdinalIgnoreCase)))
//                Favorites.Add(CityName);
//        }

//        private void RemoveSelectedFavorite()
//        {
//            if (string.IsNullOrWhiteSpace(SelectedFavorite)) return;
//            Favorites.Remove(SelectedFavorite);
//            SelectedFavorite = null;
//        }
//    }
//}



using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherDesk.WPF.Models;
using WeatherDesk.WPF.Services;
using WeatherDesk.WPF.Commands;
using WeatherDesk.WPF.Data;

namespace WeatherDesk.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ApiService _api;
        private readonly CacheService _cache;

        public ObservableCollection<WeatherForecast> Forecasts { get; set; } = new();
        public ObservableCollection<CityHistory> History { get; set; } = new();
        public ObservableCollection<CityHistory> Favorites { get; set; } = new();

        private string _cityName;
        public string CityName
        {
            get => _cityName;
            set { _cityName = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }

        public MainViewModel()
        {
            _api = new ApiService();
            _cache = new CacheService();

            SearchCommand = new RelayCommand(async _ => await SearchWeatherAsync());
            ToggleFavoriteCommand = new RelayCommand(_ => ToggleFavorite());

            LoadHistoryAndFavorites();
        }

        private async Task SearchWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(CityName)) return;

            // נבדוק קודם Cache בזיכרון
            var cachedForecasts = _cache.TryGetFromCache(CityName);

            List<WeatherForecast> forecasts;

            if (cachedForecasts != null)
            {
                forecasts = cachedForecasts;
            }
            else
            {
                var response = await _api.GetWeatherAsync(CityName);
                if (response == null || response.Forecast == null) return;

                forecasts = response.Forecast;
                _cache.SaveToCache(CityName, forecasts);
                _cache.AddCityToHistory(CityName);
            }

            // עדכון רשימת תחזיות על המסך
            Forecasts.Clear();
            foreach (var f in forecasts)
                Forecasts.Add(f);

            // טען היסטוריה ומועדפים מחדש
            LoadHistoryAndFavorites();
        }

        private void LoadHistoryAndFavorites()
        {
            History.Clear();
            foreach (var h in _cache.GetHistory())
                History.Add(h);

            Favorites.Clear();
            foreach (var f in _cache.GetFavorites())
                Favorites.Add(f);
        }

        private void ToggleFavorite()
        {
            if (string.IsNullOrWhiteSpace(CityName)) return;
            _cache.ToggleFavorite(CityName);
            LoadHistoryAndFavorites();
        }
    }
}
