using System.Windows;
using SWN.Networking;
using SWN.SWNServiceReference;

namespace SWN.Forms
{
    /// <summary>
    ///     Interaction logic for LoadCharacter.xaml
    /// </summary>
    public partial class LoadCharacter
    {
        public LoadCharacter()
        {
            InitializeComponent();
            QueryChars();
        }

        private void btLoad_Click( object sender, RoutedEventArgs e )
        {
            if (LbCharacters.SelectedItem != null)
            {
                var cnc = new CreateNewCharacter(LbCharacters.SelectedItem as Character);
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
            LbCharacters.ItemsSource = ServerConnection.LocalServiceClient.RequestSavedCharacters(MainWindow.CurrentInstance.LocalCient);
            LbCharacters.DisplayMemberPath = "Name";
        }
    }
}