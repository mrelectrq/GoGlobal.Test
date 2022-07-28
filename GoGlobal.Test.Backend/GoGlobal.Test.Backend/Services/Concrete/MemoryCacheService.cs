using GoGlobal.Test.Backend.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GoGlobal.Test.Backend.Services.Concrete;

public class MemoryCacheService<T> : IMemoryCacheService<T>
{
    protected readonly IMemoryCache _memoryCache;

    private readonly MemoryCacheEntryOptions _options;
    
    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(2));
    }
    
    public virtual Task<T> Get(string key)
    {
        T entry = default;

        _memoryCache.TryGetValue(key, out entry);
        return Task.FromResult(entry);
    }

    public void Set(string key, T entry)
    {
        _memoryCache.Set(key, entry, _options);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }
}