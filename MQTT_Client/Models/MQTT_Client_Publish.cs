using System;
using System.ComponentModel;
using System.Threading;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MQTT_Client.Models
{
    public class PublishMessage
    {
        public PublishMessage(IMqttClient client, IMqttClientOptions options, object sender, DoWorkEventArgs e)
        {    
            if (client.IsConnected == false)
            {
                client.ConnectAsync(options, CancellationToken.None);
                Console.WriteLine("Client Reconnect!");
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic("test")
                .WithPayload("test")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            client.PublishAsync(message, CancellationToken.None);
        }
    }
}