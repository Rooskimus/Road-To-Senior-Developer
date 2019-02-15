using System;

namespace WebApp
{
    public interface ITCache<T>
    {
        T Get(string cacheKeyName, int cacheTimeOut, Func<T> func);
    }
}