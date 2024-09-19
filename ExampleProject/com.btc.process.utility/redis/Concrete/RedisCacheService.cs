using com.btc.process.utility.redis.Abstract;
using Nancy.Json;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.process.utility.redis.Concrete
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _cache;

        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _cache = redisConnection.GetDatabase();
        }

        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var redisEndpoints = _redisConnection.GetEndPoints(true);
            foreach (var redisEndpoint in redisEndpoints)
            {
                var redisServer = _redisConnection.GetServer(redisEndpoint);
                redisServer.FlushAllDatabases();
            }
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await _cache.StringSetAsync(key, value, TimeSpan.FromHours(1));
        }

        public async Task<bool> SetJsonValueAsync(string key, Object value)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            var serialize = json_serializer.Serialize(value);
            return await _cache.StringSetAsync(key, serialize, TimeSpan.FromHours(1));
        }

        public async Task<T> GetData<T>(string key)
        {
            var value = await _cache.StringGetAsync(key);
            return await JsonConvert.DeserializeObjectAsync<T>(value);
        }
    }
}
