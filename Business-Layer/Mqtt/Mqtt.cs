using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace nmct.ssa.examen.businesslayer
{
    public class Mqtt
    {
        private Action<MqttMsgPublishEventArgs> _action = null;

        public void ReceiveMessage(string broker, string topic, Action<MqttMsgPublishEventArgs> action)
        {
            MqttClient client = new MqttClient(broker);
            client.Connect(Guid.NewGuid().ToString());
            _action = action;
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            _action(e);
        }

        public void SendMessage(string broker, string topic, byte[] message)
        {
            MqttClient client = new MqttClient(broker);
            client.Connect(Guid.NewGuid().ToString());
            client.Publish(topic, message, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
    }
}
