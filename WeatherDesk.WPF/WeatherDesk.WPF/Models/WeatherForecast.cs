using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDesk.WPF.Models
{
    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string CityName { get; set; }

        //public double TemperatureF => 32 + (Temperature / 0.5556);
    }

}
