using ImageServiceGUI.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;


namespace ImageServiceGUI.View
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : UserControl
    {
        public LogWindow()
        {
            InitializeComponent();
            this.DataContext = new LogViewModel();
        }

        // Simple fix for datagrid to be able to scroll with mouse wheel, Dor said it's fine!
        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta / 3);
        }
    }
}
