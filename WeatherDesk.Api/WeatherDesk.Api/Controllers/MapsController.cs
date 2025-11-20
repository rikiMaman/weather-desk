using Microsoft.AspNetCore.Mvc;
using WeatherDesk.Api.Models;
using WeatherDesk.Api.Services;

namespace WeatherDesk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapsController : ControllerBase
    {
        private readonly ILocationWeatherService locationWeatherService;

        public MapsController(ILocationWeatherService locationWeatherService)
        {
            this.locationWeatherService = locationWeatherService;
        }

        [HttpGet("weather")]
        public async Task<ActionResult<LocationWeatherResult>> GetWeatherByAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return BadRequest("Address is required");

            var result = await locationWeatherService.GetLocationAndWeatherAsync(address);

            return result == null ? NotFound("Address not found") : Ok(result);
        }

        [HttpGet("coordinates")]
        public async Task<ActionResult<LocationWeatherResult>> GetWeatherByCoordinates(double lat, double lng)
        {
            var result = await locationWeatherService.GetWeatherByCoordinatesAsync(lat, lng);
            return Ok(result);
        }


        //[HttpGet("weather")]
        //public async Task<ActionResult<LocationWeatherResult>> GetWeatherByAddress(string address)
        //{
        //    if (string.IsNullOrWhiteSpace(address))
        //        return BadRequest("Address is required");

        //    var result = await locationWeatherService.GetLocationAndWeatherAsync(address);



        //    return result == null ? NotFound("Address not found") : Ok(result);
        //}

        //[HttpGet("coordinates")]
        //public async Task<ActionResult<LocationWeatherResult>> GetWeatherByCoordinates(double lat, double lng)
        //{
        //    var result = await locationWeatherService.GetWeatherByCoordinatesAsync(lat, lng);
        //    return Ok(result);
        //}
    }
}
