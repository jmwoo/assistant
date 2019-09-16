using System;
using System.Threading.Tasks;
using Assistant.Features.News;
using Assistant.Features.Weather.Services;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers {
  
  [Route ("api/weather")]
  public class WeatherController : Controller 
  {
    readonly IWeatherApiClient _weatherApiClient;

    public WeatherController(IWeatherApiClient weatherApiClient)
    {
      _weatherApiClient = weatherApiClient;
    }

    // public async Task<IActionResult> Get () {
    //   var headlines = await _newsService.GetHeadlines ();
    //   return Json (headlines);
    // }

    [HttpGet]
    [Route("forecast")]
    public async Task<object> GetForecast([FromQuery] string zip = "92117")
    {
      var forecast = await _weatherApiClient.GetForecastByZip(zip);
      return forecast;
    }
  }
}