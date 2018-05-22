using ImageServiceGUI.Model;
using System.ComponentModel;

namespace ImageServiceGUI.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public bool IsConnected
        {
            get
            {
                return this.mainWindowModel.IsConnected;
            }
        }

        IMainWindowModel mainWindowModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            this.mainWindowModel = new MainWindowModel();
            this.mainWindowModel.PropertyChanged += this.PropertyChanged;
        }
    }
}
