using System.Windows;

namespace SWNAdmin.Forms.FactionManager
{
    /// <summary>
    ///     Interaction logic for FactionManager.xaml
    /// </summary>
    public partial class FactionManager
    {
        public FactionManager()
        {
            InitializeComponent();
        }

        private void MenuItemCreate_Click( object sender, RoutedEventArgs e )
        {
            var cf = new CreateFaction();
            cf.ShowDialog();
        }
    }
}