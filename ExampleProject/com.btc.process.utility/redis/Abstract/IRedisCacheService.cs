using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.btc.process.utility.redis.Abstract
{
    public interface IRedisCacheService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        Task Clear(string key);
        void ClearAll();
        Task<bool> SetJsonValueAsync(string key, Object value);
        Task<T> GetData<T>(string key);


}
}
