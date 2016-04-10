using System.Text.RegularExpressions;
using System.Windows;
using SWN.Controller;
using SWN.Networking;
using SWN.SWNServiceReference;

namespace SWN.Forms

{
    public partial class Registration
    {
        public Client Localclient;

        public Registration()
        {
            InitializeComponent();
            TbIpPort.Text = SettingHandler.GetIpPort();
        }

        private void Login_Click( object sender, RoutedEventArgs e )
        {
            var login = new Login();
            login.Show();
            Close();
        }

        private void button2_Click( object sender, RoutedEventArgs e )
        {
            Reset();
        }

        public void Reset()
        {
            TextBoxUserName.Text = "";
            TextBoxEmail.Text = "";
            PasswordBox1.Password = "";
            PasswordBoxConfirm.Password = "";
        }

        private void button3_Click( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private async void Submit_Click( object sender, RoutedEventArgs e )
        {
            Localclient = new Client();
            SettingHandler.SetIpPort(TbIpPort.Text);
            if (TextBoxEmail.Text.Length == 0)
            {
                Errormessage.Text = "Bitte Email-Addresse eingeben";
                TextBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(TextBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                Errormessage.Text = "Bitte eine gültige EMail-Addresse eingeben";
                TextBoxEmail.Select(0, TextBoxEmail.Text.Length);
                TextBoxEmail.Focus();
            }
            else
            {
                var sc = ServerConnection.CurrentInstance ?? new ServerConnection();
                Localclient.UserName = TextBoxUserName.Text;
                Localclient.EMail = TextBoxEmail.Text;
                Localclient.EncPassword = new Encryption(PasswordBox1.Password).EncryptStringToBytes(PasswordBox1.Password);

                if (await sc.TryReg(Localclient))
                {
                    Errormessage.Text = "Erfolgreich Registriert";
                }
                //TODOLOW: Needs a Close for the Reg Connection
            }
        }
    }
}