using ImageServiceGUI.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class MainWindowModel : IMainWindowViewModel
    {
        private Client client;

        public MainWindowModel()
        {
            this.client = Client.Instance;
        }

        bool IsConnected
        {
            get
            {
                return this.client.isConnected;
            }
        }

    }
}
