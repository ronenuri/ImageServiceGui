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
    public class LogMessage
    {
        public string Type { get; private set; }
        public string Message { get; private set; }

        public LogMessage(string type, string msg)
        {
            this.Type = type;
            this.Message = msg;
        }
    }

    class LogViewModel : INotifyPropertyChanged
    {
        private ILogModel logModel;

        public List<LogMessage> logList { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public LogViewModel()
        {
            this.logModel = new LogModel();
            logList = new List<LogMessage>();
            logList.Add(new LogMessage("INFO", "sdjvnsdkd"));
            logList.Add(new LogMessage("ERROR", "sdjvnsdkd"));
            logList.Add(new LogMessage("WARNING", "sdjvnsdkd"));
        }

    }
}
