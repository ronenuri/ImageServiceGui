using ImageServiceGUI.Communication;
using ImageServiceGUI.Infastructure;
using ImageServiceGUI.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    public class LogModel : ILogModel, INotifyPropertyChanged
    {
        private Client client;
        private bool startGettingLogs;


        public event PropertyChangedEventHandler PropertyChanged;


        public LogModel()
        {
            this.client = Client.Instance;
            this.client.LoggerCommandRecievd += LogRecieved;
            this.startGettingLogs = false;
            this.logList = new ObservableCollection<LogMessage>();
        }

        protected void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<LogMessage> logList;
        public ObservableCollection<LogMessage> LogListProp
        {
            get { return this.logList; }
            set
            {
                logList = value;
                NotifyPropertyChanged("LogListProp");
            }
        }


        public void LogRecieved(object sender, SettingsEventArgs msg)
        {
            string log = msg.Message;
            JObject obj = JObject.Parse(log);
            if (this.startGettingLogs)
            {
                LogMessage logMessage = MessageToLogMessage(msg);
                logList.Add(logMessage);
                return;
            }
            if (obj["firstTime"].Equals("false") || obj["firstTime"] == null)
            {
                return;
            }
            if (obj["firstTime"].ToString().Equals("true"))
            {
                Dictionary<int, string[]> map = new Dictionary<int, string[]>
                    (JsonConvert.DeserializeObject<Dictionary<int, string[]>>(obj["logMap"].ToString()));
                int i;
                int size = map.Count;
                for (i=1; i<size; i++)
                {
                    string[] str = map[i];
                    string type = str[0];
                    string message = str[1];
                    LogMessage logMessage = new LogMessage(type, message);
                    logList.Add(logMessage);
                }
                LogListProp = logList;
                startGettingLogs = true;
            }
        }


        public void GetLog()
        {
            int msg = (int)Infrastructure.Enums.CommandEnum.LogCommand;
            this.client.SendData(msg.ToString());
        }


        private LogMessage MessageToLogMessage(SettingsEventArgs msg)
        {
            JObject obj = JObject.Parse(msg.Message);
            string[] str = JsonConvert.DeserializeObject<string[]>(obj["logValue"].ToString());
            string type = str[0];
            string message = str[1];
            LogMessage logMessage = new LogMessage(type, message);
            return logMessage;
        }

    }
}
