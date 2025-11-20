using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using WeatherDesk.Api.Models;
using WeatherDesk.Api.Options;

namespace WeatherDesk.Api.Services
{
    public class WeatherService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;

        public WeatherService(HttpClient httpClient, IOptions<OpenWeatherMapOptions> options)
        {
            this.httpClient = httpClient;
            this.apiKey = options.Value.ApiKey ?? throw new InvalidOperationException("API key missing");
        }

        public async Task<WeatherForecast[]> GetWeatherForecastAsync(double lat, double lng)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lng}&units=metric&appid={apiKey}&lang=he";
            var response = await httpClient.GetStringAsync(url);
            Console.WriteLine(response);

            var weatherData = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(
                response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return weatherData?.List.Select(w => new WeatherForecast
            {
                //Date = DateTime.Parse(w.DtTxt),
                Date = DateTime.TryParse(w.DtTxt, out var dt) ? dt : DateTime.MinValue,
                //w.DtTxt,
                Temperature = w.Main.Temp,
                TempMin = w.Main.TempMin,
                TempMax = w.Main.TempMax,
                Description = w.Weather.FirstOrDefault()?.Description ?? "",
                Icon = w.Weather.FirstOrDefault()?.Icon ?? "",
                CityName = weatherData.City.Name,
                Country = weatherData.City.Country,
                Latitude = weatherData.City.Coord.Lat,
                Longitude = weatherData.City.Coord.Lon
            }).ToArray() ?? Array.Empty<WeatherForecast>();
        }
    }
}


