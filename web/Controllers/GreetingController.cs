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
        public async Task<IActionResult> GetIndex([FromQuery] GreetingRequest req)
        {
            var greeting = await _greetingService.GetGreeting(req);
            return Content(greeting.Message);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostIndex([FromBody] GreetingRequest req)
        {
            var greeting = await _greetingService.GetGreeting(req);

            return Json(greeting);
        }
    }
}
