using nmct.ssa.labo.webshop.businesslayer.Calculators;
using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class BasketRepository : GenericRepository<BasketItem, ApplicationDbContext>, IBasketRepository
    {
        public void AddToBasket(Device device, int amount, string user)
        {
            BasketItem check = context.BasketItem
                .Where(b => b.Device.Id == device.Id && b.UserId.Equals(user) && b.Available == true)
                .SingleOrDefault<BasketItem>();
            
            if (check != null)
                UpdateExistingBasketItem(amount, check);
            else
                AddNewBasketItem(device, amount, user);
        }

        private void UpdateExistingBasketItem(int amount, BasketItem check)
        {
            check.Amount += amount;
            context.Entry<BasketItem>(check).State = EntityState.Modified;
            context.SaveChanges();
        }

        private void AddNewBasketItem(Device device, int amount, string user)
        {
            BasketItem item = new BasketItem()
            {
                Device = device,
                Amount = amount,
                Timestamp = DateTime.Now,
                UserId = user,
                TotalPrice = TotalPriceCalculator.CalculateTotalPrice(device, amount),
                Available = true
            };

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

        public void AvailableUnavailableBasket(int id, bool mode)
        {
            context.BasketItem
                .Where(b => b.Id == id)
                .SingleOrDefault<BasketItem>()
                .Available = mode;
            context.SaveChanges();
        }

        public void UpdateBasketId(string user, string cookie)
        {
            BasketItem item = context.BasketItem
                .Where(b => b.UserId.Equals(cookie))
                .SingleOrDefault<BasketItem>();
            if(item != null)
            {
                item.UserId = user;
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}