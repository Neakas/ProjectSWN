using System.Windows;

namespace WPFDrawingTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        private MapController _mc;
        public MainWindow()
        {
            _mc = new MapController();
        }
        
    }
}
