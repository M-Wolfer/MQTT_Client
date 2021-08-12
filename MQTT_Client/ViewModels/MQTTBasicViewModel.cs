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
        public void SubscribeTopic(string Topic)
        {

        }
        public void PublishMessage(string Topic, string Message)
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