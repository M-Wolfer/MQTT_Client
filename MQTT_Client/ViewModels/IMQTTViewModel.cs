using System.Collections.Generic;
using System.ComponentModel;
namespace MQTT_Client.ViewModels
{
    public interface IMQTTViewModel
    {
        bool IsBusy { get; set; }
        string Topic { get; set; }
        string Message { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        string ReadBuffer(string Topic);
        void FillBuffer(string Topic, string Message);
        void InitBuffer();
        void SubscribeTopic(string Topic);
        void ClientPublish();
    }
}