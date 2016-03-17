using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SWNAdmin
{
    public class Server
    {
        public AutoResetEvent threadStopFlag = new AutoResetEvent(false);
        Thread ServerThread;
        //ServiceHost SWNServiceHost;
        public static ServiceHost CurrentServiceHost;

        public Server()
        {
            CurrentServiceHost = null;
            ServerThread = new Thread(StartService);
            ServerThread.IsBackground = true;
            ServerThread.Start();
            Thread.Sleep(2000);
        }

        private void StartService()
        {
            CurrentServiceHost = new ServiceHost(typeof(SWNService));
            try
            {
                CurrentServiceHost.Open();
            }
            catch (Exception e)
            {
                MainWindow.CurrentInstance.UpdateConsole(e.Message.ToString());
                MainWindow.CurrentInstance.SwitchServerState();
            }
            finally
            {
                if (CurrentServiceHost.State == CommunicationState.Opened)
                {
                    MainWindow.CurrentInstance.UpdateServerStatus(true);
                }
            }
            threadStopFlag.WaitOne();
            ServerThread.Join();
        }

        public void StopService()
        {
            if (CurrentServiceHost != null)
            {
                try
                {
                    Thread.Sleep(1000);
                    CurrentServiceHost.Close();
                }
                catch (Exception e)
                {
                    MainWindow.CurrentInstance.UpdateConsole(e.Message.ToString());
                }
                finally
                {
                    if (CurrentServiceHost.State == CommunicationState.Closed)
                    {
                        MainWindow.CurrentInstance.UpdateServerStatus(false);
                    }
                }
            }
            threadStopFlag.Set();
        }
    }
}