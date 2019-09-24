using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jmwoo.Common.Date;
using Assistant.Features.Greeting.Models;
using Assistant.Features.News;
using Assistant.Features.ReadableDateTime.Services;
using Assistant.Features.Surf;
using Assistant.Features.Weather.Services;

namespace Assistant.Features.Greeting.Services
{
    public interface IGreetingService
    {
        Task<GreetingResult> GetGreeting(IGreetingRequest req);
    }

    public class GreetingService : IGreetingService
    {
        readonly IReadableDateTimeService _readableDateTimeService;
        readonly INewsService _newsService;
        private readonly ISurfService _surfService;

        private readonly IWeatherService _weatherService;

        public GreetingService(
            IReadableDateTimeService readableDateTimeService, INewsService newsService, ISurfService surfService, IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _surfService = surfService;
            _readableDateTimeService = readableDateTimeService;
            _newsService = newsService;
        }
        public async Task<GreetingResult> GetGreeting(IGreetingRequest req)
        {
            var now = DateTime.UtcNow.UtcToPst();
            var readableNow = _readableDateTimeService.Get(now);

            var msgTasks = new[] {
                req.OpeningMsg ? Task.FromResult($"Good {readableNow.PartOfDay} {req.Name}".Trim() + "!") : DefaultMsg(),
                req.CurrentDate ? Task.FromResult($"It's {readableNow.DayOfWeek} {readableNow.Month} {readableNow.DayOfMonthEnglish} {readableNow.Time}.") : DefaultMsg(),
                req.Weather ? _weatherService.GetWeatherMessage(req.Zip) : DefaultMsg(),
                req.CurrentSurf ? _surfService.GetCurrentSurfMessage(SurfSpot.BlacksBeach) : DefaultMsg(),
                req.WaterTemp ? _surfService.GetCurrentOceanTemperatureMessage(SurfCounty.SanDiego) : DefaultMsg(),
                req.NewsHeadlines ? _newsService.GetHeadlinesMessage() : DefaultMsg()
            };

            await Task.WhenAll(msgTasks);

            var msg = string.Join(" ", msgTasks.Select(t => t.Result));

            return new GreetingResult
            {
                Message = msg.Trim()
            };
        }

        private static Task<string> DefaultMsg() => Task.FromResult(string.Empty);
    }
}