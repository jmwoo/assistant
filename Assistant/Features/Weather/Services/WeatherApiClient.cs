using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Jmwoo.Core.Http;
using Assistant.Features.Weather.Models;
using Microsoft.Extensions.Configuration;

namespace Assistant.Features.Weather.Services
{
    public interface IWeatherApiClient
    {
        Task<ForecastResult> GetForecastByZip(string zip);
        Task<WeatherResult> GetCurrentWeatherByZip(string zip);
    }

    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IForecastCache _forecastCache;
        private readonly ICurrentWeatherCache _currentWeatherCache;
        private readonly string _apiKey;
        private const string DOMAIN = "api.openweathermap.org";
        private const string OpenWeatherMapConfigKey = "Features:Weather:OpenWeatherMapApiKey";

        public WeatherApiClient(
            IHttpClientFactory httpClientFactory
            ,IForecastCache forecastCache
            ,ICurrentWeatherCache currentWeatherCache
            ,IConfiguration config
        )
        {
            _httpClientFactory = httpClientFactory;
            _forecastCache = forecastCache;
            _currentWeatherCache = currentWeatherCache;
            _apiKey = config[OpenWeatherMapConfigKey];

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception($"No OpenWeatherMap api key found, place key in {OpenWeatherMapConfigKey}");
            }
        }

        public async Task<ForecastResult> GetForecastByZip(string zip) =>
            await _forecastCache.Get(zip) ??
            await GetForecastFromService(zip);

        public async Task<WeatherResult> GetCurrentWeatherByZip(string zip) =>
            await _currentWeatherCache.Get(zip) ??
            await GetCurrentWeatherFromService(zip);

        private Dictionary<string, string> DefaultParams => new Dictionary<string, string>
        {
            { "APPID", _apiKey },
            { "units", "imperial" }
        };

        private async Task<ForecastResult> GetForecastFromService(string zip)
        {
            var p = DefaultParams;
            p.Add("zip", $"{zip}");

            var url = HttpHelpers.BuildUrl(DOMAIN, "/data/2.5/forecast", queryParams: p, useHttps: false);
            var result = await _httpClientFactory.CreateClient().SendAndReceiveAs<ForecastResult>(HttpMethod.Get, url);

            await _forecastCache.Set(result, zip);

            return result;
        }

        private async Task<WeatherResult> GetCurrentWeatherFromService(string zip)
        {
            var p = DefaultParams;
            p.Add("zip", $"{zip}");

            var url = HttpHelpers.BuildUrl(DOMAIN, "/data/2.5/weather", queryParams: p, useHttps: false);
            var result = await _httpClientFactory.CreateClient().SendAndReceiveAs<WeatherResult>(HttpMethod.Get, url);

            await _currentWeatherCache.Set(result, zip);

            return result;
        }
    }
}
