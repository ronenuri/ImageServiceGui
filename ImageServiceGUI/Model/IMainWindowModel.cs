using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface IMainWindowModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        bool IsConnected { get; set; }
    }
}
