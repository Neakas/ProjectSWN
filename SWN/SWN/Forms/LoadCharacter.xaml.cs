using System.Windows;
using SWN.Networking;
using SWN.Service_References.SWNServiceReference;

namespace SWN.Forms
{
    /// <summary>
    ///     Interaction logic for LoadCharacter.xaml
    /// </summary>
    public partial class LoadCharacter : Window
    {
        public LoadCharacter()
        {
            InitializeComponent();
            QueryChars();
        }

        private void btLoad_Click(object sender, RoutedEventArgs e)
        {
            if (lbCharacters.SelectedItem != null)
            {
                var cnc = new CreateNewCharacter(lbCharacters.SelectedItem as Character);
                cnc.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Please Select a Character to Load");
            }
        }

        private void QueryChars()
        {
            lbCharacters.ItemsSource =
                ServerConnection.LocalServiceClient.RequestSavedCharacters(MainWindow.CurrentInstance.LocalCient);
            lbCharacters.DisplayMemberPath = "Name";
        }
    }
}