using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Model
{
    interface ISettingsModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        string Output { get; }
        string SourceName { get; }
        string LogName { get; }
        int ThumbnailSize { get; }
        ObservableCollection<string> Handlers { get; set; } 


        void GetConfig();

        void RemoveHandler(string handlerPath);
    }
}
