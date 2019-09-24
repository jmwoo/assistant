using System;
using Jmwoo.Common.Caching;
using Assistant.Features.Weather.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Assistant.Features.Weather.Services
{
    public interface ICurrentWeatherCache : IBaseDistributedCache<WeatherResult>
    {
    }

    public class CurrentWeatherCache : BaseDistributedCache<WeatherResult>, ICurrentWeatherCache
    {
        public CurrentWeatherCache(IDistributedCache cache) : base(cache)
        {
        }

        public override DistributedCacheEntryOptions Options => new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = new TimeSpan(0, 10, 0)
        };
    }
}
