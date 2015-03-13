using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public interface IBasketService
    {
        List<BasketItem> GetAllBasketItems(string user);
        void AddToBasket(Device device, int amount, string user);
        void UpdateBasket(List<BasketItem> baskets, string user);
        void UnavailableBasket(List<BasketItem> baskets);
    }
}
