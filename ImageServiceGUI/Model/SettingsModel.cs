﻿using ImageServiceGUI.Communication;
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
    class SettingsModel : ISettingsModel, INotifyPropertyChanged
    {
        private Client client;

        public SettingsModel()
        {
            this.client = Client.Instance;
            this.GetConfig();

            //this.client

            this.PropertyChanged +=
       delegate (Object sender, PropertyChangedEventArgs e) {
           NotifyPropertyChanged(e.PropertyName);
       };
        }
        

        public void SettingsConfigRecieved(object sender, string msg)
        {
            JObject obj = JObject.Parse(msg);
            Output = obj[""].ToString();
            SourceName = obj[""].ToString();
            LogName = obj[""].ToString();
            int.TryParse(obj[""].ToString(),out int x);
            ThumbnailSize = x;

            JsonConvert.DeserializeObject(obj["handlersPaths"].ToString());
        }
        public void GetConfig()
        {
            this.client.SendData("");
        }

        public void RemoveHandler(string handlerPath)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<string> m_handlers;
        public ObservableCollection<string> Handlers
        {
            get { return m_handlers; }
            set
            {
                m_handlers = value;
                NotifyPropertyChanged("Handlers");
            }
        }

        private string m_output;
        public string Output
        {
            get { return m_output; }
            set
            {
                m_output = value;
                NotifyPropertyChanged("Output");
            }
        }

        private string m_sourceName;
        public string SourceName
        {
            get { return m_sourceName; }
            set
            {
                m_sourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }

        private string m_logName;
        public string LogName
        {
            get { return m_logName; }
            set
            {
                m_logName = value;
                NotifyPropertyChanged("LogName");
            }
        }

        private int m_thumbnailSize;
        public int ThumbnailSize
        {
            get { return m_thumbnailSize; }
            set
            {
                m_thumbnailSize = value;
                NotifyPropertyChanged("ThumbnailSize");
            }
        }

    }
}

