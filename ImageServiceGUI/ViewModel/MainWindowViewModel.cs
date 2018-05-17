using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
{
    class MainWindowViewModel
    {
        IMainWindowViewModel mainWindowModel;

        public MainWindowViewModel()
        {
            this.mainWindowModel = new MainWindowModel();
        }

        //bool IsConnected
        //{
        //    get
        //    {
        //        return this.mainWindowModel.isConnected;
        //    }
        //}
   
    }
}
