using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string Cachekey)
        {
        return  await _cacheRepository.GetAsync(Cachekey);
        }

        public async Task SetAsync(string Cachekey, string value, TimeSpan TimeToLive)
        {
           var Value = JsonSerializer.Serialize(value);
            await _cacheRepository.SetAsync(Cachekey, Value, TimeToLive);
        }
    }
}
