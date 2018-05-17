using ImageServiceGUI.Infastructure;
using ImageServiceGUI.Infastructure.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Communication
{
    public class Client : IClient
    {
        private static Client instance;
        private Client() { }
        private TcpClient client;

        public event EventHandler<MessageRecievedEventArgs> LoggerCommandRecievd;
        public event EventHandler<SettingsEventArgs> SettingsConfigRecieved;
        public event EventHandler<SettingsEventArgs> SettingsCloseHandlerRecieved;

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                    instance.StartClient();
                }
                return instance;
            }
        }

        public void StartClient()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            this.client = new TcpClient();
            client.Connect(ep);
            //Console.WriteLine("You are connected");
            NetworkStream stream = client.GetStream();

            //Task writingTask = new Task(() =>
            //{
            //    using (BinaryWriter writer = new BinaryWriter(stream))
            //    {
            //        while (true)
            //        {

            //            // Send data to server
            //            string line = Console.ReadLine();
            //            writer.Write(line);
            //            writer.Flush();
            //        }
            //    }
            //});

            Task readingTask = new Task(() =>
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    while (true)
                    {
                        string result = reader.ReadString();
                        ParseAndSend(result);
                    }
                }
            });

            readingTask.Start();
            //writingTask.Wait();

        }


        public void SendData(string data)
        {
            NetworkStream stream = client.GetStream();
            Task writingTask = new Task(() =>
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
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
                MessageRecievedEventArgs e = new MessageRecievedEventArgs((int)Infrastructure.Enums.CommandEnum.LogCommand, msg);
                LoggerCommandRecievd?.Invoke(this, e);
            } else if(x == (int)Infrastructure.Enums.CommandEnum.CloseCommand)
            {
                SettingsEventArgs e = new SettingsEventArgs((int)Infrastructure.Enums.CommandEnum.CloseCommand, msg);
                SettingsCloseHandlerRecieved?.Invoke(this, e);
            }
        }
    }
}

