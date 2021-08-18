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
        void Connect(object sender, DoWorkEventArgs e);
        void Pub(object sender, DoWorkEventArgs e);
        void Sub(object sender, DoWorkEventArgs e);
        void ClientPublish();
        void ClientSubscribe();

        /*        
        string ReadBuffer(string Topic);
        void FillBuffer(string Topic, string Message);
        void InitBuffer();
        */
    }
}