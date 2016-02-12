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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using amexus.Encryption;
using System.ServiceModel;
using System.Runtime.Serialization;
using SWN.SWNServiceReference;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace SWN

{
    public partial class Login : Window
    {
        public static Login CurrentLoginWindow;
        public SWNServiceClient proxy = null;
        public Client receiver = null;
        public Client localclient = null;

        public Login()
        {
            CurrentLoginWindow = this;
            InitializeComponent();
            tbIPPort.Text = SettingHandler.GetIPPort();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        { 
            proxy = null;
            errormessage.Text = "";
            ProcessUITasks();

            this.localclient = new Client();

            SettingHandler.SetIPPort(tbIPPort.Text);

            if (textBoxUsername.Text.Length == 0)
            {
                errormessage.Text = "Please enter a Username.";
                textBoxUsername.Focus();
            }
            else
            {
                    this.localclient.UserName = textBoxUsername.Text;
                    string password = passwordBox1.Password;
                    Encryption E = new Encryption(passwordBox1.Password);
                    this.localclient.encPassword = E.EncryptStringToBytes(passwordBox1.Password);
                    ServerConnection SC = new ServerConnection();
                    int SuccessfullLogin = SC.ClientInit(localclient);
                    if (SuccessfullLogin == 1)
                    {
                        MainWindow MW = new MainWindow();
                        MW.localclient = localclient;
                        MW.receiver = receiver;
                        MW.proxy = ServerConnection.SWNClient;
                        MW.UserName = this.localclient.UserName;
                        SettingHandler.SetIsLoggedIn(true);
                        MW.Show();
                        List<String> UserList = SC.GrabLoggedInUsers();
                        MW.lbUserOnline.ItemsSource = UserList;
                    Close();
                    }
                    else if (SuccessfullLogin == 0)
                    {
                        errormessage.Text = "Please enter an existing Username and Password";
                    }
                    else if (SuccessfullLogin == -1 || SuccessfullLogin == -2)
                    {
                        return;
                    }
            }
        }

        public static void ProcessUITasks()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate (object parameter) {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }

        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btLogin_Click(this, null);
            }
        }

        private void passwordBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btLogin_Click(this, null);
            }
        }

        private void btOverride_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            Close();
        }

        public void NotifyUserJoined(string UserName)
        {

        }
    }
}