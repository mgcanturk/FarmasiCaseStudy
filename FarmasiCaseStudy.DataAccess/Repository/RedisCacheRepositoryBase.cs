using FarmasiCaseStudy.Core.Repository.Abstract;
using FarmasiCaseStudy.Core.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.DataAccess.Repository
{
    public class RedisCacheRepositoryBase : ICacheGenericRepository
    {
        private readonly IDistributedCache _cache;
        public RedisCacheRepositoryBase(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _cache.GetStringAsync(key);
            if (value != null)
            { 
                return JsonSerializer.Deserialize<T>(value); 
            }
            return default;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            var timeOut = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12),
                SlidingExpiration = TimeSpan.FromMinutes(60)
            };
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), timeOut);
            return value;
        }
    }
}
