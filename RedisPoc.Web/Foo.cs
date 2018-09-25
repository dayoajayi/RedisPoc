using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisPoc.Web
{
    public class Foo : IFoo
    {
        private readonly IDistributedCache _distributedCache;

        public Foo(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task Fah(string fahVal)
        {
            try
            {
                var key = "GarysMessage";

                var data = Encoding.UTF8.GetBytes(fahVal);
                await _distributedCache.SetAsync(key, data);

                var cachedData = await _distributedCache.GetAsync(key);
                var cachedMessage = Encoding.UTF8.GetString(cachedData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}