using nmct.ssa.labo.webshop.businesslayer.Caching;
using nmct.ssa.labo.webshop.businesslayer.Caching.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.Calculators;
using nmct.ssa.labo.webshop.businesslayer.Repositories;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class BasketService : IBasketService
    {
        private IBasketRepository basketRepository = null;
        private IWebshopCache cache = null;

        public BasketService(IBasketRepository repBasket, IWebshopCache cache)
	    {
            this.basketRepository = repBasket;
            this.cache = cache;
	    

        public List<BasketItem> GetAllBasketItems(string user)
        {
            return basketRepository.GetAllBasketItems(user);
        }

        public int CountAllBasketItems(string user)
        {
            return cache.GetItemFromCache<int>(CacheNames.CACHE_BASKET);
        }

        public void AddToBasket(Device device, int amount, string user)
        {
            basketRepository.AddToBasket(device, amount, user);
            //cache.IncrementCache<BasketItem>(CacheNames.CACHE_BASKET, basket.Count);
            RefreshItemsInBasket(user);
        }

        public void UpdateBasket(List<BasketItem> basket, string user)
        {
            this.basketRepository.UpdateBasket(basket, user);
            //cache.IncrementCache<BasketItem>(CacheNames.CACHE_BASKET, basket.Count);
            RefreshItemsInBasket(user);
        }

        public void UnavailableBasket(List<BasketItem> baskets)
        {
            this.basketRepository.UnavailableBaskets(baskets);
        }

        public void AvailableUnavailableBasket(int id, bool mode)
        {
            this.basketRepository.AvailableUnavailableBasket(id, mode);
        }

        public void UpdateBasketID(string user, string cookie)
        {
            this.basketRepository.UpdateBasketId(user, cookie);
        }

        public int GetItemsInBasket(string user)
        {
            if (cache.CacheCheck(CacheNames.CACHE_BASKET))
                RefreshItemsInBasket(user);
            return cache.GetItemFromCache<int>(CacheNames.CACHE_BASKET);
        }

        public void RefreshItemsInBasket(string user)
        {
            List<BasketItem> basket = basketRepository.GetAllBasketItems(user);
            cache.RefreshCache<int>(basket.Count, CacheNames.CACHE_BASKET);
        }
    }
}