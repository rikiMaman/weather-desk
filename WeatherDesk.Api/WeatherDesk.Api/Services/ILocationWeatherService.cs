using WeatherDesk.Api.Models;

namespace WeatherDesk.Api.Services
{
    public interface ILocationWeatherService
    {
        Task<LocationWeatherResult?> GetLocationAndWeatherAsync(string address);
        Task<LocationWeatherResult?> GetWeatherByCoordinatesAsync(double lat, double lng);
    }
}
