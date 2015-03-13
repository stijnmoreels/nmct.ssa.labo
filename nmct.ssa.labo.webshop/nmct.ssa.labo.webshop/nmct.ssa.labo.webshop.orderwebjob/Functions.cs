using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.businesslayer.Repositories;

namespace nmct.ssa.labo.webshop.orderwebjob
{
    public class Functions
    {

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("orders")] string message, TextWriter log)
        {
            Order order = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(message);
            if(order != null)
            {
                IOrderService service = new OrderService(new OrderRepository());
                service.AddOrderToDatabase(order);
            }
            log.WriteLine(message);
        }
    }
}
