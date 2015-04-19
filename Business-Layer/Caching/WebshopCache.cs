using nmct.ssa.labo.webshop.businesslayer.Caching.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.Repositories;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace nmct.ssa.labo.webshop.businesslayer.Caching
{
    public class WebshopCache : IWebshopCache
    {
        private static IDatabase cache = null;
        private static string appsetting = WebConfigurationManager.AppSettings["RedisCache"];

        public static void Setup()
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(appsetting);
            cache = connection.GetDatabase();
        }

        //TO-DO => Search "cache-aside"
        public bool CacheCheck(string name)
        {
            return cache.Get(name) == null;
        }

        public IEnumerable<T> GetItemsFromCache<T>(string name)
        {
            return (IEnumerable<T>)cache.Get(name);
        }

        public T GetItemFromCache<T>(string name)
        {
            return (T)cache.Get(name);
        }

        public void RefreshCache<T>(T value, string name)
        {
            cache.Set(name, value);
        }

        public void RefreshCache<T>(IEnumerable<T> values, string name)
        {
            cache.Set(name, values);
        }

        public void IncrementCache<T>(string name, int number)
        {
            cache.StringIncrement(name, number, CommandFlags.None);
        }
    }
}
