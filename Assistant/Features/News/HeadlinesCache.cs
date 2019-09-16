using System;
using Jmwoo.Core.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Assistant.Features.News
{
    public interface IHeadlinesCache : IBaseDistributedCacheSingle<HeadlinesResult>
    {
    }

    public class HeadlinesCache : BaseDistributedCache<HeadlinesResult>, IHeadlinesCache
    {
        public HeadlinesCache(IDistributedCache cache) : base(cache)
        {
        }

        public override DistributedCacheEntryOptions Options => new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = new TimeSpan(0, 5, 0)
        };
    }
}
