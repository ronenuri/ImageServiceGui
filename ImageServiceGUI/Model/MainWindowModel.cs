using ImageServiceGUI.Communication;
using System;
using ImageServiceGUI.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    class MainWindowModel : IMainWindowModel, INotifyPropertyChanged
    {
        private Client client;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool isConnected;
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
            set
            {
                this.isConnected = value;
                NotifyPropertyChanged("IsConnected");
            }
        }


        public MainWindowModel()
        {
            this.client = Client.Instance;
            // Getting information whether connection was sucessfull or not
            this.IsConnected = client.IsConnected;
        }
    }
}
