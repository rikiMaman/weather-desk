namespace WeatherDesk.Api.Models
{
    public class WeatherResult
    {
        public string CityName { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }

    //public class WeatherResult
    //{
    //    public List<WeatherInfo> Weather { get; set; } = new();
    //    public MainInfo Main { get; set; } = new();
    //    public WindInfo Wind { get; set; } = new();
    //}

    public class WeatherInfo
    {
        public string Description { get; set; } = string.Empty;
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
    }
}
