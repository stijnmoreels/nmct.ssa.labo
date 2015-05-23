using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Services.Interfaces
{
    public interface IOrderService
    {
        void AddToQueue(Order order);
        void AddOrderToDatabase(Order order);
        void SendMail(Order order);
        List<Courier> GetCouriers();
        List<Order> GetOrdersFromUser(string user);
        List<OrderLine> GetOrderLinesFromOrder(int id);
        Courier GetCourier(int id);
        List<OrderChart> GetAllJoinedOrders(int mode);
        Order GetOrderById(int id);
    }
}
