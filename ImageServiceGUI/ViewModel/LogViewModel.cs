using ImageServiceGUI.Infastructure;
using ImageServiceGUI.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

        /// <summary>
        /// Constructor of the LogVM
        /// </summary>
        public LogViewModel()
        {
            this.logModel = new LogModel();
            //put the event handler of this class in the model eventHandler
            this.logModel.PropertyChanged += this.PropertyChanged;
            // asks for all the logs configurations
            this.logModel.GetLog();
        }
    }
}
