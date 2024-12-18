using System;
using System.Configuration;
using System.Web;


namespace ICMS.Common
{
    public static class MCache
    {
        static System.Web.Caching.Cache _memCache = HttpRuntime.Cache;
        static double _defaultExpiration = ConfigurationManager.AppSettings["CacheExpiry"] != null ? 
                                           (Convert.ToInt32(ConfigurationManager.AppSettings["CacheExpiry"]) * 60) : (20 * 60); // in seconds
        
        public static void Set(string key, object item)
        {
            _memCache.Insert(key, item, null, DateTime.UtcNow.AddSeconds(_defaultExpiration), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static object Get(string key)
        {
            return _memCache.Get(key);
        }

    }
}