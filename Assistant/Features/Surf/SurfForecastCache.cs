using System;
using System.Collections.Generic;
using Jmwoo.Common.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Assistant.Features.Surf
{
    public interface ISurfForecastCache : IBaseDistributedCache<List<SurfMoment>>
    {
    }
    public class SurfForecastCache : BaseDistributedCache<List<SurfMoment>>, ISurfForecastCache
    {
      public SurfForecastCache(IDistributedCache cache) : base(cache)
      {
      }

      public override DistributedCacheEntryOptions Options => new DistributedCacheEntryOptions
      {
          AbsoluteExpirationRelativeToNow = new TimeSpan(0, 10, 0)
      };
    }
}