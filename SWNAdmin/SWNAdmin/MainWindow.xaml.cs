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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ServiceModel;
using UniverseGeneration;
using SWNAdmin.Forms;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace SWNAdmin
{
    public partial class MainWindow : Window
    {
        private static AutoResetEvent stopFlag = new AutoResetEvent(false);
        public bool StopFlag = false;
        public static MainWindow CurrentInstance;
        public static Server ServiceServer;
        private Storyboard myStoryboard;


        public MainWindow()
        {
            CurrentInstance = this;
            InitializeComponent();
            btServerStart.IsEnabled = true;
            btServerStop.IsEnabled = false;
            // SWNAdmin.Utility.XmlSkillImporter.Import();
        }

        public void UpdateUserOnline(string User, bool Delete)
        {
            if (!Delete)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    ListBoxItem UserItem = new ListBoxItem();
                    ContextMenu cm = new ContextMenu();
                    MenuItem mi = new MenuItem();
                    mi.Click += new RoutedEventHandler(MenuItemClick);                    
                    mi.Header = "Kick";
                    mi.Background = Brushes.Black;
                    cm.Items.Add(mi);
                    UserItem.ContextMenu = cm;
                    UserItem.Content = User;
                    lbUserOnline.Items.Add(UserItem);
                });
            }
            if (Delete)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    ListBoxItem delitem = new ListBoxItem();
                    foreach (ListBoxItem item in lbUserOnline.Items)
                    {
                        if (item.Content.ToString() == User)
                        {
                            delitem = item;
                        }
                    }
                    lbUserOnline.Items.Remove(delitem);
                });
            }
        }

        //Test
        public void MenuItemClick(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = lbUserOnline.SelectedItem  as ListBoxItem;
            Client kickclient = new Client();
            kickclient.UserName = selectedItem.Content.ToString();
            SWNService.CurrentService.KickSelectedUser(kickclient);
        }


        public void UpdateServerStatus(bool Online)
        {
            if (Online)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    lServerStatus.Content = "Running";
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Running");
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Listening on Port: " + Server.CurrentServiceHost.Description.Endpoints[0].ListenUri.Port.ToString());
                    lServerStatus.Foreground = Brushes.LimeGreen;

                    DoubleAnimation myDoubleAnimation = new DoubleAnimation();
                    this.RegisterName(lServerStatus.Name, lServerStatus);
                    myDoubleAnimation.From = 1.0;
                    myDoubleAnimation.To = 0.0;
                    myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    myDoubleAnimation.AutoReverse = true;
                    myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(myDoubleAnimation);
                    Storyboard.SetTargetName(myDoubleAnimation, lServerStatus.Name);
                    Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Rectangle.OpacityProperty));
                    myStoryboard.Begin(this);
                });
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    lServerStatus.Content = "Shutdown";
                    lServerStatus.Foreground = Brushes.Red;
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server successfully Shutdown");
                    SwitchServerState();
                    lbUserOnline.Items.Clear();
                    tbChatPane.Clear();
                    RepeatBehavior rb = new RepeatBehavior(0);
                    myStoryboard.RepeatBehavior = rb;
                    myStoryboard.Begin(this);
                });
            }
           
        }

        private void ServerLoaded(object sender, RoutedEventArgs e)
        {
            myStoryboard.Begin(this);
        }

        public void UpdateChatWindow(string Message, String UserName)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                tbChatPane.Focus();
                tbChatPane.CaretIndex = tbChatPane.Text.Length;
                tbChatPane.ScrollToEnd();
                tbChatPane.Text += UserName + ": " + Message + "\r\n";
                tbChatInput.Focus();
            });
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

        public void btServerStart_Click(object sender, RoutedEventArgs e)
        {
            SwitchServerState();
            ClearConsole();
            UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Booting Up");
            ProcessUITasks();
            ServiceServer = new Server();
            ProcessUITasks();
        }

        //TODOLOW Shutdown Message not Showing before Thread Block
        public void btServerStop_Click(object sender, RoutedEventArgs e)
        {
            UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Shutting Down");
            SWNService.CurrentService.ServerIsInShutdownMode();
            ProcessUITasks();
            ServiceServer.StopService();
            UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Server Stopped");
            ProcessUITasks();
        }

        #region Hide

        public void SwitchServerState()
        {
            if (btServerStart.IsEnabled)
            {
                btServerStart.IsEnabled = false;
                submenuTransferImage.IsEnabled = true;
                submenuTransferFile.IsEnabled = true;
            }
            else
            {
                btServerStart.IsEnabled = true;
                submenuTransferImage.IsEnabled = false;
                submenuTransferFile.IsEnabled = false;
            }
            if (btServerStop.IsEnabled)
                btServerStop.IsEnabled = false;
            else
                btServerStop.IsEnabled = true;
        }

        public bool UpdateConsole(string Msg)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                this.tbConsole.Text += Msg + "\r\n";
                this.tbConsole.ScrollToEnd();
            });
            return true;
        }

        public void ClearConsole()
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                tbConsole.Text = "";
            });
        }

        public void btGenerate_Click(object sender, RoutedEventArgs e)
        {
            SystemGeneration SG = new SystemGeneration();
            SG.ShowDialog();
        }

        private void MenuDatabaseManager_Click(object sender, RoutedEventArgs e)
        {
            DataBaseManager DBM = new DataBaseManager();
            DBM.ShowDialog();
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            if (tbChatInput.Text == null || tbChatInput.Text == "")
            {
                return;
            }
            else
            {               
                SWNService.CurrentService.ServerSendMessage(tbChatInput.Text, "Server");
                tbChatInput.Text = "";
            }
        }

        private void tbChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btSend_Click(this, null);
            }
        }

        private void submenuSystemSelector_Click(object sender, RoutedEventArgs e)
        {
            SystemSelector SS = new SystemSelector();
            SS.Show();
        }

        private void subMenuSystemGeneration_Click(object sender, RoutedEventArgs e)
        {
            SystemGeneration SG = new SystemGeneration();
            SG.ShowDialog();
        }

        private void submenuTransferImage_Click(object sender, RoutedEventArgs e)
        {
            SWNService.CurrentService.SendImage();
        }

        private void submenuTransferFile_Click(object sender, RoutedEventArgs e)
        {
            SWNService.CurrentService.SendFile();
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (Server.CurrentServiceHost != null && Server.CurrentServiceHost.State == CommunicationState.Opened)
            //{
            //    btServerStop_Click(this, null);
            //}
        }

        public void UpdateImageWindow(Uri uri)
        {
            //Cleanup Check what is needed here!
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.imgTest.Source = new BitmapImage(uri)));
            //Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Content = "No Filetransfer"));
            //Application.Current.Dispatcher.BeginInvoke(new Action(() => this.lblFileTransfer.Foreground = Brushes.White));
        }

        private void submenuTimeManager_Click(object sender, RoutedEventArgs e)
        {
            TimeManager tm = new TimeManager();
            tm.Show();
        }

        private void subMenuFactionManager_Click(object sender, RoutedEventArgs e)
        {
            FactionManager fm = new FactionManager();
            fm.Show();
        }

        private void subMenuEncyclopediaManger_Click(object sender, RoutedEventArgs e)
        {
            UI.MainEncyclopedia enc = new UI.MainEncyclopedia();
            enc.Show();
        }
    }
}