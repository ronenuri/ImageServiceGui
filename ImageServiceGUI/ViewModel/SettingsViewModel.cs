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

        public ObservableCollection<string> Handlers {
            get{ return this.settingModel.Handlers;}
            set { this.settingModel.Handlers = new ObservableCollection<string>(value);}
        }

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
        public string Output
        {
            get
            {
                return this.settingModel.Output;
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
                return this.settingModel.LogName;
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

            this.RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            this.settingModel = new SettingsModel();
            this.settingModel.PropertyChanged += OnPropertyChanged;

            //Handlers = new ObservableCollection<string>();
            //this.Handlers.Add("ssdsad");
            this.settingModel.GetConfig();
            this.PropertyChanged += RemovePropertyChange;
        }

        public void updateHandler(object sender, PropertyChangedEventArgs e)
        {
            this.Handlers = e.
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
