namespace GoGlobal.Test.Backend.Services.Interfaces;

public interface IAuthMemoryCacheService : IMemoryCacheService<string>
{
    
}

public interface IMemoryCacheService<T>
{
    Task<T> Get(string key);
    void Set(string key, T entry);
    void Remove(string key);
}
