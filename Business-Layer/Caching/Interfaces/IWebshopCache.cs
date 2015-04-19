using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nmct.ssa.labo.webshop.businesslayer.Caching.Interfaces
{
    public interface IWebshopCache
    {
        bool CacheCheck(string name);
        T GetItemFromCache<T>(string name);
        IEnumerable<T> GetItemsFromCache<T>(string name);
        void RefreshCache<T>(T value, string name);
        void RefreshCache<T>(IEnumerable<T> values, string name);
        void IncrementCache<T>(string name, int number);
    }
}
