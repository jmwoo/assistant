using System;
using System.Threading.Tasks;
using Assistant.Features.News;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    [Route("api/news")]
    public class NewsController : Controller
    {
        readonly INewsService _newsService;

        public NewsController(
            INewsService newsService
        )
        {
            _newsService = newsService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var headlines = await _newsService.GetHeadlines();
            return Json(headlines);
        }
    }
}
