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

        event EventHandler<SettingsEventArgs> LoggerCommandRecievd;
        event EventHandler<SettingsEventArgs> SettingsConfigRecieved;
        event EventHandler<SettingsEventArgs> SettingsCloseHandlerRecieved;
    }
}
