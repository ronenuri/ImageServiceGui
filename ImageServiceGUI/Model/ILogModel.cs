using ImageServiceGUI.Infastructure;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ImageServiceGUI.Model
{
    interface ILogModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<LogMessage> LogListProp { get; set; }
        void GetLog();
    }
}
