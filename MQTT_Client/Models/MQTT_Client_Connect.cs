using System.ComponentModel;
using System.Threading;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MQTT_Client.Models
{
    public class MQTT_Client_Connect
    {
        IMqttClient client;
        IMqttFactory mqttFactory;
        IMqttClientOptions options;
        BackgroundWorker thread;
        public MQTT_Client_Connect()
        {
            //create client
            mqttFactory = new MqttFactory();
            client = mqttFactory.CreateMqttClient();

            //define options
            options = new MqttClientOptionsBuilder()
            //.WithTcpServer("10.10.21.116", 1883) // Port is optional
            //.WithTcpServer("localhost", 1883) // LOCALHOST
            .WithTcpServer("192.168.178.117", 1883) // HO
            .Build();

            //Create a BGW object and use its eventhandler to connect mqtt client
            thread = new BackgroundWorker();
            thread.RunWorkerAsync();
            thread.DoWork += Connect;

        }
        public void Connect(object sender, DoWorkEventArgs e)
        {
            client.ConnectAsync(options, CancellationToken.None);
        }

        public void GetConnection()
        {
            thread.DoWork += Connect;
        }

        public IMqttClient GetMyClient()
        {
            return client;
        }
        public IMqttClientOptions GetMyOptions()
        {
            return options;
        }
    }
}
