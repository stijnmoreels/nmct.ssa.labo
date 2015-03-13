using nmct.ssa.labo.webshop.businesslayer.Calculators;
using nmct.ssa.labo.webshop.businesslayer.Repositories;
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

        public BasketService(IBasketRepository repBasket)
	    {
            this.basketRepository = repBasket;
	    }

        public List<BasketItem> GetAllBasketItems(string user)
        {
            return basketRepository.GetAllBasketItems(user);
        }

        public void AddToBasket(Device device, int amount, string user)
        {
            basketRepository.AddToBasket(device, amount, user);
        }

        public void UpdateBasket(List<BasketItem> baskets, string user)
        {
            basketRepository.UpdateBasket(baskets, user);
        }

        public void UnavailableBasket(List<BasketItem> baskets)
        {
            this.basketRepository.UnavailableBaskets(baskets);
        }
    }
}