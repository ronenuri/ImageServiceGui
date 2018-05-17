using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Infastructure
{
    public class SettingsEventArgs : EventArgs
    {
        public int CommandID { get; set; }
        public string Message { get; set; }

        public SettingsEventArgs(int id, string msg)
        {
            CommandID = id;
            Message = msg;
        }
    }
}
