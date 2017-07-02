using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;

namespace SimpleFeedReader.Cache
{
    public class FeedCache : ICache
    {
        private const string applicationName = "SimpleFeedReader";

        public FeedCache()
        {
            Initialise();
        }

        private void Initialise()
        {
            BlobCache.ApplicationName = applicationName;
            BlobCache.EnsureInitialized();
        }

        public async Task Insert<TObj>(string key, TObj value, DateTimeOffset? expiration = default(DateTimeOffset?))
        {
            await BlobCache.LocalMachine.InsertObject(key, value, expiration);
        }

        public async Task<TObj> Get<TObj>(string key)
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<TObj>(key);
            }
            catch (KeyNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return default(TObj);
            }
        }
    }
}
