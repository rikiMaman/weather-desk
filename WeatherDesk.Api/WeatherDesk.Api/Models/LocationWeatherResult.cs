using WeatherDesk.Api.Services;

namespace WeatherDesk.Api.Models
{

    public class LocationWeatherResult
    {
        public string Address { get; set; } = "";
        public Coordinates Coordinates { get; set; } = new Coordinates();
        //public WeatherResult Weather { get; set; } = new WeatherResult();
        public List<WeatherForecast> Forecast { get; set; } = new();
        public List<WeatherForecast> WeatherForecast { get; set; } = new();
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    //public class WeatherForecast
    //{
    //    public DateTime Date { get; set; }
    //    public double Temperature { get; set; }
    //    public double TempMin { get; set; }
    //    public double TempMax { get; set; }
    //    public string Description { get; set; } = "";
    //    public string Icon { get; set; } = "";
    //}

}
