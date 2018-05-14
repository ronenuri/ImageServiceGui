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
        private NetworkStream stream;
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

        private void StartClient()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            this.client = new TcpClient();
            client.Connect(ep);
            this.stream = client.GetStream();
            ReadData();
        }

        private void ReadData()
        {
            Task readTask = new Task(() =>
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    while (true)
                    {
                        string result = reader.ReadString();
                        
                    }
                }
            });
            readTask.Start();
            
        }


        public void SendData(string data)
        {
            Task sendData = new Task(() =>
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        writer.Write(data);
                        writer.Flush();
                    }
                }
            });
            sendData.Start();
        }
    }
}
