using ImageServiceGUI.Infastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface ILogModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<LogMessage> LogListProp { get; set; }
        void GetLog();
    }
}
