using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.CacheRefresher.Tests;

internal sealed class TestNotifiableCacheRefresher : ContentTreeChangedCacheRefresher
{
    private readonly ITestCache<CacheValue> _cache;

    public TestNotifiableCacheRefresher(ITestCache<CacheValue> cache)
    {
        _cache = cache;
    }

    protected override void ContentChanged(IContent content, TreeChangeTypes treeChangeType)
    {
        _cache.Remove(content.Id.ToString());
    }
}

internal sealed class TestNotifiableAsyncCacheRefresher : ContentTreeChangedAsyncCacheRefresher
{
    private readonly ITestCache<CacheValue> _cache;

    public TestNotifiableAsyncCacheRefresher(ITestCache<CacheValue> cache)
    {
        _cache = cache;
    }

    protected override async Task ContentChangedAsync(IContent content, TreeChangeTypes treeChangeType, CancellationToken cancellation)
    {
        await _cache.RemoveAsync(content.Id.ToString());
    }
}
