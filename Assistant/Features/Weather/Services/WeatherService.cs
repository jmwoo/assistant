using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assistant.Features.ReadableDateTime.Services;
using Assistant.Features.Weather.Models;

namespace Assistant.Features.Weather.Services
{
    public interface IWeatherService
    {
        Task<string> GetWeatherMessage(string zip);
    }
    
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherApiClient _weatherApiClient;

        private readonly IReadableDateTimeService _readableDateTimeService;

        public WeatherService(IWeatherApiClient weatherApiClient, IReadableDateTimeService readableDateTimeService)
        {
            _readableDateTimeService = readableDateTimeService;
            _weatherApiClient = weatherApiClient;
        }

        public async Task<string> GetWeatherMessage(string zip)
        {
            var currentWeatherTask = _weatherApiClient.GetCurrentWeatherByZip(zip);
            var forecastTask = _weatherApiClient.GetForecastByZip(zip);

            var sb = new StringBuilder();

            await Task.WhenAll(currentWeatherTask, forecastTask);

            sb.Append($"Right now the weather in {currentWeatherTask.Result.name} is {currentWeatherTask.Result.main.temp} degrees with {currentWeatherTask.Result.weather.First().description}. ");

            var laterWeather = forecastTask.Result.List.Skip(2).First();
            sb.Append(GetForecastInstanceMsg("At", GetWeatherChangePhrase(currentWeatherTask.Result.main.temp, laterWeather.Main.Temp), laterWeather));

            var lastWeather = forecastTask.Result.List.Skip(4).First();
            sb.Append(GetForecastInstanceMsg("By", GetWeatherChangePhrase(laterWeather.Main.Temp, lastWeather.Main.Temp), lastWeather));

            return sb.ToString();
        }

        public string GetWeatherChangePhrase(double prev, double current)
        {
            prev = Math.Floor(prev);
            current = Math.Floor(current);

            if (current > prev)
            {
                return "heat up to";
            }
            else if (prev > current)
            {
                return "cool down to";
            }

            return "stay at";
        }

        public string GetForecastInstanceMsg(string prefix, string weatherChangePhrase, ForecastList forecastInstance)
        {
            var readableLaterTime = _readableDateTimeService.Get(forecastInstance.DateTimePst);
            return $"{prefix} {readableLaterTime.Time} It will {weatherChangePhrase} {forecastInstance.Main.Temp} degrees with {forecastInstance.Weather.FirstOrDefault().Description}. ";
        }
    }
}