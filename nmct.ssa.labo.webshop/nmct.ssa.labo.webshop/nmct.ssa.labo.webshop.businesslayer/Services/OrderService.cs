using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmct.ssa.labo.webshop.businesslayer.Repositories;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class OrderService : IOrderService
    {
        private IOrderResporitory orderRepository = null;

        public OrderService(IOrderResporitory orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void AddToQueue(Order order)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient client = account.CreateCloudQueueClient();
            CloudQueue queue = client.GetQueueReference("orders");
            queue.CreateIfNotExists();

            order.Timestamp = DateTime.Now;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            CloudQueueMessage message = new CloudQueueMessage(json);
            queue.AddMessage(message);
        }

        public void AddOrderToDatabase(Order order)
        {
            orderRepository.Insert(order);
            orderRepository.SaveChanges();
        }
    }
}