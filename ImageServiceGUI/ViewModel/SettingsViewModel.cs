using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI
{
    class SettingsViewModel
    {
        private ISettingsModel settingModel;
       
        public SettingsViewModel()
        {
            this.settingModel = new SettingsModel();
        }
    }
}
