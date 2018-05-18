﻿using ImageServiceGUI.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface ILogModel
    {
        List<LogMessage> LogListProp { get; set; }
        void GetLog();
    }
}
