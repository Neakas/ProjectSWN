using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SWN.Controller;
using SWN.Networking;
using SWN.Service_References.SWNServiceReference;

namespace SWN.Forms

{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            CurrentInstance = this;
            tbIPPort.Text = SettingHandler.GetIpPort();
        }

        public static Login CurrentInstance { get; set; }

        public Client UserClient { get; set; }

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
            SettingHandler.SetIpPort(tbIPPort.Text);

            if (textBoxUsername.Text.Length == 0)
            {
                errormessage.Text = "Please enter a Username.";
                textBoxUsername.Focus();
                return;
            }
            errormessage.Text = "";
            biBusy.IsBusy = true;
            //Force UI Redraw
            Dispatcher.Invoke(() => { }, DispatcherPriority.ContextIdle);

            UserClient.UserName = textBoxUsername.Text;
            var E = new Encryption(passwordBox1.Password);
            UserClient.encPassword = E.EncryptStringToBytes(passwordBox1.Password);
            var SuccessfullLogin = await SC.tryLogin(UserClient);
            if (SuccessfullLogin)
            {
                var MW = new MainWindow();

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
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(delegate
                {
                    frame.Continue = false;
                    return null;
                }), null);
            Dispatcher.PushFrame(frame);
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            var registration = new Registration();
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
            var MW = new MainWindow();
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