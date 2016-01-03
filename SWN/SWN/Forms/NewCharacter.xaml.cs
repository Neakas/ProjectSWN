using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
using SWNAdmin;

namespace SWN
{
    /// <summary>
    /// Interaction logic for CharacterWindow.xaml
    /// </summary>
    public partial class NewCharacter : Window
    {
        public CharacterController returnedChar;
        public NewCharacter()
        {
            InitializeComponent();
            tbPlayer.Text = SettingHandler.GetUserName();
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            //Init Character
            InitCharacter();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            //Close this Window
            this.Close();
        }
        private void InitCharacter()
        {
            //try
            //{
            //    CharacterController UserCharacter = new CharacterController();
            //    UserCharacter.Name = tbName.Text;
            //    UserCharacter.Player = tbPlayer.Text;
            //    UserCharacter.PointTotal = Int32.Parse(tbUnspendPoints.Text);

            //    ServerConnection sv = new ServerConnection();
            //    string UserName = SettingHandler.GetUserName();
            //    returnedChar = sv.TransferCharacter(UserName, UserCharacter);
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());
            //    throw;
            //}
            //this.Close();
        }
    }
}
