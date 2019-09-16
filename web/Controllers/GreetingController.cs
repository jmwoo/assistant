using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assistant.Features.Greeting.Models;
using Assistant.Features.Greeting.Services;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    [Route("greet")]
    public class GreetingController : Controller
    {
        readonly IGreetingService _greetingService;

        public GreetingController(
            IGreetingService greetingService
        )
        {
            _greetingService = greetingService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetIndex()
        {
            var greeting = await _greetingService.GetGreeting(new GetGreetingRequest
            {
                Zip = "92117",
                Name = "Jimmy",
                OpeningMsg = true,
                CurrentDate = true,
                Weather = true,
                CurrentSurf = true,
                WaterTemp = true,
                NewsHeadlines = true
            });

            return Content(greeting.Message);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostIndex([FromBody] GetGreetingRequest req)
        {
            var greeting = await _greetingService.GetGreeting(req);

            return Json(greeting);
        }
    }
}
