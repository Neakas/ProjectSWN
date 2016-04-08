using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using SWN.Service_References.SWNServiceReference;
using SWN.UserControls;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Message = SWN.Service_References.SWNServiceReference.Message;
using MessageBox = System.Windows.MessageBox;

namespace SWN
{
    public partial class MainWindow : Window
    {
        public Character myCharacter;
        private Storyboard myStoryboard;
        public Dictionary<Notification, string> NotificationDict = new Dictionary<Notification, string>();


        public MainWindow()
        {
            InitializeComponent();
            CurrentInstance = this;
            lbUserOnline.ItemsSource = ServerConnection.CurrentInstance.GrabLoggedInUsers();
            App.mplayer.Stop();
        }

        public Client LocalCient { get; set; }

        public static MainWindow CurrentInstance { get; set; }

        private void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new FaultedInvoker(ServerConnection.CurrentInstance.HandleProxy));
                return;
            }
            ServerConnection.CurrentInstance.HandleProxy();
        }

        private void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new FaultedInvoker(ServerConnection.CurrentInstance.HandleProxy));
                return;
            }
            ServerConnection.CurrentInstance.HandleProxy();
        }

        private void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    new FaultedInvoker(ServerConnection.CurrentInstance.HandleProxy));
                return;
            }
            ServerConnection.CurrentInstance.HandleProxy();
        }


        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDateTimeDisplay();
        }

        public void UpdateDateTimeDisplay()
        {
            // TODO: Hier Update Event vom Server
        }

        private void btOpenMap_Click(object sender, RoutedEventArgs e)
        {
            var Map1 = new Forms.Map();
            Map1.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ServerConnection.LocalServiceClient.Disconnect(LocalCient);
            Thread.Sleep(500);
            ServerConnection.CurrentInstance.CloseOrAbortServiceChannel(ServerConnection.LocalServiceClient);
            Application.Current.Shutdown();
        }

        private void MenuOpenCV_Click(object sender, RoutedEventArgs e)
        {
            var CV = new Forms.CharacterViewer();
            CV.ShowDialog();
        }

        private void btsend_Click(object sender, RoutedEventArgs e)
        {
            if (ServerConnection.CurrentInstance.CheckConnection())
            {
                if (string.IsNullOrEmpty(tbChatInput.Text))
                {
                }
                else
                {
                    //ServerConnection sc = new ServerConnection();
                    //sc.SendMessageToServer(tbChatInput.Text, UserName);
                    var m = new Message();
                    m.Content = tbChatInput.Text;
                    m.Sender = LocalCient.UserName;
                    ServerConnection.LocalServiceClient.SendMessage(m);
                    tbChatInput.Text = "";
                }
            }
        }

        public void UpdateChatWindow(string message)
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                tbChatPane.Focus();
                tbChatPane.CaretIndex = tbChatPane.Text.Length;
                tbChatPane.ScrollToEnd();
                tbChatPane.Text += message + "\r\n";
                tbChatInput.Focus();
            });
        }

        public void UpdateFileReceive()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => lblFileTransfer.Content = "Receiving File!"));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => lblFileTransfer.Foreground = Brushes.Green));
        }

        public void UpdateFileReceive(bool reset)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => lblFileTransfer.Content = "No Filetransfer"));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => lblFileTransfer.Foreground = Brushes.White));
        }

        public void UpdateChatUserJoined(string username)
        {
            Application.Current.Dispatcher.BeginInvoke(
                new Action(() => tbChatPane.Text += "The User: " + username + " has Joined!" + "\r\n"));
        }

        public void UpdateImageWindow(Uri uri)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => imgTest.Source = new BitmapImage(uri)));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => biBusy.IsBusy = false));
        }

        public void SetImageGridBusy()
        {
            biBusy.IsBusy = true;
            //Force UI Redraw
            Dispatcher.Invoke(() => { }, DispatcherPriority.ContextIdle);
            //Application.Current.Dispatcher.BeginInvoke(new Action(() => biBusy.IsBusy = true));
        }

        public void CreateNotification(object notificationobject)
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                var Notification = new Notification(notificationobject);
                Notification.NotificationText = "Received New Image";
                NotificationDict.Add(Notification, "Notification" + (NotificationDict.Count + 1));
                ReloadNotificationPanel();
            });
        }

        private void ReloadNotificationPanel()
        {
            //spNotificationPanel.Children.Clear();
            foreach (var notificationitem in NotificationDict.Keys)
            {
                if (!spNotificationPanel.Children.Contains(notificationitem))
                {
                    spNotificationPanel.Children.Add(notificationitem);
                    // Animation Code für Notifications
                    //DoubleAnimation myAnimation = new DoubleAnimation();
                    var myAnimation = new ThicknessAnimation();
                    RegisterName(NotificationDict[notificationitem], notificationitem);
                    myAnimation.From = new Thickness(66, 0, 0, 0);
                    myAnimation.To = new Thickness(0, 0, 0, 0);
                    myAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(myAnimation);
                    Storyboard.SetTargetName(myAnimation, NotificationDict[notificationitem]);
                    Storyboard.SetTargetProperty(myAnimation, new PropertyPath(MarginProperty));
                    myStoryboard.Begin(this);
                }
            }
        }

        public void UpdateUserOnline(List<string> Userlist)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => lbUserOnline.ItemsSource = null));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => lbUserOnline.ItemsSource = Userlist));
        }

        private void tbChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (ServerConnection.CurrentInstance.CheckConnection())
            {
                if (e.Key == Key.Enter)
                {
                    btsend_Click(this, null);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (ServerConnection.LocalServiceClient != null)
            {
                if (ServerConnection.LocalServiceClient.State == CommunicationState.Opened)
                {
                    ServerConnection.LocalServiceClient.Disconnect(LocalCient);
                    //dont set proxy.Close(); because isTerminating = true on Disconnect()
                    //and this by default will call HandleProxy() to take care of this.
                }
                else
                {
                    ServerConnection.CurrentInstance.HandleProxy();
                }
            }
        }


        private void submenuOptions_Click(object sender, RoutedEventArgs e)
        {
            var o = new Options();
            o.ShowDialog();
        }

        public void UserGetsKicked()
        {
            if (ServerConnection.LocalServiceClient != null)
            {
                if (ServerConnection.LocalServiceClient.State == CommunicationState.Opened)
                {
                    ServerConnection.LocalServiceClient.Disconnect(LocalCient);
                    //dont set proxy.Close(); because isTerminating = true on Disconnect()
                    //and this by default will call HandleProxy() to take care of this.
                }
                else
                {
                    ServerConnection.CurrentInstance.HandleProxy();
                }
            }
        }

        private void menuCreateNewCharacter_Click(object sender, RoutedEventArgs e)
        {
            myCharacter = ServerConnection.LocalServiceClient.GetBlankCharacter(LocalCient);
            var cnc = new Forms.CreateNewCharacter(myCharacter);
            cnc.ShowDialog();
        }

        private void menuLoadCharacter_Click(object sender, RoutedEventArgs e)
        {
            var lc = new Forms.LoadCharacter();
            lc.ShowDialog();
        }

        private void submenuSectorViewer_Click(object sender, RoutedEventArgs e)
        {
            if (PrepareSystem())
            {
                try
                {
                    var p1 =
                        Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                      @"\AstroSynthesis3\RunAsDate.exe");
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
        }

        private bool PrepareSystem()
        {
            try
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                          @"\Nbos\AstroSynthesis3\");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Create Activation Folder");
                return false;
            }
            try
            {
                var sitePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                               @"\Nbos\AstroSynthesis3\site.dat";
                File.WriteAllText(sitePath,
                    "x8bPz9HPyYPLzcvNx9HKzcvOycbPyc3Kg8jMzMnRycmDy83LzMnRyMrHysfIxsfJzoPLx83J0czLg8vNy8vN0crNy87Jxs/Jzco");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Create Activation File");
                return false;
            }

            try
            {
                RegistryKey key;
                key =
                    Registry.CurrentUser.OpenSubKey("Software", false)
                        .OpenSubKey("NBOS Software", false)
                        .OpenSubKey("AstroSynthesis", false)
                        .OpenSubKey("3.0", true);
                key.SetValue("site-key",
                    "zcjHy9HKzIPLzcvNx9HKzcvOycbPyc3Kg8nKyM7RyciDy83LzMnRyMrHysfIxsfJzoPKy8fI0c/Ng8vNy8vN0crNy87Jxs/Jzco");
                key.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Set Registry Key");
                return false;
            }
            return true;
        }

        private delegate void FaultedInvoker();
    }
}