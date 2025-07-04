using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer _Connection) : ICacheRepository
    {
        private readonly IDatabase _database = _Connection.GetDatabase();

        public async Task<string?> GetAsync(string Cachekey)
        {
            var CacheValue = await _database.StringGetAsync(Cachekey);
            return CacheValue.HasValue ? CacheValue.ToString() : null;
        }

        public async Task SetAsync(string Cachekey, string value, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(Cachekey, value, TimeToLive);
        }
    }
}
