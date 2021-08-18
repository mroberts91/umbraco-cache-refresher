
namespace Our.Umbraco.CacheRefresher.Tests;

public class ContentTreeChangedCacheRefresherTests : UmbracoBuilderFixture
{
    private readonly Bogus.Faker _faker = new();

    protected override void ConfigureServices()
    {
        _services.AddMemoryCache();
        _services.AddTransient(typeof(ITestCache<>), typeof(TestCache<>));
        _services.AddSingleton<IEventAggregator, EventAggregator>();
        _umbracoBuilder.AddCacheRefresher<ContentTreeChangeNotification, TestNotifiableCacheRefresher>();
        _umbracoBuilder.AddAsyncCacheRefresher<ContentTreeChangeNotification, TestNotifiableAsyncCacheRefresher>();
    }

    [Fact]
    public void ClearedFromCache()
    {
        var cache = _serviceProvider.GetRequiredService<ITestCache<CacheValue>>();
        var treeChanges = GetTreeChanges();
        foreach (var change in treeChanges)
        {
            CacheValue value = new()
            {
                Id = change.Item.Id,
                Name = change.Item.Name,
            };

            cache.SetValue(change.Item.Id.ToString(), value);
            
            var cachedValue = cache.GetValue(change.Item.Id.ToString());
            cachedValue.ShouldNotBeNull();
            cachedValue.Equals(value).ShouldBeTrue();

        }

        SendContentTreeChangeNotification(treeChanges);

        foreach (var change in treeChanges)
            cache.GetValue(change.Item.Id.ToString()).ShouldBeNull();
    }

    [Fact]
    public async Task ClearedFromCacheAsync()
    {
        var cache = _serviceProvider.GetRequiredService<ITestCache<CacheValue>>();
        var treeChanges = GetTreeChanges();
        foreach (var change in treeChanges)
        {
            CacheValue value = new()
            {
                Id = change.Item.Id,
                Name = change.Item.Name,
            };

            await cache.SetValueAsync(change.Item.Id.ToString(), value);

            var cachedValue = await cache.GetValueAsync(change.Item.Id.ToString());
            cachedValue.ShouldNotBeNull();
            cachedValue.Equals(value).ShouldBeTrue();

        }

        SendContentTreeChangeNotification(treeChanges);

        foreach (var change in treeChanges)
            (await cache.GetValueAsync(change.Item.Id.ToString())).ShouldBeNull();
    }

    private IEnumerable<TreeChange<IContent>> GetTreeChanges()
    {
        return Enumerable.Range(0, 2).Select(_ =>
        {
            Content content = new(_faker.Random.String2(10), -1, new ContentType(null, -1))
            {
                Id = _faker.Random.Int(min: 0, max: 10000)
            };
            return new TreeChange<IContent>(content, TreeChangeTypes.RefreshNode);
        });
    }

    private void SendContentTreeChangeNotification(IEnumerable<TreeChange<IContent>> treeChanges)
    {
        var ea = _serviceProvider.GetRequiredService<IEventAggregator>();
        ea.Publish(new ContentTreeChangeNotification(treeChanges, new()));
    }

}