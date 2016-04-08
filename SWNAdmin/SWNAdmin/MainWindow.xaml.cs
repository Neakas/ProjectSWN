using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SWNAdmin.Classes;
using SWNAdmin.Forms;
using SWNAdmin.Forms.EncyclopediaManager;
using SWNAdmin.Networking;
using DataBaseManager = SWNAdmin.Forms.DatabaseManager.DataBaseManager;

namespace SWNAdmin
{
    public partial class MainWindow : Window
    {
        private static AutoResetEvent stopFlag = new AutoResetEvent(false);
        public static MainWindow CurrentInstance;
        public static Server ServiceServer;
        private Storyboard myStoryboard;
        public bool StopFlag = false;
        


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
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    var UserItem = new ListBoxItem();
                    var cm = new ContextMenu();
                    var mi = new MenuItem();
                    mi.Click += MenuItemClick;
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
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    var delitem = new ListBoxItem();
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
            var selectedItem = lbUserOnline.SelectedItem as ListBoxItem;
            var kickclient = new Client();
            kickclient.UserName = selectedItem.Content.ToString();
            SWNService.CurrentService.KickSelectedUser(kickclient);
        }


        public void UpdateServerStatus(bool Online)
        {
            if (Online)
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    lServerStatus.Content = "Running";
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Running");
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Listening on Port: " +
                                  Server.CurrentServiceHost.Description.Endpoints[0].ListenUri.Port);
                    lServerStatus.Foreground = Brushes.LimeGreen;

                    var myDoubleAnimation = new DoubleAnimation();
                    RegisterName(lServerStatus.Name, lServerStatus);
                    myDoubleAnimation.From = 1.0;
                    myDoubleAnimation.To = 0.0;
                    myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    myDoubleAnimation.AutoReverse = true;
                    myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(myDoubleAnimation);
                    Storyboard.SetTargetName(myDoubleAnimation, lServerStatus.Name);
                    Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(OpacityProperty));
                    myStoryboard.Begin(this);
                });
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    lServerStatus.Content = "Shutdown";
                    lServerStatus.Foreground = Brushes.Red;
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server successfully Shutdown");
                    SwitchServerState();
                    lbUserOnline.Items.Clear();
                    tbChatPane.Clear();
                    var rb = new RepeatBehavior(0);
                    myStoryboard.RepeatBehavior = rb;
                    myStoryboard.Begin(this);
                });
            }
        }

        private void ServerLoaded(object sender, RoutedEventArgs e)
        {
            myStoryboard.Begin(this);
        }

        public void UpdateChatWindow(string Message, string UserName)
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
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
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(delegate
                {
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //if (Server.CurrentServiceHost != null && Server.CurrentServiceHost.State == CommunicationState.Opened)
            //{
            //    btServerStop_Click(this, null);
            //}
        }

        public void UpdateImageWindow(Uri uri)
        {
            //Cleanup Check what is needed here!
            Application.Current.Dispatcher.BeginInvoke(new Action(() => imgTest.Source = new BitmapImage(uri)));
        }

        private void submenuTimeManager_Click(object sender, RoutedEventArgs e)
        {
            var tm = new TimeManager();
            tm.Show();
        }

        private void subMenuFactionManager_Click(object sender, RoutedEventArgs e)
        {
            var fm = new Forms.FactionManager.FactionManager();
            fm.Show();
        }

        private void subMenuEncyclopediaManger_Click(object sender, RoutedEventArgs e)
        {
            var enc = new MainEncyclopedia();
            enc.Show();
        }

        private void ctxImageButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => imgTest.Source = null));
        }

        #region Hide

        public void SwitchServerState()
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate
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
                btServerStop.IsEnabled = !btServerStop.IsEnabled;
            });
        }

        public bool UpdateConsole(string Msg)
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                tbConsole.Text += Msg + "\r\n";
                tbConsole.ScrollToEnd();
            });
            return true;
        }

        public void ClearConsole()
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate { tbConsole.Text = ""; });
        }

        public void btGenerate_Click(object sender, RoutedEventArgs e)
        {
            var SG = new Forms.SystemGeneration();
            SG.ShowDialog();
        }

        private void MenuDatabaseManager_Click(object sender, RoutedEventArgs e)
        {
            var DBM = new DataBaseManager();
            DBM.ShowDialog();
        }

        private void btSend_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbChatInput.Text))
            {
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
            var SS = new SystemSelector();
            SS.Show();
        }

        private void subMenuSystemGeneration_Click(object sender, RoutedEventArgs e)
        {
            var SG = new Forms.SystemGeneration();
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
    }
}