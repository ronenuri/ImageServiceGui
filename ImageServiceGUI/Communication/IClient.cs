using ImageServiceGUI.Infastructure;
using ImageServiceGUI.Infastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Communication
{
    interface IClient
    {
        void SendData(string data);

        event EventHandler<MessageEventArgs> LoggerCommandRecievd;
        event EventHandler<MessageEventArgs> SettingsConfigRecieved;
        event EventHandler<MessageEventArgs> SettingsCloseHandlerRecieved;
    }
}
