using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Core.Objects.DataClasses;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories
{
    public class OrderRepository : GenericRepository<Order, ApplicationDbContext>, IOrderResporitory
    {
        public List<Order> GetOrdersByUser(string user)
        {
            return context.Order
                .Include(lines => lines.Orders)
                .Where(o => o.UserId.Equals(user) && o.Deleted == false)
                .OrderByDescending(o => o.Timestamp).ToList<Order>();
        }

        public List<OrderLine> GetOrderLinesFromOrder(int id)
        {
            return context.Orderline.Where(o => o.OrderId.Equals(id))
                .ToList<OrderLine>();
        }

        public override Order Insert(Order entity)
        {
            entity.Orders.ForEach(o => context.Entry<Device>(o.Device).State 
                = System.Data.Entity.EntityState.Unchanged);
            return context.Order.Add(entity);
        }

        public List<OrderChart> GetAllJoinedOrders(int mode)
        {
            var query = new List<OrderChart>();
            switch (mode)
            {
                case 0:
                    query = context.Order.Include("OrderLines")
                    .GroupBy(o => new { Date = o.Timestamp.Day })
                    .Select(o => new OrderChart { Label = o.Key.Date.ToString(), Count = o.ToList().Count })
                    .ToList<OrderChart>();
                    break;
                case 1:
                     query = context.Order.Include("OrderLines")
                    .GroupBy(o => new { Date = o.Timestamp.Month })
                    .Select(o => new OrderChart { Label = o.Key.Date.ToString(), Count = o.ToList().Count })
                    .ToList<OrderChart>();
                    break;
                case 2:
                     query = context.Order.Include("OrderLines")
                    .GroupBy(o => new { Date = o.Timestamp.Year })
                    .Select(o => new OrderChart { Label = o.Key.Date.ToString(), Count = o.ToList().Count })
                    .ToList<OrderChart>();
                    break;
            }               
            return query;
        }

        private int SetMode(DateTime time, int mode)
        {
            return new List<int>() { time.Day, time.Month, time.Year }[mode];
        }
    }
}
