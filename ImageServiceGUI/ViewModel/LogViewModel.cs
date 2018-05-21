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

            this.logModel.PropertyChanged += NPC;
            //this.PropertyChanged += NPC;
            this.logModel.GetLog();
            ////LogList = new List<LogMessage>();
            //LogList.Add(new LogMessage("INFO", "sdjvnsdkd"));
            //LogList.Add(new LogMessage("ERROR", "sdjvnsdkd"));
            //LogList.Add(new LogMessage("WARNING", "sdjvnsdkd"));
        }

        public void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void NPC(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta / 3);
        }
    }
}
