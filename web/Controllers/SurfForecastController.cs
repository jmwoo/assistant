using System;
using System.Threading.Tasks;
using Assistant.Features.News;
using Assistant.Features.Surf;
using Assistant.Features.Weather.Services;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers {

  [Route ("api/surf")]
  public class SurfForecastController : Controller {

    private readonly ISurfService _surfService;

    public SurfForecastController (ISurfService surfService) {
      _surfService = surfService;
    }

    // public async Task<IActionResult> Get () {
    //   var headlines = await _newsService.GetHeadlines ();
    //   return Json (headlines);
    // }

    [HttpGet]
    [Route ("forecast")]
    public async Task<object> GetForecast () {
      return await _surfService.GetSurfForecast(SurfSpot.ScrippsPier);
    }
  }
}