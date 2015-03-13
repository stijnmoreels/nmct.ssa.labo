using nmct.ssa.labo.webshop.businesslayer.Calculators;
using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class BasketRepository : GenericRepository<BasketItem, ApplicationDbContext>, IBasketRepository
    {
        public BasketRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void AddToBasket(Device device, int amount, string user)
        {
            BasketItem item = new BasketItem();
                item.Device = device;
                item.Amount = amount;
                item.Timestamp = DateTime.Now;
                item.UserId = user;
                item.TotalPrice = TotalPriceCalculator.CalculateTotalPrice(device, amount);
                item.Available = true;
           
            context.Entry(device).State = System.Data.Entity.EntityState.Unchanged;
            context.BasketItem.Add(item);
            context.SaveChanges();
        }

        public void UpdateBasket(List<BasketItem> baskets, string user)
        {
            //List<BasketItem> items = GetAllBasketItems(user);
            for(int i = 0; i < baskets.Count; i++)
            {
                int deviceId = baskets[i].DeviceId;
                BasketItem item = context.BasketItem
                    .Where(b => b.DeviceId == deviceId && b.UserId.Equals(user))
                    .SingleOrDefault<BasketItem>();
                
                    item.Amount = baskets[i].Amount;
                    item.TotalPrice = TotalPriceCalculator.CalculateTotalPrice(item.Device, item.Amount);
                context.Entry(item).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public List<BasketItem> GetAllBasketItems(string user)
        {
            return context.BasketItem
                .Where(b => b.UserId.Equals(user) && b.Available)
                .ToList<BasketItem>();
        }

        public void UnavailableBaskets(List<BasketItem> baskets)
        {
            foreach(BasketItem basket in baskets)
            {
                context.BasketItem
                    .Where(b => b.Id == basket.Id)
                    .SingleOrDefault<BasketItem>()
                    .Available = false;
            }
            context.SaveChanges();
        }
    }
}