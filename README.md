[![Build (main)](https://github.com/mroberts91/umbraco-cache-refresher/actions/workflows/ci.yml/badge.svg)](https://github.com/mroberts91/umbraco-cache-refresher/actions/workflows/ci.yml)
![GitHub tag (latest SemVer pre-release)](https://img.shields.io/github/v/tag/mroberts91/umbraco-cache-refresher?include_prereleases)
![Umbraco Version Support](https://img.shields.io/badge/Umbraco-9%2B-blue)
![GitHub last commit](https://img.shields.io/github/last-commit/mroberts91/umbraco-cache-refresher)
![GitHub](https://img.shields.io/github/license/mroberts91/umbraco-cache-refresher)  

# Our.Umbraco.CacheRefresher

This package is designed to add types that extend the new Umbraco v9+ event aggregator
pattern, allowing application code that manages it's own caches to hook into
core Umbraco events and invalidate or updated those caches triggered via
a notification event.

## Adding to `IUmbracoBuilder`
``` csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddUmbraco(_env, _config)
        // Add the INotification and INotifiableCacheRefresher pair using the Umbraco builder
        .AddCacheRefresher<ContentTreeChangeNotification, TestNotifiableCacheRefresher>()
        // Add the INotification and INotifiableAsyncCacheRefresher pair using the Umbraco builder
        .AddAsyncCacheRefresher<ContentTreeChangeNotification, TestNotifiableAsyncCacheRefresher>();
}
```
## Types Added
``` csharp
public interface INotifiableCacheRefresher<in TNotification> : INotificationHandler<TNotification> { }
public abstract class NotifiableCacheRefresher<TNotification> : INotifiableCacheRefresher<TNotification> { }
public abstract class ContentTreeChangedCacheRefresher : NotifiableCacheRefresher<ContentTreeChangeNotification> { }

public interface INotifiableAsyncCacheRefresher<in TNotification> : INotificationAsyncHandler<TNotification> { }
public abstract class NotifiableAsyncCacheRefresher<TNotification> : INotifiableAsyncCacheRefresher<TNotification> { }
public abstract class ContentTreeChangedAsyncCacheRefresher : NotifiableAsyncCacheRefresher<ContentTreeChangeNotification> { }
```

## Usage
``` csharp
// Async, inheriting from the the base abstract refresher
internal sealed class TestAsyncCacheRefresher : NotifiableAsyncCacheRefresher<ContentTreeChangeNotification>
{
    private readonly ITestCache<CacheValue> _cache;

    public TestNotifiableCacheRefresher(ITestCache<CacheValue> cache)
    {
        _cache = cache;
    }

    protected override async Task OnNotificationReceivedAsync(ContentTreeChangeNotification notification, CancellationToken cancellationToken)
    {
        if (!(notification?.Changes is IEnumerable<TreeChange<IContent>> changes))
            return;

        IEnumerable<Task> tasks = changes.Select(tc => _cache.RemoveAsync(tc.Item.Id));
        await Task.WhenAll(tasks);
    }
}

// Sync, inheriting from the Contnet Changed abstract refresher
internal sealed class TestCacheRefresher : ContentTreeChangedCacheRefresher
{
    private readonly ITestCache<CacheValue> _cache;

    public TestCacheRefresher(ITestCache<CacheValue> cache)
    {
        _cache = cache;
    }

    protected override void ContentChanged(IContent content, TreeChangeTypes treeChangeType)
    {
        _cache.Remove(content.Id);
    }
}
```
