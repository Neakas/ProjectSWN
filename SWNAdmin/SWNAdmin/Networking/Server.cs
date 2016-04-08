using System;
using System.ServiceModel;
using System.Threading;

namespace SWNAdmin.Networking
{
    public class Server
    {
        //ServiceHost SWNServiceHost;
        public static ServiceHost CurrentServiceHost;
        private readonly Thread ServerThread;
        public AutoResetEvent threadStopFlag = new AutoResetEvent(false);

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
            CurrentServiceHost = new ServiceHost(typeof (SWNService));
            try
            {
                CurrentServiceHost.Open();
            }
            catch (Exception e)
            {
                MainWindow.CurrentInstance.UpdateConsole(e.Message);
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
                    MainWindow.CurrentInstance.UpdateConsole(e.Message);
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