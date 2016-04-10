using System;
using System.ServiceModel;
using System.Threading;

namespace SWNAdmin.Networking
{
    public class Server
    {
        //ServiceHost SWNServiceHost;
        public static ServiceHost CurrentServiceHost;
        private readonly Thread _serverThread;
        private readonly AutoResetEvent _threadStopFlag = new AutoResetEvent(false);

        public Server()
        {
            CurrentServiceHost = null;
            _serverThread = new Thread(StartService)
            {
                IsBackground = true
            };
            _serverThread.Start();
            Thread.Sleep(2000);
        }

        private void StartService()
        {
            CurrentServiceHost = new ServiceHost(typeof (SwnService));
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
            _threadStopFlag.WaitOne();
            _serverThread.Join();
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
            _threadStopFlag.Set();
        }
    }
}