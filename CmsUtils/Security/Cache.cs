﻿using System;
using System.Web;

namespace CmsUtils
{
    public class Cache
    {
        private static readonly System.Web.Caching.Cache cache = HttpRuntime.Cache;

        public T GetCache<T>(string cacheKey) where T : class
        {
            if (cache[cacheKey] != null)
                return (T) cache[cacheKey];
            return default(T);
        }

        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            cache.Insert(cacheKey, value, null, DateTime.Now.AddMinutes(10),
                System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            cache.Insert(cacheKey, value, null, expireTime, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public void RemoveCache(string cacheKey)
        {
            cache.Remove(cacheKey);
        }

        public void RemoveCache()
        {
            var CacheEnum = cache.GetEnumerator();
            while (CacheEnum.MoveNext())
                cache.Remove(CacheEnum.Key.ToString());
        }
    }
}