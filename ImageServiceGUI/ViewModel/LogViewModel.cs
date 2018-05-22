using ImageServiceGUI.Infastructure;
using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ImageServiceGUI.ViewModel
{

    class LogViewModel : INotifyPropertyChanged
    {
        private ILogModel logModel;

        public ObservableCollection<LogMessage> LogList
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
            this.logModel.PropertyChanged += this.PropertyChanged;
            this.logModel.GetLog();
        }
    }
}
