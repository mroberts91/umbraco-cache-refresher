
namespace Our.Umbraco.CacheRefresher.Tests.Common;
public interface ITestCache<T>
    where T : class
{
    T? GetValue(string key);
    Task<T?> GetValueAsync(string key);
    void SetValue(string key, T value);
    Task SetValueAsync(string key, T value);
    void Remove(string key);
    Task RemoveAsync(string key);
}

internal sealed class TestCache<T> : ITestCache<T>
    where T : class
{
    private readonly IMemoryCache _memoryCache;
    private readonly Lazy<string> CACHE_PREFIX = new(() => typeof(T).GUID.ToString("N"));
    private string CacheKey(string key) => $"{CACHE_PREFIX.Value}_{key}";

    public TestCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T? GetValue(string key) => _memoryCache.Get<T>(CacheKey(key));
    public Task<T?> GetValueAsync(string key) => Task.FromResult<T?>(_memoryCache.Get<T>(CacheKey(key)));
    public void SetValue(string key, T value) => _memoryCache.Set(CacheKey(key), value);
    public Task SetValueAsync(string key, T value)
    {
        _memoryCache.Set(CacheKey(key), value);
        return Task.CompletedTask;
    }

    public void Remove(string key) => _memoryCache.Remove(CacheKey(key));

    public Task RemoveAsync(string key)
    {
        _memoryCache.Remove(CacheKey(key));
        return Task.CompletedTask;
    }
}
