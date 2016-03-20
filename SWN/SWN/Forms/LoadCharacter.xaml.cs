using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SWN
{
    /// <summary>
    /// Interaction logic for LoadCharacter.xaml
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
                CreateNewCharacter cnc = new CreateNewCharacter(lbCharacters.SelectedItem as SWNServiceReference.Character);
                cnc.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Select a Character to Load");
            }
        }

        private void QueryChars()
        {
            lbCharacters.ItemsSource = ServerConnection.LocalServiceClient.RequestSavedCharacters(MainWindow.CurrentInstance.LocalCient);
            lbCharacters.DisplayMemberPath = "Name";
        }
    }
}
