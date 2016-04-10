using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using SWN.Forms;
using SWN.Networking;
using SWN.SWNServiceReference;
using SWN.UserControls;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Message = SWN.SWNServiceReference.Message;
using MessageBox = System.Windows.MessageBox;

namespace SWN
{
    public partial class MainWindow
    {
        public delegate void FaultedInvoker();

        private Storyboard _myStoryboard;
        public Character MyCharacter;
        public Dictionary<Notification, string> NotificationDict = new Dictionary<Notification, string>();

        public MainWindow()
        {
            InitializeComponent();
            CurrentInstance = this;
            LbUserOnline.ItemsSource = ServerConnection.CurrentInstance.GrabLoggedInUsers();
            App.Mplayer.Stop();
        }

        public Client LocalCient { get; set; }

        public static MainWindow CurrentInstance { get; set; }

        public void Window_Loaded( object sender, RoutedEventArgs e )
        {
            UpdateDateTimeDisplay();
        }

        public void UpdateDateTimeDisplay()
        {
            // TODO: Hier Update Event vom Server
        }

        private void Window_Closed( object sender, EventArgs e )
        {
            ServerConnection.LocalServiceClient.Disconnect(LocalCient);
            Thread.Sleep(500);
            ServerConnection.CurrentInstance.CloseOrAbortServiceChannel(ServerConnection.LocalServiceClient);
            Application.Current.Shutdown();
        }

        private void MenuOpenCV_Click( object sender, RoutedEventArgs e )
        {
            var cv = new CharacterViewer();
            cv.ShowDialog();
        }

        private void btsend_Click( object sender, RoutedEventArgs e )
        {
            if (!ServerConnection.CurrentInstance.CheckConnection())
            {
                return;
            }
            if (string.IsNullOrEmpty(TbChatInput.Text))
            {
            }
            else
            {
                var m = new Message
                {
                    Content = TbChatInput.Text,
                    Sender = LocalCient.UserName
                };
                ServerConnection.LocalServiceClient.SendMessage(m);
                TbChatInput.Text = "";
            }
        }

