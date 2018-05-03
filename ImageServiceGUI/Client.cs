using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI
{
    public class Client
    {
        private static Client instance;
        private Client() { }
        private TcpClient client;
        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Client();
                }
                return instance;
            }
        }

        public void StartClient()
        {
            while(true)
            {
                string result;
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                this.client = new TcpClient();
                client.Connect(ep);
            }
            
        }
    }
}
