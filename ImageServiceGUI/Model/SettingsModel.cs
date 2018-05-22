﻿using ImageServiceGUI.Communication;
using ImageServiceGUI.Infastructure;
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
using System.Threading;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class SettingsModel : ISettingsModel, INotifyPropertyChanged
    {
        private Client client;

        public SettingsModel()
        {
            this.client = Client.Instance;
            this.m_handlers = new ObservableCollection<string>();
            this.client.SettingsConfigRecieved += SettingsConfigRecieved;
            this.client.SettingsCloseHandlerRecieved += HandlerRemoveRecived;
            this.GetConfig();
        }

        /// <summary>
        /// Updating our settings configuration
        /// </summary>
        /// <param name="sender"> Event sender</param>
        /// <param name="msg"> The message recived from server</param>
        public void SettingsConfigRecieved(object sender, MessageEventArgs msg)
        {
            // Parsing our message
            string message = msg.Message;
            JObject obj = JObject.Parse(message);
            // Updating each configuratoin field
            Output = obj["Output"].ToString();
            SourceName = obj["SourceName"].ToString();
            LogName = obj["LogName"].ToString();
            int.TryParse(obj["thumbnailSize"].ToString(), out int thumbnailSize);
            ThumbnailSize = thumbnailSize;
            // Parsing our handlerPaths
            string[] handlerPaths = JsonConvert.DeserializeObject<string[]>(obj["handlersPaths"].ToString());
            // Adding each one to our observable collection list
            foreach (string str in handlerPaths)
            {
                App.Current.Dispatcher.BeginInvoke((Action)delegate
                {
                    Handlers.Add(str);
                });
            }
        }

        /// <summary>
        /// Removing a handler
        /// </summary>
        /// <param name="sender"> Event sender</param>
        /// <param name="e">The handler path to be removed</param>
        public void HandlerRemoveRecived(object sender, MessageEventArgs e)
        {
            JObject msg = JObject.Parse(e.Message);
            string path = msg["Directory"].ToString();
            // Checking if the path is indeed on the list and removing it
            if (this.Handlers.Contains(path))
            {
                Handlers.Remove(path);
                NotifyPropertyChanged("Handlers");
            }

        }

        /// <summary>
        /// Sending the server the configuration command
        /// </summary>
        public void GetConfig()
        {
            int msg = (int)Infrastructure.Enums.CommandEnum.GetConfigCommand;
            this.client.SendData(msg.ToString());
        }

        /// <summary>
        /// Sending the server to remove a handler
        /// </summary>
        /// <param name="handlerPath"> The handlers path</param>
        public void RemoveHandler(string handlerPath)
        {
            int msg = (int)Infrastructure.Enums.CommandEnum.CloseCommand;
            this.client.SendData(msg.ToString() + " " + this.ChosenHandler);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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

        private string m_chosenHandler;
        public string ChosenHandler
        {
            get { return m_chosenHandler; }
            set
            {
                m_chosenHandler = value;
                NotifyPropertyChanged("ChosenHandler");
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

