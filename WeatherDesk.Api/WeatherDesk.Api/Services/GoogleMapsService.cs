using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherDesk.Api.Models;
using WeatherDesk.Api.Options;

namespace WeatherDesk.Api.Services
{
    public class GoogleMapsService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration config;
        private string apiKey = "";
        //"AIzaSyAWFkJ0Qvi6K8l4J4iMXorP-hsOo2qRvho";

        public GoogleMapsService(HttpClient httpClient, IOptions<GoogleMapsOptions> options)
        {
            // pull from the file: appsettings.Development.json
            //apiKey = configuration["GoogleMaps:ApiKey"];
            this.httpClient = httpClient;
            //this.config = configuration;
            this.apiKey = options.Value.ApiKey ?? throw new InvalidOperationException("API key missing"); ;

        }

        public async Task<GoogleGeocodeResponse?> GetCoordinatesAsync(string address)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";

            var response = await httpClient.GetStringAsync(url);

            Console.WriteLine("Google Geocode Response:");
            Console.WriteLine(response);  // הדפסה למסוף

            return JsonSerializer.Deserialize<GoogleGeocodeResponse>(response);
        }



        //public async Task<GoogleGeocodeResponse?> GetCoordinatesAsync(string address)
        //{
        //    var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";

        //    var response = await httpClient.GetStringAsync(url);

        //    var result = JsonSerializer.Deserialize<GoogleGeocodeResponse>(response);

        //    // בדיקת שגיאות מצד גוגל
        //    if (result == null)
        //        throw new Exception("Failed to parse Google Maps response.");

        //    if (result.Status != "OK")
        //    {
        //        Console.WriteLine($"Google Maps API error: {result.Status}");
        //        return null; // יגרום ל־NotFound בקונטרולר
        //    }

        //    return result;
        //}
        //public async Task<GoogleGeocodeResponse?> GetCoordinatesAsync(string address)
        //{
        //    //apiKey = config["GoogleMaps:ApiKey"];
        //    string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";

        //    var response = await httpClient.GetStringAsync(url);

        //    return JsonSerializer.Deserialize<GoogleGeocodeResponse>(response);
        //}


        public async Task<string> GetLocationAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("Google Maps API key is not configured.");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address must be provided.", nameof(address));

            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={apiKey}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "WeatherDeskApp");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }




}



