using System.Text.Json.Serialization;

namespace WeatherDesk.Api.Models
{




    public class GoogleGeocodeResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("results")]
        public List<GoogleGeocodeResult> Results { get; set; } = new();
    }

    public class GoogleGeocodeResult
    {
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; } = string.Empty;

        [JsonPropertyName("geometry")]
        public GoogleGeometry Geometry { get; set; } = new();
    }

    public class GoogleGeometry
    {
        [JsonPropertyName("location")]
        public GoogleLocation Location { get; set; } = new();
    }

    public class GoogleLocation
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }
}
