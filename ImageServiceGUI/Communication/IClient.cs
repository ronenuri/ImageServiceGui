﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Communication
{
    interface IClient
    {
        void SendData(string data);
    }
}