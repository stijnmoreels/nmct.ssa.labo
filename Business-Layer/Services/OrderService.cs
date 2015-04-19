using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using nmct.ssa.labo.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmct.ssa.labo.webshop.businesslayer.Repositories;
using System.Net;
using System.Net.Mail;
using SendGrid;
using Microsoft.Azure;
using System.IO;
using System.Text;
using nmct.ssa.labo.webshop.businesslayer.Repositories.Interfaces;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;

namespace nmct.ssa.labo.webshop.businesslayer.Services
{
    public class OrderService : IOrderService
    {
        private IOrderResporitory orderRepository = null;
        private ICourierRepository courierRepository = null;

        public OrderService(IOrderResporitory orderRepository, ICourierRepository courierRepository)
        {
            this.orderRepository = orderRepository;
            this.courierRepository = courierRepository;
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

        public void SendMail(Order order)
        {
            var message = new SendGridMessage();
            message.From = new MailAddress("stijn.moreels@student.howest.be");
            message.AddTo("stijn.moreels@student.howest.be");
            message.Subject = "Order Received";
            message.Html = File.ReadAllText(@"C:\Users\stijn\Source\Workspaces\Workspace\nmct.ssa.labo.webshop\nmct.ssa.labo.webshop\nmct.ssa.labo.webshop.businesslayer\Mail\index.htm", Encoding.UTF8);
            message.Text = "Thank you";

            var credentials = new NetworkCredential("stijnmoreels", "P@ssw0rd");
            var transport = new Web(credentials);
            transport.Deliver(message);
        }

        public List<Courier> GetCouriers()
        {
            return courierRepository.All().ToList<Courier>();
        }

        public Courier GetCourier(int id)
        {
            return courierRepository.GetItemByID(id);
        }

        public List<Order> GetOrdersFromUser(string user)
        {
            return orderRepository.GetOrdersByUser(user);
        }

        public List<OrderLine> GetOrderLinesFromOrder(int id)
        {
            return orderRepository.GetOrderLinesFromOrder(id);
        }

        public List<OrderChart> GetAllJoinedOrders(int mode)
        {
            return orderRepository.GetAllJoinedOrders(mode);
        }
    }
}