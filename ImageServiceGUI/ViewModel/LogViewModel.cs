using ImageServiceGUI.Infastructure;
using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ImageServiceGUI.ViewModel
{


    class LogViewModel : INotifyPropertyChanged
    {
        private ILogModel logModel;

        public List<LogMessage> LogList
        {
            get
            {
                return this.logModel.LogListProp;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public LogViewModel()
        {
            this.logModel = new LogModel();
            //LogList = new List<LogMessage>();
            LogList.Add(new LogMessage("INFO", "sdjvnsdkd"));
            LogList.Add(new LogMessage("ERROR", "sdjvnsdkd"));
            LogList.Add(new LogMessage("WARNING", "sdjvnsdkd"));
        }

        public void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
