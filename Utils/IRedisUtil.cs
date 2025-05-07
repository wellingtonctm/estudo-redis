using redis_teste.Models;

namespace redis_teste.Utils;

public interface IRedisUtil
{
    Task<RedisGetObject<T>?> GetAsync<T>(string key) where T : class;
    Task SetAsync(string key, object value, TimeSpan? tempoDuracao = null);
    Task<RedisGetObject<T>?> GetAsync<T>(ERedis key) where T : class;
    Task SetAsync(ERedis key, object value, TimeSpan? tempoDuracao = null);
    ValueTask DisposeAsync();
}