using System;
using System.Threading.Tasks;

namespace SimpleFeedReader
{
    public interface ICache
    {
        Task Insert<TObj>(string key, TObj value, DateTimeOffset? expiration = default(DateTimeOffset?));

        Task<TObj> Get<TObj>( string key );
    }
}
