using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.ServiceBusHelper
{
    public class ServiceBus : IServiceBus
    {
        private NamespaceManager namespaceManager;
        private string _connectionstring;
        public string Connectionstring
        {
            get { return _connectionstring; }
            set { _connectionstring = value; GetNamespaceFromConnectionstring(); }
        }

        private void GetNamespaceFromConnectionstring()
        {
            Connectionstring = CloudConfigurationManager.GetSetting(_connectionstring);
            namespaceManager = NamespaceManager.CreateFromConnectionString(_connectionstring);
        }

        public ServiceBus(string connectionstring = "Microsoft.ServiceBus.ConnectionString")
        {
            this.Connectionstring = connectionstring;
        }

        public void AddToServiceBus<T>(T item, string topic)
        {
            if (!namespaceManager.TopicExists(topic))
                namespaceManager.CreateTopic(topic);

            TopicClient client = TopicClient.CreateFromConnectionString(_connectionstring, topic);
            BrokeredMessage message = new BrokeredMessage(item);
            client.Send(message);
        }

        public void AddNewSubscription(string name, string topic)
        {
            if (!namespaceManager.SubscriptionExists(topic, name))
                namespaceManager.CreateSubscription(topic, name);
        }

        public void AddNewSubscription(string name, string topic, string filter)
        {
            //SqlFilter filter = new SqlFilter("vb. MessageNumber > 3");
            SqlFilter sqlFilter = new SqlFilter(filter);
            if (!namespaceManager.SubscriptionExists(topic, name))
                namespaceManager.CreateSubscription(topic, name, sqlFilter);
        }

        public void handleIncommingMessage(string topic, string subscription, Action<BrokeredMessage> action)
        {
            SubscriptionClient client = SubscriptionClient.CreateFromConnectionString(Connectionstring, topic, subscription);
            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);
            client.OnMessage(message => action(message), options);
        }
    }
}
