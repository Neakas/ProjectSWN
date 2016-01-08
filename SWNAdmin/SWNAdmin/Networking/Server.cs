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
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public AutoResetEvent stopFlag = new AutoResetEvent(false);
        Thread ServerThread;
        ServiceHost SWNServiceHost;
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
            SWNServiceHost = new ServiceHost(typeof(SWNService));
            CurrentServiceHost = SWNServiceHost;
            try
            {
                SWNServiceHost.Open();
            }
            catch (Exception e)
            {
                MainWindow.CurrentInstance.UpdateConsole(e.Message.ToString());
                MainWindow.CurrentInstance.SwitchServerState();
            }
            finally
            {
                if (SWNServiceHost.State == CommunicationState.Opened)
                {
                    MainWindow.CurrentInstance.UpdateServerStatus(true);
                }
            }
            stopFlag.WaitOne();
            ServerThread.Join();
        }

        public void StopService()
        {
            if (SWNServiceHost != null)
            {
                try
                {
                    Thread.Sleep(1000);
                    SWNServiceHost.Close();
                }
                catch (Exception e)
                {
                    MainWindow.CurrentInstance.UpdateConsole(e.Message.ToString());
                }
                finally
                {
                    if (SWNServiceHost.State == CommunicationState.Closed)
                    {
                        MainWindow.CurrentInstance.UpdateServerStatus(false);
                    }
                }
            }
            stopFlag.Set();
        }

        //Cleanup Check if Cleanup needed
        //public void CloseOrAbortServiceChannel(ICommunicationObject communicationObject)
        //{
        //    bool isClosed = false;

        //    if (communicationObject == null || communicationObject.State == CommunicationState.Closed)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        if (communicationObject.State != CommunicationState.Faulted)
        //        {
        //            communicationObject.Close();
        //            isClosed = true;
        //        }
        //    }
        //    catch (CommunicationException)
        //    {
        //        // Catch this expected exception so it is not propagated further.
        //        // Perhaps write this exception out to log file for gathering statistics...
        //    }
        //    catch (TimeoutException)
        //    {
        //        // Catch this expected exception so it is not propagated further.
        //        // Perhaps write this exception out to log file for gathering statistics...
        //    }
        //    catch (Exception)
        //    {
        //        // An unexpected exception that we don't know how to handle.
        //        // Perhaps write this exception out to log file for support purposes...
        //        throw;
        //    }
        //    finally
        //    {
        //        // If State was Faulted or any exception occurred while doing the Close(), then do an Abort()
        //        if (!isClosed)
        //        {
        //            AbortServiceChannel(communicationObject);
        //        }
        //    }
        //}

        //private static void AbortServiceChannel(ICommunicationObject communicationObject)
    }
}