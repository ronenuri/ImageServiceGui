using ImageServiceGUI.Infastructure;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ImageServiceGUI.Communication
{
    /// <summary>
    /// Our communication client singelton
    /// </summary>
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

        //Events for each gui tab and part
        public event EventHandler<MessageEventArgs> LoggerCommandRecievd;
        public event EventHandler<MessageEventArgs> SettingsConfigRecieved;
        public event EventHandler<MessageEventArgs> SettingsCloseHandlerRecieved;

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                    // Creating a connection
                    Instance.IsConnected = instance.StartClient();
                    Thread.Sleep(100);
                }
                return instance;
            }
        }

        /// <summary>
        /// Starting our clients connection with the server
        /// </summary>
        /// <returns>Bool whether or not connection was successfull</returns>
        public bool StartClient()
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                this.client = new TcpClient();
                client.Connect(ep);
                this.stream = client.GetStream();
                // Creating a seprate reading task always listening to the server
                Task readingTask = new Task(() =>
                {
                    BinaryReader reader = new BinaryReader(stream);
                    {
                        while (true)
                        {
                            string result = reader.ReadString();
                            // Sending each result to be handled by the correct GUI tab
                            App.Current.Dispatcher.BeginInvoke((Action)delegate
                            {
                                ParseAndSend(result);
                            });
                        }
                    }
                });
                readingTask.Start();
                // Returning connection was successfull
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Sending comands to our server
        /// </summary>
        /// <param name="data"> String of the clients command</param>
        public void SendData(string data)
        {
            if (!IsConnected)
            {
                return;
            }
            // Creating a writing task sending the command to the server
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

        /// <summary>
        /// Parsing each server message and notyfing the appropriate class
        /// </summary>
        /// <param name="msg"></param>
        public void ParseAndSend(string msg)
        {
            // Parsing our jobjects command enum
            JObject obj = JObject.Parse(msg);
            int.TryParse(obj["CommandEnum"].ToString(), out int command);
            MessageEventArgs e = new MessageEventArgs((int)Infrastructure.Enums.CommandEnum.GetConfigCommand, msg);
            // Sending the server message to the right gui tab via event
            if (command == (int)Infrastructure.Enums.CommandEnum.GetConfigCommand)
            {
                SettingsConfigRecieved?.Invoke(this, e);
            }
            else if (command == (int)Infrastructure.Enums.CommandEnum.LogCommand)
            {
                LoggerCommandRecievd?.Invoke(this, e);
            }
            else if (command == (int)Infrastructure.Enums.CommandEnum.CloseCommand)
            {
                SettingsCloseHandlerRecieved?.Invoke(this, e);
            }
        }
    }
}

