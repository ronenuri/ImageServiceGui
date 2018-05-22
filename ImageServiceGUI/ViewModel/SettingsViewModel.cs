using ImageServiceGUI.Model;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public string ChosenHandler
        {
            get { return settingModel.ChosenHandler; }
            set
            {
                settingModel.ChosenHandler = value;
                NotifyPropertyChanged("ChosenHandler");
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

            this.settingModel = new SettingsModel();
            RemoveCommand = new DelegateCommand<object>(this.OnRemove, this.CanRemove);
            PropertyChanged += RemovePropertyChange;
            this.settingModel.PropertyChanged += this.PropertyChanged;

        }

        /// <summary>
        /// Determine whether or not the remove button can be pressed
        /// </summary>
        /// <param name="sender"> Event sender</param>
        /// <param name="e"></param>
        private void RemovePropertyChange(object sender, PropertyChangedEventArgs e)
        {
            var command = this.RemoveCommand as DelegateCommand<object>;
            command.RaiseCanExecuteChanged();
        }

        public ICommand RemoveCommand { get; private set; }

        /// <summary>
        /// Remove command action, sending the proper command to the server
        /// </summary>
        /// <param name="obj"></param>
        private void OnRemove(object obj)
        {
            this.settingModel.RemoveHandler(this.ChosenHandler);
        }

        /// <summary>
        /// Determine whether or not the remove button can be pressed
        /// </summary>
        /// <param name="obj"> Event sender</param>
        /// <returns></returns>
        private bool CanRemove(object obj)
        {
            // Return true only if a path has been chosen
            if (string.IsNullOrEmpty(this.ChosenHandler))
            {
                return false;
            }
            return true;
        }

    }
}
