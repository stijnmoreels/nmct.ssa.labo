using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public interface IOrderService
    {
        void AddToQueue(Order order);
        void AddOrderToDatabase(Order order);
    }
}
