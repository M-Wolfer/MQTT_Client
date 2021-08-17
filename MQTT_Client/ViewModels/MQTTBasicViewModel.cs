using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MQTT_Client.ViewModels;

namespace MQTT_Client.ViewModels
{
    public class MQTTBasicViewModel : INotifyPropertyChanged, IMQTTViewModel
    {
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
            */
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
            */
        }

        public void SubscribeTopic(string Topic)
        {

        }
        public void PublishMessage()
        {
            
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
    }
}