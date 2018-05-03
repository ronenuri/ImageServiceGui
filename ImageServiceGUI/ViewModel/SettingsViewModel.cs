using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageServiceGUI.ViewModel
{
    // DONT FORGET TO ADD DOR'S COMMAND NUGET THING!
    class SettingsViewModel : INotifyPropertyChanged
    {
        private ISettingsModel settingModel;
        public ObservableCollection<string> Handlers { get; private set; }
        public string ChosenHandler { get; set; }
        public string Output { get; set; }
        public string SourceName { get; set; }
        public string LogName { get; set; }
        public int ThumbnailSize { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            this.settingModel = new SettingsModel();
            Handlers = new ObservableCollection<string>
            {
                "PLEASE",
                "FUCKING",
                "WORK",
                "THANK YOU"
            };

            ChosenHandler = "THANK YOU";

            Output = "out";
            SourceName = "source";
            LogName = "log";
            ThumbnailSize = 120;
           
        }

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
