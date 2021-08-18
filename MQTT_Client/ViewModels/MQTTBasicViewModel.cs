using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTT_Client.Models;
using MQTT_Client.ViewModels;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MQTT_Client.ViewModels
{
    public class MQTTBasicViewModel : INotifyPropertyChanged, IMQTTViewModel
    {
        IMqttClient client;
        IMqttClientOptions options;    
        BackgroundWorker pub, sub;
        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
        private string topic = "";
        public string Topic
        {
            get => topic;
            set{
                topic = value;
                OnPropertyChanged();
            }
        }
        private string message = "";
        public string Message
        {
            get => message;
            set{
                message = value;
                OnPropertyChanged();
            }
        }
        public void ClientPublish()
        {
            //Create a BGW object and use its eventhandler to sub on mqtt client
            pub = new BackgroundWorker();
            pub.RunWorkerAsync();
            pub.DoWork += Pub;
        }
        public void ClientSubscribe()
        {
            //Create a BGW object and use its eventhandler to sub on mqtt client
            sub = new BackgroundWorker();
            sub.RunWorkerAsync();
            sub.DoWork += Sub;
        }
        public void Pub(object sender, DoWorkEventArgs e)
        {
            MQTT_Client_Connect clientConnecter = new MQTT_Client_Connect();
            client = clientConnecter.GetMyClient();
            options = clientConnecter.GetMyOptions();
            
            if (client.IsConnected == false)
            {
                client.ConnectAsync(options, CancellationToken.None);
                Console.WriteLine("Client Reconnect!");
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(Topic)
                .WithPayload(Message)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            client.PublishAsync(message, CancellationToken.None);
        }
        public void Sub(object sender, DoWorkEventArgs e)
        {
            if (client.IsConnected == false)
            {
                client.ConnectAsync(options, CancellationToken.None);
                Console.WriteLine("Client Reconnect!");
            }

            client.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(Topic)
                .Build());

            client.UseApplicationMessageReceivedHandler(e =>
            {
                try
                {
                    string topic = e.ApplicationMessage.Topic;
                    if (string.IsNullOrWhiteSpace(topic) == false)
                    {
                        string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        Console.WriteLine($"Topic: {topic} Message Received: {payload}");
                        Message = payload;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex);
                }
            });
            //CloseSubThread();
        }
            
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingFiled, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
            backingFiled = value;
            OnPropertyChanged(propertyName);
        }

        /*
                //creates 2 dimensional string Buffer
        // 1. Dimension = Topic
        // 2. Dimension = Message
        public IDictionary<string, string> buffer = new Dictionary<string, string>();
        public void InitBuffer()
        {
            buffer.Add("TopicFill", "MessageFill");
        }
        
        public void FillBuffer (string Topic, string Message)
        {
            buffer[Topic] = Message;
            /*
            try
            {
                buffer.Add(Topic, Message);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("An element with Topic = " + Topic + " already exists.");
            }
        }
        public string ReadBuffer(string Topic)
        {
            string message = "";
            if (buffer.TryGetValue(Topic, out message))
            {
                Console.WriteLine("For Topic = " + Topic + ", Message = {0}", message);
            }
            else
            {
                Console.WriteLine("Topic = " + Topic + " is not found.");
            }
            return message;

            /*
            string message = "";
            try{
                    message = buffer[Topic];
            } catch(KeyNotFoundException){
                Console.WriteLine("Key = " + Topic + " is not found.");
            }

            return message;
        }
        */
    }
}