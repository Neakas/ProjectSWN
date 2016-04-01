using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SWN.SWNServiceReference;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WPF.Themes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace SWN
{
    public partial class MainWindow : Window
    {
        private delegate void FaultedInvoker();
        private Client localclient;
        private Storyboard myStoryboard;
        private Dictionary<UserControls.Notification, string> NotificationDict = new Dictionary<UserControls.Notification, string>();

        public Client LocalCient
        {
            get { return localclient; }
            set { localclient = value; }
        }

        private static MainWindow currentinstance;
        public static MainWindow CurrentInstance
        {
            get { return currentinstance; }
            set { currentinstance = value; }
        }
        public Character myCharacter;


        public MainWindow()
        {
            InitializeComponent();
            CurrentInstance = this;
            lbUserOnline.ItemsSource = ServerConnection.CurrentInstance.GrabLoggedInUsers();
            App.mplayer.Stop();
        }

        void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(ServerConnection.CurrentInstance.HandleProxy));
                return;
            }
            ServerConnection.CurrentInstance.HandleProxy();
        }

        void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(ServerConnection.CurrentInstance.HandleProxy));
                return;
            }
            ServerConnection.CurrentInstance.HandleProxy();
        }

        void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(ServerConnection.CurrentInstance.HandleProxy));
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
            Map Map1 = new Map();
            Map1.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ServerConnection.LocalServiceClient.Disconnect(localclient);
            System.Threading.Thread.Sleep(500);
            ServerConnection.CurrentInstance.CloseOrAbortServiceChannel(ServerConnection.LocalServiceClient);
            Application.Current.Shutdown();
        }

        private void MenuOpenCV_Click(object sender, RoutedEventArgs e)
        {
            CharacterViewer CV = new CharacterViewer();
            CV.ShowDialog();
        }

        private void btsend_Click(object sender, RoutedEventArgs e)
        {
            if (ServerConnection.CurrentInstance.CheckConnection())
            {
                if (tbChatInput.Text == null || tbChatInput.Text == "")
                {
                    return;
                }
                else
                {
                    //ServerConnection sc = new ServerConnection();
                    //sc.SendMessageToServer(tbChatInput.Text, UserName);
                    SWNServiceReference.Message m = new SWNServiceReference.Message();
                    m.Content = tbChatInput.Text;
                    m.Sender = localclient.UserName;
                    ServerConnection.LocalServiceClient.SendMessage(m);
                    tbChatInput.Text = "";
                }
            }
        }

        public void UpdateChatWindow(string message)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                tbChatPane.Focus();
                tbChatPane.CaretIndex = tbChatPane.Text.Length;
                tbChatPane.ScrollToEnd();
                this.tbChatPane.Text += message + "\r\n";
                tbChatInput.Focus();
            });
        }

        public void UpdateFileReceive()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Content = "Receiving File!"));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Foreground = Brushes.Green));
        }

        public void UpdateFileReceive(bool reset)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Content = "No Filetransfer"));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Foreground = Brushes.White));
        }

        public void UpdateChatUserJoined(string username)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.tbChatPane.Text += "The User: " + username + " has Joined!" + "\r\n"));
        }

        public void UpdateImageWindow(Uri uri)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.imgTest.Source = new BitmapImage(uri)));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Content = "No Filetransfer"));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Foreground = Brushes.White));
        }

        public void CreateNotification(object notificationobject)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                UserControls.Notification Notification = new UserControls.Notification(notificationobject);
                Notification.NotificationText = "Received New Image";
                NotificationDict.Add(Notification,"Notification" + (NotificationDict.Count+1));
                ReloadNotificationPanel();
            });
        }

        private void ReloadNotificationPanel()
        {
            //spNotificationPanel.Children.Clear();
            foreach (UserControls.Notification notificationitem in NotificationDict.Keys)
            {
                if (!spNotificationPanel.Children.Contains(notificationitem))
                {
                    spNotificationPanel.Children.Add(notificationitem);
                    // Animation Code für Notifications
                    //DoubleAnimation myAnimation = new DoubleAnimation();
                    ThicknessAnimation myAnimation = new ThicknessAnimation();
                    this.RegisterName(NotificationDict[notificationitem], notificationitem);
                    myAnimation.From = new Thickness(66, 0, 0, 0);
                    myAnimation.To = new Thickness(0, 0, 0, 0);
                    myAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(myAnimation);
                    Storyboard.SetTargetName(myAnimation, NotificationDict[notificationitem]);
                    Storyboard.SetTargetProperty(myAnimation, new PropertyPath(Border.MarginProperty));
                    myStoryboard.Begin(this);
                }
            }
        }

        public void UpdateUserOnline(List<string> Userlist)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lbUserOnline.ItemsSource = null));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lbUserOnline.ItemsSource = Userlist));
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

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (ServerConnection.LocalServiceClient != null)
            {
                if (ServerConnection.LocalServiceClient.State == CommunicationState.Opened)
                {
                    ServerConnection.LocalServiceClient.Disconnect(this.localclient);
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
            Forms.Options o = new Forms.Options();
            o.ShowDialog();
        }

        public void UserGetsKicked()
        {
            if (ServerConnection.LocalServiceClient != null)
            {
                if (ServerConnection.LocalServiceClient.State == CommunicationState.Opened)
                {
                    ServerConnection.LocalServiceClient.Disconnect(this.localclient);
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
            myCharacter = ServerConnection.LocalServiceClient.GetBlankCharacter(localclient);
            CreateNewCharacter cnc = new CreateNewCharacter(myCharacter);
            cnc.ShowDialog();
        }

        private void menuLoadCharacter_Click(object sender, RoutedEventArgs e)
        {
            LoadCharacter lc = new LoadCharacter();
            lc.ShowDialog();
        }

        private void submenuSectorViewer_Click(object sender, RoutedEventArgs e)
        {
            if (PrepareSystem())
            {
                try
                {
                    Process p1 = Process.Start(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\AstroSynthesis3\RunAsDate.exe");
                    Thread.Sleep(500);
                    System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(1000);
                    System.Windows.Forms.SendKeys.SendWait("{ENTER}");
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
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Nbos\AstroSynthesis3\");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Create Activation Folder");
                return false;
            }
            try
            {
                string sitePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Nbos\AstroSynthesis3\site.dat";
                File.WriteAllText(sitePath, "x8bPz9HPyYPLzcvNx9HKzcvOycbPyc3Kg8jMzMnRycmDy83LzMnRyMrHysfIxsfJzoPLx83J0czLg8vNy8vN0crNy87Jxs/Jzco");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not Create Activation File");
                return false;
            }

            try
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", false).OpenSubKey("NBOS Software", false).OpenSubKey("AstroSynthesis", false).OpenSubKey("3.0", true);
                key.SetValue("site-key", "zcjHy9HKzIPLzcvNx9HKzcvOycbPyc3Kg8nKyM7RyciDy83LzMnRyMrHysfIxsfJzoPKy8fI0c/Ng8vNy8vN0crNy87Jxs/Jzco");
                key.Close();
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
