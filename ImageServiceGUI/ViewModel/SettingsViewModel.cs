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

        public void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<string> Handlers
        {
            get
            {
                return this.settingModel.Handlers;
            }
        }

        private string m_chosenHandler;
        public string ChosenHandler
        {
            get { return settingModel.ChosenHandler; }
            set
            {
                settingModel.ChosenHandler = value;
                //NotifyPropertyChanged("ChosenHandler");
            }
        }

        public string Output
        {
            get
            {
                return settingModel.Output;
            }
        }
        public string SourceName
        {
            get
            {
                return this.settingModel.SourceName;
            }
        }
        public string LogName
        {
            get
            {
                return settingModel.LogName;
            }
        }
        public int ThumbnailSize
        {
            get
            {
                return this.settingModel.ThumbnailSize;
            }
        }

        public SettingsViewModel()
        {
            //Handlers = new ObservableCollection<string>();
            this.settingModel = new SettingsModel();

            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            this.PropertyChanged += RemovePropertyChange;
            settingModel.PropertyChanged += this.PropertyChanged;

            this.settingModel.GetConfig();
        }

        private void RemovePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
        }

        public ICommand RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            this.settingModel.RemoveHandler(this.ChosenHandler);
            //this.Handlers.Remove(this.ChosenHandler);
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
