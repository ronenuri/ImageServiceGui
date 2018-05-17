using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Infastructure.Logging
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public MessageRecievedEventArgs (int stat, string msg)
        {
            this.Status = stat;
            this.Message = msg;
        }

    }
}
