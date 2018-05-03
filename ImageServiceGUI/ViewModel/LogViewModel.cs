using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ImageServiceGUI.ViewModel
{
    class LogViewModel
    {
        private ILogModel logModel;

        public LogViewModel()
        {
            this.logModel = new LogModel();
           
        }

    }
}
