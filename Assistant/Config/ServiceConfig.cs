using System;
using Assistant.Features;
using Assistant.Features.Age;
using Assistant.Features.Greeting.Services;
using Assistant.Features.News;
using Assistant.Features.ReadableDateTime.Services;
using Assistant.Features.Surf;
using Assistant.Features.Weather.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assistant.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection ConfigureAssistantServices(this IServiceCollection services, IConfiguration config)
        {
            if (Convert.ToBoolean(config["UserMemoryCache"]))
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(option =>
                {
                    option.Configuration = config["RedisConfiguration"];
                    option.InstanceName = "RedisInstance";
                });
            }

            services.AddHttpClient();

            // features
            services.AddSingleton<IReadableDateTimeService, ReadableDateTimeService>();

            // weather
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<IWeatherApiClient, WeatherApiClient>();
            services.AddSingleton<IForecastCache, ForecastCache>();
            services.AddSingleton<ICurrentWeatherCache, CurrentWeatherCache>();

            // greeting
            services.AddSingleton<IGreetingService, GreetingService>();

            services.AddSingleton<IAgeCalculationService, AgeCalculationService>();

            // news
            services.AddSingleton<INewsService, NewsService>();
            services.AddSingleton<IHeadlinesCache, HeadlinesCache>();

            // surf
            services.AddSingleton<ISurfService, SurfService>();
            services.AddSingleton<ISurfForecastCache, SurfForecastCache>();
            services.AddSingleton<IWaterTemperatureCache, WaterTemperatureCache>();

            return services;
        }
    }
}
