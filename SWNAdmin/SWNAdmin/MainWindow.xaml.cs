﻿using System;
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

namespace SWNAdmin
{
    public partial class MainWindow : Window
    {
        private static AutoResetEvent stopFlag = new AutoResetEvent(false);
        public bool StopFlag = false;
        public static MainWindow CurrentInstance;
        public static Server ServiceServer;

        public MainWindow()
        {
            CurrentInstance = this;
            InitializeComponent();
            TimeHandler.ResetDateTime();
            //UpdateDateTimeDisplay();
            btServerStart.IsEnabled = true;
            btServerStop.IsEnabled = false;
            //Cleanup Remove this if not needed anymore
           // SqlManager.QueryAdvantage();
        }

        public void UpdateUserOnline(string User, bool Delete)
        {
            if (!Delete)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    lbUserOnline.Items.Add(User);
                });
            }
            if (Delete)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    lbUserOnline.Items.Remove(User);
                });
            }
        }

        public void UpdateServerStatus(bool Online)
        {
            if (Online)
            {
                Application.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    lServerStatus.Content = "Running";
                    UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Running");
                    lServerStatus.Foreground = Brushes.Green;
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
                });
            }
           
        }

        public List<string> GetUsersOnline()
        {
            List<string> OnlineUsers = this.lbUserOnline.Items.OfType<string>().ToList();
            return OnlineUsers;
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
    }
}