        public void UpdateChatWindow( string message )
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                TbChatPane.Focus();
                TbChatPane.CaretIndex = TbChatPane.Text.Length;
                TbChatPane.ScrollToEnd();
                TbChatPane.Text += message + "\r\n";
                TbChatInput.Focus();
            });
        }

        public void UpdateFileReceive()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => LblFileTransfer.Content = "Receiving File!"));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => LblFileTransfer.Foreground = Brushes.Green));
        }

        public void UpdateFileReceive( bool reset )
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => LblFileTransfer.Content = "No Filetransfer"));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => LblFileTransfer.Foreground = Brushes.White));
        }

        public void UpdateChatUserJoined( string username )
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => TbChatPane.Text += "The User: " + username + " has Joined!" + "\r\n"));
        }

        public void UpdateImageWindow( Uri uri )
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => ImgTest.Source = new BitmapImage(uri)));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => BiBusy.IsBusy = false));
        }

        public void SetImageGridBusy()
        {
            BiBusy.IsBusy = true;
            //Force UI Redraw
            Dispatcher.Invoke(() => { }, DispatcherPriority.ContextIdle);
            //Application.Current.Dispatcher.BeginInvoke(new Action(() => BiBusy.IsBusy = true));
        }

        public void CreateNotification( object notificationobject )
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                var notification = new Notification(notificationobject)
                {
                    NotificationText = "Received New Image"
                };
                NotificationDict.Add(notification, "Notification" + ( NotificationDict.Count + 1 ));
                ReloadNotificationPanel();
            });
        }

        private void ReloadNotificationPanel()
        {
            foreach (var notificationitem in NotificationDict.Keys.Where(notificationitem => !SpNotificationPanel.Children.Contains(notificationitem)))
            {
                SpNotificationPanel.Children.Add(notificationitem);
                var myAnimation = new ThicknessAnimation();
                RegisterName(NotificationDict[notificationitem], notificationitem);
                myAnimation.From = new Thickness(66, 0, 0, 0);
                myAnimation.To = new Thickness(0, 0, 0, 0);
                myAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                _myStoryboard = new Storyboard();
                _myStoryboard.Children.Add(myAnimation);
                Storyboard.SetTargetName(myAnimation, NotificationDict[notificationitem]);
                Storyboard.SetTargetProperty(myAnimation, new PropertyPath(MarginProperty));
                _myStoryboard.Begin(this);
            }
        }

        public void UpdateUserOnline( List<string> userlist )
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => LbUserOnline.ItemsSource = null));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => LbUserOnline.ItemsSource = userlist));
        }

        private void tbChatInput_KeyDown( object sender, KeyEventArgs e )
        {
            if (!ServerConnection.CurrentInstance.CheckConnection())
            {
                return;
            }
            if (e.Key == Key.Enter)
            {
                btsend_Click(this, null);
            }
        }

        protected override void OnClosing( CancelEventArgs e )
        {
            if (ServerConnection.LocalServiceClient == null)
            {
                return;
            }
            if (ServerConnection.LocalServiceClient.State == CommunicationState.Opened)
            {
                ServerConnection.LocalServiceClient.Disconnect(LocalCient);
            }
            else
            {
                ServerConnection.CurrentInstance.HandleProxy();
            }
        }

        private void submenuOptions_Click( object sender, RoutedEventArgs e )
        {
            var o = new Options();
            o.ShowDialog();
        }

        public void UserGetsKicked()
        {
            if (ServerConnection.LocalServiceClient == null)
            {
                return;
            }
            if (ServerConnection.LocalServiceClient.State == CommunicationState.Opened)
            {
                ServerConnection.LocalServiceClient.Disconnect(LocalCient);
            }
            else
            {
                ServerConnection.CurrentInstance.HandleProxy();
            }
        }

        private void menuCreateNewCharacter_Click( object sender, RoutedEventArgs e )
        {
            MyCharacter = ServerConnection.LocalServiceClient.GetBlankCharacter(LocalCient);
            var cnc = new CreateNewCharacter(MyCharacter);
            cnc.ShowDialog();
        }

        private void menuLoadCharacter_Click( object sender, RoutedEventArgs e )
        {
            var lc = new LoadCharacter();
            lc.ShowDialog();
        }

        private void submenuSectorViewer_Click( object sender, RoutedEventArgs e )
        {
            if (!PrepareSystem())
            {
                return;
            }
            try
            {
                Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\AstroSynthesis3\RunAsDate.exe");
                Thread.Sleep(500);
                SendKeys.SendWait("{ENTER}");
                Thread.Sleep(1000);
                SendKeys.SendWait("{ENTER}");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Start System Viewer Process");
            }
        }

        private static bool PrepareSystem()
        {
            try
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Nbos\AstroSynthesis3\");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Create Activation Folder");
                return false;
            }
            try
            {
                var sitePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Nbos\AstroSynthesis3\site.dat";
                File.WriteAllText(sitePath, @"x8bPz9HPyYPLzcvNx9HKzcvOycbPyc3Kg8jMzMnRycmDy83LzMnRyMrHysfIxsfJzoPLx83J0czLg8vNy8vN0crNy87Jxs/Jzco");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Create Activation File");
                return false;
            }

            try
            {
                var openSubKey = Registry.CurrentUser.OpenSubKey("Software", false);
                var registryKey = openSubKey?.OpenSubKey("NBOS Software", false);
                if (registryKey != null)
                {
                    var subKey = registryKey.OpenSubKey("AstroSynthesis", false);
                    var key = subKey?.OpenSubKey("3.0", true);
                    if (key != null)
                    {
                        key.SetValue("site-key", "zcjHy9HKzIPLzcvNx9HKzcvOycbPyc3Kg8nKyM7RyciDy83LzMnRyMrHysfIxsfJzoPKy8fI0c/Ng8vNy8vN0crNy87Jxs/Jzco");
                        key.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Set Registry Key");
                return false;
            }
            return true;
        }
    }
}