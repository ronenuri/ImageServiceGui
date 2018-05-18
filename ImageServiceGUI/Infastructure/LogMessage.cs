using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Infastructure
{
    public class LogMessage
    {
        public string Type { get; private set; }
        public string Message { get; private set; }

        public LogMessage(string type, string msg)
        {
            this.Type = type;
            this.Message = msg;
        }
    }
}
