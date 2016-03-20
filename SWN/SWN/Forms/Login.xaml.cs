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
using System.Threading.Tasks;

namespace SWN

{
    public partial class Login : Window
    {
        private static Login currentinstance;

        public static Login CurrentInstance
        {
            get { return currentinstance; }
            set { currentinstance = value; }
        }

        public Login()
        {
            InitializeComponent();
            CurrentInstance = this;
            tbIPPort.Text = SettingHandler.GetIPPort();
        }

        private Client userclient;

        public Client UserClient
        {
            get { return userclient; }
            set { userclient = value; }
        }

        private async void btLogin_Click(object sender, RoutedEventArgs e)
        {
            ServerConnection SC;
            if (ServerConnection.CurrentInstance == null)
            {
                SC = new ServerConnection();
            }
            else
            {
                SC = ServerConnection.CurrentInstance;
            }

            UserClient = new Client();
            SettingHandler.SetIPPort(tbIPPort.Text);

            if (textBoxUsername.Text.Length == 0)
            {
                errormessage.Text = "Please enter a Username.";
                textBoxUsername.Focus();
                return;
            }
            errormessage.Text = "";
            biBusy.IsBusy = true;
            //Force UI Redraw
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle);

            UserClient.UserName = textBoxUsername.Text;
            Encryption E = new Encryption(passwordBox1.Password);
            UserClient.encPassword = E.EncryptStringToBytes(passwordBox1.Password);
            bool SuccessfullLogin = await SC.tryLogin(UserClient);
            if (SuccessfullLogin)
            {
                MainWindow MW = new MainWindow();

                MW.LocalCient = UserClient;
                SettingHandler.SetIsLoggedIn(true);
                MW.Show();
                Close();
            }
            else
            {
                biBusy.IsBusy = false;
            }
        }

        public static void ProcessUITasks()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate (object parameter)
            {
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
            if (e.Key == Key.Enter)
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

        private void btSound_Click(object sender, RoutedEventArgs e)
        {
            if (App.musicplaying)
            {
                App.mplayer.Pause();
                App.musicplaying = false;
            }
            else
            {
                App.mplayer.Play();
                App.musicplaying = true;
            }
        }
    }
}