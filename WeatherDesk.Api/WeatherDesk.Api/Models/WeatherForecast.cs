namespace WeatherDesk.Api.Models
{

    //public class WeatherForecast
    //{
    //    public DateTime Date { get; set; } // שונה מ-string ל-DateTime
    //    public double Temperature { get; set; }
    //    public double TempMin { get; set; }
    //    public double TempMax { get; set; }
    //    public string Description { get; set; } = "";
    //    public string Icon { get; set; } = "";

    //    // נוספים למיפוי ערים וקואורדינטות
    //    public string CityName { get; set; } = "";
    //    public string Country { get; set; } = "";
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //}
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public string Description { get; set; } = "";
        public string Icon { get; set; } = "";

        // חדשים:
        public string CityName { get; set; } = "";
        public string Country { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

  

}


