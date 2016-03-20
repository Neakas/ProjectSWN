using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using amexus.Encryption;
using SWN.SWNServiceReference;

namespace SWN

{
    public partial class Registration : Window
    {
        public Client localclient = null;
        public Registration()
        {
            InitializeComponent();
            tbIPPort.Text = SettingHandler.GetIPPort();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
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
            SettingHandler.SetIPPort(tbIPPort.Text);
            ServerConnection SC;
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Bitte Email-Addresse eingeben";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
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
                string password = passwordBox1.Password;
                Encryption E = new Encryption(passwordBox1.Password);
                localclient.encPassword = E.EncryptStringToBytes(passwordBox1.Password);

                
                bool RegSuccessful = await SC.tryReg(localclient);

                if (RegSuccessful)
                {
                    errormessage.Text = "Erfolgreich Registriert";
                }
                //TODOLOW: Needs a Close for the Reg Connection
            }
        }
    }
}