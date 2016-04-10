using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SWNAdmin.Classes;
using SWNAdmin.Forms;
using SWNAdmin.Forms.DatabaseManager;
using SWNAdmin.Forms.EncyclopediaManager;
using SWNAdmin.Forms.FactionManager;
using SWNAdmin.Networking;

namespace SWNAdmin
{
    public partial class MainWindow
    {
        public static MainWindow CurrentInstance;
        public static Server ServiceServer;
        private Storyboard _myStoryboard;
        public bool StopFlag = false;

        public MainWindow()
        {
            CurrentInstance = this;
            InitializeComponent();
            BtServerStart.IsEnabled = true;
            BtServerStop.IsEnabled = false;
            // SWNAdmin.Utility.XmlSkillImporter.Import();
        }

        public void UpdateUserOnline( string user, bool delete )
        {
            if (!delete)
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    var userItem = new ListBoxItem();
                    var cm = new ContextMenu();
                    var mi = new MenuItem();
                    mi.Click += MenuItemClick;
                    mi.Header = "Kick";
                    mi.Background = Brushes.Black;
                    cm.Items.Add(mi);
                    userItem.ContextMenu = cm;
                    userItem.Content = user;
                    LbUserOnline.Items.Add(userItem);
                });
            }
            if (delete)
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    var delitem = new ListBoxItem();
                    foreach (var item in LbUserOnline.Items.Cast<ListBoxItem>().Where(item => item.Content.ToString() == user))
                    {
                        delitem = item;
                    }
                    LbUserOnline.Items.Remove(delitem);
                });
            }
        }

        //Test
        public void MenuItemClick( object sender, RoutedEventArgs e )
        {
            var selectedItem = LbUserOnline.SelectedItem as ListBoxItem;
            if (selectedItem == null)
            {
                return;
            }
            var kickclient = new Client
            {
                UserName = selectedItem.Content.ToString()
            };
            SwnService.CurrentService.KickSelectedUser(kickclient);
        }

        public void UpdateServerStatus( bool online )
        {
            if (online)
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    LServerStatus.Content = "Running";
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Running");
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Listening on Port: " + Server.CurrentServiceHost.Description.Endpoints[0].ListenUri.Port);
                    LServerStatus.Foreground = Brushes.LimeGreen;

                    var myDoubleAnimation = new DoubleAnimation();
                    RegisterName(LServerStatus.Name, LServerStatus);
                    myDoubleAnimation.From = 1.0;
                    myDoubleAnimation.To = 0.0;
                    myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    myDoubleAnimation.AutoReverse = true;
                    myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                    _myStoryboard = new Storyboard();
                    _myStoryboard.Children.Add(myDoubleAnimation);
                    Storyboard.SetTargetName(myDoubleAnimation, LServerStatus.Name);
                    Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(OpacityProperty));
                    _myStoryboard.Begin(this);
                });
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke((Action) delegate
                {
                    LServerStatus.Content = "Shutdown";
                    LServerStatus.Foreground = Brushes.Red;
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server successfully Shutdown");
                    SwitchServerState();
                    LbUserOnline.Items.Clear();
                    TbChatPane.Clear();
                    var rb = new RepeatBehavior(0);
                    _myStoryboard.RepeatBehavior = rb;
                    _myStoryboard.Begin(this);
                });
            }
        }

        public void UpdateChatWindow( string message, string userName )
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                TbChatPane.Focus();
                TbChatPane.CaretIndex = TbChatPane.Text.Length;
                TbChatPane.ScrollToEnd();
                TbChatPane.Text += userName + ": " + message + "\r\n";
                TbChatInput.Focus();
            });
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

        public void btServerStart_Click( object sender, RoutedEventArgs e )
        {
            SwitchServerState();
            ClearConsole();
            UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Booting Up");
            ProcessUiTasks();
            ServiceServer = new Server();
            ProcessUiTasks();
        }

        //TODOLOW Shutdown message not Showing before Thread Block
        public void btServerStop_Click( object sender, RoutedEventArgs e )
        {
            UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Shutting Down");
            SwnService.CurrentService.ServerIsInShutdownMode();
            ProcessUiTasks();
            ServiceServer.StopService();
            UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Server Stopped");
            ProcessUiTasks();
        }

        private void Window_Closing( object sender, CancelEventArgs e )
        {
            //if (Server.CurrentServiceHost != null && Server.CurrentServiceHost.State == CommunicationState.Opened)
            //{
            //    btServerStop_Click(this, null);
            //}
        }

        public void UpdateImageWindow( Uri uri )
        {
            //Cleanup Check what is needed here!
            Application.Current.Dispatcher.BeginInvoke(new Action(() => ImgTest.Source = new BitmapImage(uri)));
        }

        private void submenuTimeManager_Click( object sender, RoutedEventArgs e )
        {
            var tm = new TimeManager();
            tm.Show();
        }

        private void subMenuFactionManager_Click( object sender, RoutedEventArgs e )
        {
            var fm = new FactionManager();
            fm.Show();
        }

        private void subMenuEncyclopediaManger_Click( object sender, RoutedEventArgs e )
        {
            var enc = new MainEncyclopedia();
            enc.Show();
        }

        private void ctxImageButton_Click( object sender, RoutedEventArgs e )
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => ImgTest.Source = null));
        }

        #region Hide

        public void SwitchServerState()
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                if (BtServerStart.IsEnabled)
                {
                    BtServerStart.IsEnabled = false;
                    SubmenuTransferImage.IsEnabled = true;
                    SubmenuTransferFile.IsEnabled = true;
                }
                else
                {
                    BtServerStart.IsEnabled = true;
                    SubmenuTransferImage.IsEnabled = false;
                    SubmenuTransferFile.IsEnabled = false;
                }
                BtServerStop.IsEnabled = !BtServerStop.IsEnabled;
            });
        }

        public bool UpdateConsole( string msg )
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate
            {
                TbConsole.Text += msg + "\r\n";
                TbConsole.ScrollToEnd();
            });
            return true;
        }

        public void ClearConsole()
        {
            Application.Current.Dispatcher.BeginInvoke((Action) delegate { TbConsole.Text = ""; });
        }

        public void btGenerate_Click( object sender, RoutedEventArgs e )
        {
            var sg = new SystemGeneration();
            sg.ShowDialog();
        }

        private void MenuDatabaseManager_Click( object sender, RoutedEventArgs e )
        {
            var dbm = new DataBaseManager();
            dbm.ShowDialog();
        }

        private void btSend_Click( object sender, RoutedEventArgs e )
        {
            if (string.IsNullOrEmpty(TbChatInput.Text))
            {
            }
            else
            {
                SwnService.CurrentService.ServerSendMessage(TbChatInput.Text, "Server");
                TbChatInput.Text = "";
            }
        }

        private void tbChatInput_KeyDown( object sender, KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                btSend_Click(this, null);
            }
        }

        private void submenuSystemSelector_Click( object sender, RoutedEventArgs e )
        {
            var ss = new SystemSelector();
            ss.Show();
        }

        private void subMenuSystemGeneration_Click( object sender, RoutedEventArgs e )
        {
            var sg = new SystemGeneration();
            sg.ShowDialog();
        }

        private void submenuTransferImage_Click( object sender, RoutedEventArgs e )
        {
            SwnService.CurrentService.SendImage();
        }

        private void submenuTransferFile_Click( object sender, RoutedEventArgs e )
        {
            SwnService.CurrentService.SendFile();
        }

        #endregion
    }
}