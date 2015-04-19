using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces
{
    public interface IBasketRepository : IGenericRepository<BasketItem>
    {
        void AddToBasket(Device device, int amount, string user);
        void UpdateBasket(List<BasketItem> baskets, string user);
        List<BasketItem> GetAllBasketItems(string user);
        void UnavailableBaskets(List<BasketItem> baskets);
        void AvailableUnavailableBasket(int id, bool mode);
        void UpdateBasketId(string user, string cookie);
    }
}
