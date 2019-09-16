using System;
using Jmwoo.Core.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Assistant.Features.Surf
{
    public interface IWaterTemperatureCache : IBaseDistributedCache<WaterTemperatureResult>
    {
        
    }
  public class WaterTemperatureCache : BaseDistributedCache<WaterTemperatureResult>, IWaterTemperatureCache
  {
    public WaterTemperatureCache(IDistributedCache cache) : base(cache)
    {
    }

    public override DistributedCacheEntryOptions Options => new DistributedCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = new TimeSpan(0, 10, 0)
    };
  }
}