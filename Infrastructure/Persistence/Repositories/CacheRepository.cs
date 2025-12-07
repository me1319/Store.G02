using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase database; 
        public CacheRepository(IConnectionMultiplexer connection)
        {
            database = connection.GetDatabase();
        }
      
        public async Task<string> GetAsync(string id)
        {
           var redisValue = await  database.StringGetAsync(id);
            return !redisValue.IsNullOrEmpty ?redisValue  : default;
        }

        public async Task SetAsync(string key, object value, TimeSpan timeSpan)
        {
            var redisValue = JsonSerializer.Serialize(value);
           var cache =await database.StringSetAsync(key, redisValue, timeSpan);                   
        }
    }
}
