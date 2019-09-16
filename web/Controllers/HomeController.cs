using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Assistant.Features;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(
        )
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Test()
        {
            await Task.Yield();
            var obj = new {Hello = "Test!"};
            return Json(obj);
        }
    }
}
