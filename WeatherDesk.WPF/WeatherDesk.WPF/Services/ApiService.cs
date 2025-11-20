using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherDesk.WPF.Models;
using System.Collections.Generic;

namespace WeatherDesk.WPF.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7101/")
            };
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            return await _http.GetFromJsonAsync<WeatherResponse>($"api/Maps/weather?address={city}");
        }
    }

    public class WeatherResponse
    {
        public string Address { get; set; }
        public List<WeatherForecast> Forecast { get; set; }
    }
}





