﻿using ImageServiceGUI.Communication;
using ImageServiceGUI.Infastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

        /// <summary>
        /// Given Log from the client class, the method parse the Log
        /// check if its a full table or a single Log and send to the VM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public void LogRecieved(object sender, MessageEventArgs msg)
        {
            string log = msg.Message;
            JObject obj = JObject.Parse(log);
            // case of single Log update
            if (this.startGettingLogs)
            {
                LogMessage logMessage = MessageToLogMessage(msg);
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    //Handlers.Add(str);
                    logList.Add(logMessage);
                });
                return;
            }
            // if log was given before the Log Table was initialized
            if (obj["firstTime"].Equals("false") || obj["firstTime"] == null)
            {
                return;
            }
            // case if it's the first time the Log table was given
            if (obj["firstTime"].ToString().Equals("true"))
            {
                Dictionary<int, string[]> map = new Dictionary<int, string[]>
                    (JsonConvert.DeserializeObject<Dictionary<int, string[]>>(obj["logMap"].ToString()));
                int i;
                int size = map.Count;
                for (i = 1; i < size; i++)
                {
                    string[] str = map[i];
                    string type = GetType(str[0]);
                    string message = str[1];
                    LogMessage logMessage = new LogMessage(type, message);
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        //Handlers.Add(str);
                        logList.Add(logMessage);
                    });
                }
                LogListProp = logList;
                startGettingLogs = true;
            }
        }

        /// <summary>
        /// The method converts from the message Type to the required one.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string GetType(string msg)
        {
            if (msg.Equals("Information"))
            {
                return "INFO";
            }
            else if (msg.Equals("Warning"))
            {
                return "WARNING";
            }
            else
            {
                return "ERROR";
            }
        }

        /// <summary>
        /// The method asks from the client class to get the log table
        /// </summary>
        public void GetLog()
        {
            int msg = (int)Infrastructure.Enums.CommandEnum.LogCommand;
            this.client.SendData(msg.ToString());
        }

        /// <summary>
        /// The method get the MessageEventArgs and retrun a logMessage Class
        /// which fits the to the table
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private LogMessage MessageToLogMessage(MessageEventArgs msg)
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
