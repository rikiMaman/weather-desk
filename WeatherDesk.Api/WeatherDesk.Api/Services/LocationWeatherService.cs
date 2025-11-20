using WeatherDesk.Api.Models;

namespace WeatherDesk.Api.Services
{
    public class LocationWeatherService : ILocationWeatherService
    {
        private readonly GoogleMapsService mapsService;
        private readonly WeatherService weatherService;

        public LocationWeatherService(GoogleMapsService mapsService, WeatherService weatherService)
        {
            this.mapsService = mapsService;
            this.weatherService = weatherService;
        }

        public async Task<LocationWeatherResult?> GetLocationAndWeatherAsync(string address)
        {
            var mapsResult = await mapsService.GetCoordinatesAsync(address);

            if (mapsResult == null || mapsResult.Status != "OK" || mapsResult.Results.Count == 0)
                return null;

            var resultLocation = mapsResult.Results[0];
            var location = resultLocation.Geometry.Location;

            // תחזית ל-5 ימים
            var weatherForecast = await weatherService.GetWeatherForecastAsync(location.Lat, location.Lng);

            return new LocationWeatherResult
            {
                Address = resultLocation.FormattedAddress ?? address,
                Coordinates = new Coordinates
                {
                    Latitude = location.Lat,
                    Longitude = location.Lng
                },
                Forecast = weatherForecast.ToList()
            };
        }

        public async Task<LocationWeatherResult?> GetWeatherByCoordinatesAsync(double lat, double lng)
        {
            var weatherForecast = await weatherService.GetWeatherForecastAsync(lat, lng);

            return new LocationWeatherResult
            {
                Address = "",
                Coordinates = new Coordinates { Latitude = lat, Longitude = lng },
                Forecast = weatherForecast.ToList()
            };
        }


        //public async Task<object?> GetLocationAndWeatherAsync(string address)
        //{
        //    var mapsResult = await mapsService.GetCoordinatesAsync(address);

        //    if (mapsResult == null || mapsResult.Status != "OK" || mapsResult.Results.Count == 0)
        //        return null;

        //    var resultLocation = mapsResult.Results[0];
        //    var location = resultLocation.Geometry.Location;

        //    // תחזית ל-5 ימים
        //    var weatherForecast = await weatherService.GetWeatherForecastAsync(location.Lat, location.Lng);

        //    return new
        //    {
        //        Address = resultLocation.FormattedAddress ?? address,
        //        Coordinates = new
        //        {
        //            Latitude = location.Lat,
        //            Longitude = location.Lng
        //        },
        //        GoogleMapsFull = mapsResult,  // החזרת כל האובייקט המלא של Google Maps
        //        WeatherForecast = weatherForecast
        //    };
        //}

        //public async Task<object?> GetWeatherByCoordinatesAsync(double lat, double lng)
        //{
        //    var weatherForecast = await weatherService.GetWeatherForecastAsync(lat, lng);

        //    return new
        //    {
        //        Address = "",
        //        Coordinates = new { Latitude = lat, Longitude = lng },
        //        WeatherForecast = weatherForecast
        //    };
        //}
    }
}
