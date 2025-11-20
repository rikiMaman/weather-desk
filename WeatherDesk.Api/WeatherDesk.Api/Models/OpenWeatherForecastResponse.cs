using System.Runtime;

namespace WeatherDesk.Api.Models
{
    
    public class OpenWeatherForecastResponse
    {

        public List<WeatherItem> List { get; set; } = new();
        public CityInfo City { get; set; } = new();

        //public string Cod { get; set; } = "";
        //public int Cnt { get; set; }
        //public List<WeatherItem> List { get; set; } = new();
    }

    public class CityInfo
    {
        public string Name { get; set; } = "";
        public string Country { get; set; } = "";
        public Coord Coord { get; set; } = new();
    }

    public class Coord
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
    public class WeatherItem
    {
        public string DtTxt { get; set; } = "";
        public MainData Main { get; set; } = new();
        public List<WeatherDescription> Weather { get; set; } = new();

        // JSON מזהה מפתח "dt_txt" או "dt" => חשוב לכתוב JsonPropertyName
        [System.Text.Json.Serialization.JsonPropertyName("dt_txt")]
        public string DtTxtJson
        {
            get => DtTxt;
            set => DtTxt = value;
        }
    }

    public class MainData
    {
        [System.Text.Json.Serialization.JsonPropertyName("temp")]
        public double Temp { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("temp_min")]
        public double TempMin { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("temp_max")]
        public double TempMax { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; } = "";
        public string Icon { get; set; } = "";
    }

}
