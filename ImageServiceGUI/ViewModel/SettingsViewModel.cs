using ImageServiceGUI.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImageServiceGUI.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private ISettingsModel settingModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<string> Handlers { get; private set; }
        private string m_chosenHandler;
        public string ChosenHandler
        {
            get { return m_chosenHandler; }
            set
            {
                m_chosenHandler = value;
                OnPropertyChanged("ChosenHandler");
            }
        }
        public string Output { get; private set; }
        public string SourceName { get; private set; }
        public string LogName { get; private set; }
        public int ThumbnailSize { get; private set; }

        public SettingsViewModel()
        {
            this.settingModel = new SettingsModel();
            Handlers = new ObservableCollection<string>
            {
                "PLEASE",
                "FUCKING",
                "WORK",
                "THANK YOU",
                "VERY",
                "LONG",
                "FOR",
                "SCROLLING"
            };

            Output = "out";
            SourceName = "source";
            LogName = "log";
            ThumbnailSize = 120;

            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            this.PropertyChanged += RemovePropertyChange;
        }

        private void RemovePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
        }

        public ICommand RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            //this.settingModel.RemoveHandler(this.ChosenHandler);
            this.Handlers.Remove(this.ChosenHandler);
        }

        private bool CanRemove(object obj)
        {
            if (string.IsNullOrEmpty(this.ChosenHandler))
            {
                return false;
            }
            return true;
        }

    }
}
