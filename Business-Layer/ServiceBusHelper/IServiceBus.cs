using Microsoft.ServiceBus.Messaging;
using System;
namespace nmct.ssa.labo.webshop.businesslayer.ServiceBusHelper
{
    public interface IServiceBus
    {
        void AddToServiceBus<T>(T item, string topic);
        void AddNewSubscription(string name, string topic);
        void AddNewSubscription(string name, string topic, string filter);
        string Connectionstring { get; set; }
        void handleIncommingMessage(string topic, string subscription, Action<BrokeredMessage> action);
    }
}
