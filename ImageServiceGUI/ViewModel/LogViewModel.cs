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

        public event PropertyChangedEventHandler PropertyChanged;

        public LogViewModel()
        {
            this.logModel = new LogModel();
        }

    }
}
