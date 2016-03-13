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

namespace SWN
{
    public partial class MainWindow : Window
    {
        public string UserName = SettingHandler.GetUserName();
        public static MainWindow CurrentInstance;
        Dictionary<ListBoxItem, Client> OnlineClients = new Dictionary<ListBoxItem, Client>();
        private delegate void FaultedInvoker();
        public SWNServiceClient proxy = null;
        public Client receiver = null;
        public Client localclient = null;
        public Character myCharacter;


        public MainWindow()
        {
            InitializeComponent();
            CurrentInstance = this;
            //TODOLOW: Reimplement
            //LoadStackPanelContent();
            //TODOHIGH: ServerCrash catch still doesnt work
        }

        void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }

        void InnerDuplexChannel_Opened(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }

        void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            if (!this.Dispatcher.CheckAccess())
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new FaultedInvoker(HandleProxy));
                return;
            }
            HandleProxy();
        }

        public void HandleProxy()
        {
            if (proxy != null)
            {
                switch (this.proxy.State)
                {
                    case CommunicationState.Closed:
                        proxy = null;
                        lbUserOnline.Items.Clear();
                        MessageBox.Show("Connection to Server Closed");
                        break;
                    case CommunicationState.Closing:
                        break;
                    case CommunicationState.Created:
                        break;
                    case CommunicationState.Faulted:
                        proxy.Abort();
                        proxy = null;
                        lbUserOnline.ItemsSource = null;
                        lbUserOnline.Items.Clear();
                        MessageBox.Show("Connection to Server Lost");
                        this.Close();
                        break;
                    case CommunicationState.Opened:
                        MessageBox.Show("Connected");
                        break;
                    case CommunicationState.Opening:
                        break;
                    default:
                        break;
                }
            }
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
            ServerConnection.SWNClient.Disconnect(localclient);
            System.Threading.Thread.Sleep(500);
            ServerConnection.CurrentConnection.CloseOrAbortServiceChannel(ServerConnection.SWNClient);
            Application.Current.Shutdown();
        }

        private void MenuOpenCV_Click(object sender, RoutedEventArgs e)
        {
            CharacterViewer CV = new CharacterViewer();
            CV.ShowDialog();
        }

        public void LoadStackPanelContent()
        {
            //TODOLOW: Reimplement

            //sP1.Children.Clear();
            ////var context = new Utility.Db1Entities();
            ////var query = from c in context.Advantages select c;
            //ServerConnection sc = new ServerConnection();
            //var advlist = sc.GrabAdvantageListFromServer();

            //foreach (SWNAdmin.Utility.Advantages adv in advlist)
            //{
            //    Expander ex = new Expander();
            //    AdvantageControl AC = new AdvantageControl();
            //    AC.InitControl(adv);
            //    Viewbox vb1 = new Viewbox();
            //    sP1.Children.Add(ex);
            //    ex.Content = vb1;
            //    ex.Header = adv.Name;
            //    vb1.Child = AC;
            //    vb1.Height = AC.Height;
            //}
        }

        private void cbCodeTest_Click(object sender, RoutedEventArgs e)
        {
            //TestButton

            //ServerConnection sc = new ServerConnection();
            //sc.GrabAdvantageListFromServer();
        }

        private void btsend_Click(object sender, RoutedEventArgs e)
        {
            if (CheckConnection())
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
                    m.Sender = UserName;
                    proxy.SendMessage(m);
                    tbChatInput.Text = "";
                }
            }
        }

        public void UpdateChatWindow(string message, string username)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                tbChatPane.Focus();
                tbChatPane.CaretIndex = tbChatPane.Text.Length;
                tbChatPane.ScrollToEnd();
                this.tbChatPane.Text += username + ": " + message + "\r\n";
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

        private void tbChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (CheckConnection())
            {
                if (e.Key == Key.Enter)
                {
                    btsend_Click(this, null);
                }
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (proxy != null)
            {
                if (proxy.State == CommunicationState.Opened)
                {
                    proxy.Disconnect(this.localclient);
                    //dont set proxy.Close(); because isTerminating = true on Disconnect()
                    //and this by default will call HandleProxy() to take care of this.
                }
                else
                {
                    HandleProxy();
                }
            }
        }

        public bool CheckConnection()
        {
            bool AllGood = false;
            if (proxy != null)
            {
                if (proxy.State == CommunicationState.Faulted || proxy.State == CommunicationState.Closed || proxy.State == CommunicationState.Closing || proxy.InnerDuplexChannel.State == CommunicationState.Faulted)
                {
                    HandleProxy();
                    return AllGood;
                }
                else
                {
                    AllGood = true;
                }
            }
            return AllGood;
        }

        private void submenuOptions_Click(object sender, RoutedEventArgs e)
        {
            Forms.Options o = new Forms.Options();
            o.ShowDialog();
        }

        public void UserGetsKicked()
        {
            if (proxy != null)
            {
                if (proxy.State == CommunicationState.Opened)
                {
                    proxy.Disconnect(this.localclient);
                    //dont set proxy.Close(); because isTerminating = true on Disconnect()
                    //and this by default will call HandleProxy() to take care of this.
                }
                else
                {
                    HandleProxy();
                }
            }
        }

        private void menuCreateNewCharacter_Click(object sender, RoutedEventArgs e)
        {
            myCharacter = ServerConnection.SWNClient.GetBlankCharacter(localclient);
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
