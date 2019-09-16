using System;
using Jmwoo.Core.Caching;
using Assistant.Features.Weather.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Assistant.Features.Weather.Services
{
    public interface IForecastCache : IBaseDistributedCache<ForecastResult>
    {
    }

    public class ForecastCache : BaseDistributedCache<ForecastResult>, IForecastCache
    {
        public ForecastCache(IDistributedCache cache) : base(cache)
        {
        }

        public override DistributedCacheEntryOptions Options => new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = new TimeSpan(0, 10, 0)
        };
    }
}
