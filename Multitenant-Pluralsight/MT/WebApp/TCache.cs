using System;

namespace WebApp
{
    public class TCache<T>
    {
        // This class exists to make the caching Redis ready, i.e. we will later add
        // a switch that determines which cache to use and accesses the external cache
        // from the cloud.
        public T Get(string cacheKeyName, int cacheTimeOutSeconds, Func<T> func)
        {
            return new TCacheInternal<T>().Get(cacheKeyName, cacheTimeOutSeconds, func);
        }
    }
}