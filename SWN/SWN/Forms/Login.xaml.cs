using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using SWN.Controller;
using SWN.Networking;
using SWN.SWNServiceReference;

namespace SWN.Forms

{
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
            CurrentInstance = this;
            TbIpPort.Text = SettingHandler.GetIpPort();
        }

        public static Login CurrentInstance { get; set; }

        public Client UserClient { get; set; }

        private async void btLogin_Click( object sender, RoutedEventArgs e )
        {
            var sc = ServerConnection.CurrentInstance ?? new ServerConnection();

            UserClient = new Client();
            SettingHandler.SetIpPort(TbIpPort.Text);

            if (TextBoxUsername.Text.Length == 0)
            {
                Errormessage.Text = "Please enter a Username.";
                TextBoxUsername.Focus();
                return;
            }
            Errormessage.Text = "";
            BiBusy.IsBusy = true;
            //Force UI Redraw
            Dispatcher.Invoke(() => { }, DispatcherPriority.ContextIdle);

            UserClient.UserName = TextBoxUsername.Text;
            UserClient.EncPassword = new Encryption(PasswordBox1.Password).EncryptStringToBytes(PasswordBox1.Password);
            var successfullLogin = await sc.TryLogin(UserClient);
            if (successfullLogin)
            {
                var mw = new MainWindow
                {
                    LocalCient = UserClient
                };

                SettingHandler.SetIsLoggedIn(true);
                mw.Show();
                Close();
            }
            else
            {
                BiBusy.IsBusy = false;
            }
        }

        public static void ProcessUiTasks()
        {
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate
            {
                frame.Continue = false;
                return null;
            }), null);
            Dispatcher.PushFrame(frame);
        }

        private void buttonRegister_Click( object sender, RoutedEventArgs e )
        {
            var registration = new Registration();
            registration.Show();
            Close();
        }

        private void textBoxUsername_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                btLogin_Click(this, null);
            }
        }

        private void passwordBox1_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                btLogin_Click(this, null);
            }
        }

        private void btOverride_Click( object sender, RoutedEventArgs e )
        {
            var mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void btSound_Click( object sender, RoutedEventArgs e )
        {
            if (App.Musicplaying)
            {
                App.Mplayer.Pause();
                App.Musicplaying = false;
            }
            else
            {
                App.Mplayer.Play();
                App.Musicplaying = true;
            }
        }
    }
}