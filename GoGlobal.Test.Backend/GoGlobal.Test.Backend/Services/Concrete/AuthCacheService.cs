using GoGlobal.Test.Backend.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace GoGlobal.Test.Backend.Services.Concrete;

public class AuthCacheService : MemoryCacheService<string>, IAuthMemoryCacheService
{
    public AuthCacheService(IMemoryCache memoryCache) : base(memoryCache)
    {
    }
}