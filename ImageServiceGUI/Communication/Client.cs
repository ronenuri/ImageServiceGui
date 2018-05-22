using ImageServiceGUI.Infastructure;
using ImageServiceGUI.Infastructure.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageServiceGUI.Communication
{
    public class Client : IClient
    {
        private static Client instance;
        private Client() { }
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected;
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
            set
            { this.isConnected = value; }
        }

        public event EventHandler<SettingsEventArgs> LoggerCommandRecievd;
        public event EventHandler<SettingsEventArgs> SettingsConfigRecieved;
        public event EventHandler<SettingsEventArgs> SettingsCloseHandlerRecieved;

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                    Instance.IsConnected = instance.StartClient();
                    Thread.Sleep(100);
                }
                return instance;
            }
        }

        public bool StartClient()
        {
            try { 
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            this.client = new TcpClient();
            client.Connect(ep);
            this.stream = client.GetStream();

            
                Task readingTask = new Task(() =>
                {
                    BinaryReader reader = new BinaryReader(stream);
                    {
                        while (true)
                        {
                            string result = reader.ReadString();
                            App.Current.Dispatcher.BeginInvoke((Action)delegate
                            {
                                ParseAndSend(result);
                            });
                        }
                    }
                });
                readingTask.Start();
                return true;
            } catch
            {
                return false;
            }
            
        }


        public void SendData(string data)
        {
            if (!IsConnected)
            {
                return;
            }
            Task writingTask = new Task(() =>
            {
                BinaryWriter writer = new BinaryWriter(this.stream);
                {
                        // Send data to server
                        writer.Write(data);
                        writer.Flush();
                }
            });
            writingTask.Start();

        }

        public void ParseAndSend(string msg)
        {
            JObject obj = JObject.Parse(msg);
            int.TryParse(obj["CommandEnum"].ToString(), out int x);
            if (x == (int)Infrastructure.Enums.CommandEnum.GetConfigCommand)
            {
                SettingsEventArgs e = new SettingsEventArgs((int)Infrastructure.Enums.CommandEnum.GetConfigCommand, msg);
                SettingsConfigRecieved?.Invoke(this, e);
            }
            else if (x == (int)Infrastructure.Enums.CommandEnum.LogCommand)
            {
                SettingsEventArgs e = new SettingsEventArgs((int)Infrastructure.Enums.CommandEnum.LogCommand, msg);
                LoggerCommandRecievd?.Invoke(this, e);
            }
            else if(x == (int)Infrastructure.Enums.CommandEnum.CloseCommand)
            {
                SettingsEventArgs e = new SettingsEventArgs((int)Infrastructure.Enums.CommandEnum.CloseCommand, msg);
                SettingsCloseHandlerRecieved?.Invoke(this, e);
            }
        }
    }
}

