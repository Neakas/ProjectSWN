using System.Text.RegularExpressions;
using System.Windows;
using SWN.Controller;
using SWN.Networking;
using SWN.Service_References.SWNServiceReference;

namespace SWN.Forms

{
    public partial class Registration : Window
    {
        public Client localclient;

        public Registration()
        {
            InitializeComponent();
            tbIPPort.Text = SettingHandler.GetIpPort();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxUserName.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            localclient = new Client();
            SettingHandler.SetIpPort(tbIPPort.Text);
            ServerConnection SC;
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Bitte Email-Addresse eingeben";
                textBoxEmail.Focus();
            }
            else if (
                !Regex.IsMatch(textBoxEmail.Text,
                    @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Bitte eine gültige EMail-Addresse eingeben";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                if (ServerConnection.CurrentInstance == null)
                {
                    SC = new ServerConnection();
                }
                else
                {
                    SC = ServerConnection.CurrentInstance;
                }
                localclient.UserName = textBoxUserName.Text;
                localclient.eMail = textBoxEmail.Text;
                var password = passwordBox1.Password;
                var E = new Encryption(passwordBox1.Password);
                localclient.encPassword = E.EncryptStringToBytes(passwordBox1.Password);


                var RegSuccessful = await SC.tryReg(localclient);

                if (RegSuccessful)
                {
                    errormessage.Text = "Erfolgreich Registriert";
                }
                //TODOLOW: Needs a Close for the Reg Connection
            }
        }
    }
}