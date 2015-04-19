using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces
{
    public interface IOrderResporitory : IGenericRepository<Order>
    {
        List<Order> GetOrdersByUser(string user);
        List<OrderLine> GetOrderLinesFromOrder(int id);
        List<OrderChart> GetAllJoinedOrders(int mode);
    }
}
