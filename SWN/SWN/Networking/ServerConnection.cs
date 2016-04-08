using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SWN.Controller;
using SWN.Service_References.SWNServiceReference;

namespace SWN.Networking
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ServerConnection : SWNServiceCallback
    {
        public ServerConnection()
        {
            if (CurrentInstance == null)
            {
                CurrentInstance = this;
            }
        }

        public static SWNServiceClient LocalServiceClient { get; set; }

        public static ServerConnection CurrentInstance { get; set; }

        public void SendErrorCode(string ErrorCode)
        {
            Application.Current.Dispatcher.BeginInvoke(
                (Action) delegate { Forms.Login.CurrentInstance.errormessage.Text = ErrorCode; });
        }


        public void SendImage(FileMessage fileMsg)
        {
            var ok = true;
            foreach (var img in SettingHandler.ImageList)
            {
                if (img == fileMsg.FileName)
                {
                    ok = false;
                }
            }
            if (ok)
            {
                MainWindow.CurrentInstance.UpdateFileReceive();
                try
                {
                    var fileStrm =
                        new FileStream(
                            XmlHandler.GrabXmlValue(SettingHandler.GrabSettingFile(), "PicFilePath") + @"\" +
                            fileMsg.FileName, FileMode.Create, FileAccess.ReadWrite);
                    fileStrm.Write(fileMsg.Data, 0, fileMsg.Data.Length);
                    fileStrm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MainWindow.CurrentInstance.CreateNotification(
                new Uri(XmlHandler.GrabXmlValue(SettingHandler.GrabSettingFile(), "PicFilePath") + @"\" +
                        fileMsg.FileName));

            var picnotinlist = true;
            foreach (var img in SettingHandler.ImageList)
            {
                if (fileMsg.FileName == img)
                {
                    picnotinlist = false;
                }
            }
            if (picnotinlist)
            {
                SettingHandler.ImageList.Add(fileMsg.FileName);
            }
        }

        public void SendFile(FileMessage fileMsg)
        {
            MainWindow.CurrentInstance.UpdateFileReceive();
            try
            {
                var fileStrm =
                    new FileStream(XmlHandler.GrabXmlValue(SettingHandler.GrabSettingFile(), "DataFilePath") +
                                   fileMsg.FileName, FileMode.Create,
                        FileAccess.ReadWrite);
                fileStrm.Write(fileMsg.Data, 0, fileMsg.Data.Length);
                fileStrm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MainWindow.CurrentInstance.UpdateFileReceive(true);
        }

        public async Task<bool> tryLogin(Client C)
        {
            Forms.Login.CurrentInstance.errormessage.Text = "";
            try
            {
                if (LocalServiceClient == null)
                {
                    LocalServiceClient = new SWNServiceClient(new InstanceContext(this), "netTcpBinding",
                        "net.tcp://" + SettingHandler.GetIpPort() + "/swnservice");
                }
                if (LocalServiceClient.State != CommunicationState.Opened)
                {
                    LocalServiceClient.Open();
                }
                var SuccessfulLogin = await LocalServiceClient.ConnectAsync(C);
                return SuccessfulLogin;
            }
            catch (FaultException exception)
            {
                Forms.Login.CurrentInstance.errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);

                return false;
            }
            catch (CommunicationException)
            {
                Forms.Login.CurrentInstance.errormessage.Text = "Server not Responding";
                CloseOrAbortServiceChannel(LocalServiceClient);

                return false;
            }
            catch (TimeoutException exception)
            {
                Forms.Login.CurrentInstance.errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);

                return false;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
                throw;
            }
        }


        public async Task<bool> tryReg(Client C)
        {
            try
            {
                if (LocalServiceClient == null)
                {
                    LocalServiceClient = new SWNServiceClient(new InstanceContext(this), "netTcpBinding",
                        "net.tcp://" + SettingHandler.GetIpPort() + "/swnservice");
                }
                if (LocalServiceClient.State != CommunicationState.Opened)
                {
                    LocalServiceClient.Open();
                }
                var Successfulreg = await LocalServiceClient.RegisterAsync(C);
                return Successfulreg;
            }
            catch (FaultException exception)
            {
                Forms.Login.CurrentInstance.errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
            catch (CommunicationException)
            {
                Forms.Login.CurrentInstance.errormessage.Text = "Server not Responding";
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
            catch (TimeoutException exception)
            {
                Forms.Login.CurrentInstance.errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
                throw;
            }
        }

        public void SendMessageToServer(string Message, string Username)
        {
            var m = new Message
            {
                Sender = Username,
                Time = DateTime.Now,
                Content = Message
            };
            LocalServiceClient.SendMessage(m);
        }

        public List<string> GrabLoggedInUsers()
        {
            return LocalServiceClient.RequestOnlineUsersList();
        }

        #region Server Callbacks

        public void RefreshClients(List<string> clients)
        {
            if (MainWindow.CurrentInstance != null)
            {
                MainWindow.CurrentInstance.UpdateUserOnline(clients);
            }
        }

        public void UserJoin(Client c)
        {
            if (MainWindow.CurrentInstance != null)
            {
                MainWindow.CurrentInstance.UpdateChatWindow("Server: " + c.UserName + " has Joined!");
            }
        }

        public void UserLeft(Client c)
        {
            MainWindow.CurrentInstance.UpdateChatWindow("Server: " + c.UserName + " has Left!");
        }

        public void Receive(Message m)
        {
            MainWindow.CurrentInstance.UpdateChatWindow(m.Sender + ": " + m.Content);
        }

        public void CloseOrAbortServiceChannel(ICommunicationObject communicationObject)
        {
            var isClosed = false;

            if (communicationObject == null || communicationObject.State == CommunicationState.Closed)
            {
                return;
            }

            try
            {
                if (communicationObject.State != CommunicationState.Faulted)
                {
                    communicationObject.Close();
                    isClosed = true;
                }
            }
            catch (CommunicationException)
            {
                // Catch this expected exception so it is not propagated further.
                // Perhaps write this exception out to log file for gathering statistics...
            }
            catch (TimeoutException)
            {
                // Catch this expected exception so it is not propagated further.
                // Perhaps write this exception out to log file for gathering statistics...
            }
            catch (Exception)
            {
                // An unexpected exception that we don't know how to handle.
                // Perhaps write this exception out to log file for support purposes...
                throw;
            }
            finally
            {
                // If State was Faulted or any exception occurred while doing the Close(), then do an Abort()
                if (!isClosed)
                {
                    AbortServiceChannel(communicationObject);
                }
            }
        }

        private static void AbortServiceChannel(ICommunicationObject communicationObject)
        {
            try
            {
                communicationObject.Abort();
                LocalServiceClient = null;
            }
            catch (Exception)
            {
            }
        }

        public bool CheckConnection()
        {
            var AllGood = false;
            if (LocalServiceClient != null)
            {
                if (LocalServiceClient.State == CommunicationState.Faulted ||
                    LocalServiceClient.State == CommunicationState.Closed ||
                    LocalServiceClient.State == CommunicationState.Closing ||
                    LocalServiceClient.InnerDuplexChannel.State == CommunicationState.Faulted)
                {
                    HandleProxy();
                    return AllGood;
                }
                AllGood = true;
            }
            return AllGood;
        }

        public void HandleProxy()
        {
            if (LocalServiceClient != null)
            {
                switch (LocalServiceClient.State)
                {
                    case CommunicationState.Closed:
                        LocalServiceClient = null;
                        MainWindow.CurrentInstance.lbUserOnline.Items.Clear();
                        MessageBox.Show("Connection to Server Closed");
                        break;
                    case CommunicationState.Closing:
                        break;
                    case CommunicationState.Created:
                        break;
                    case CommunicationState.Faulted:
                        LocalServiceClient.Abort();
                        LocalServiceClient = null;
                        MainWindow.CurrentInstance.lbUserOnline.ItemsSource = null;
                        MainWindow.CurrentInstance.lbUserOnline.Items.Clear();
                        MessageBox.Show("Connection to Server Lost");
                        MainWindow.CurrentInstance.Close();
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

        public void ServiceIsShuttingDown()
        {
            Thread.Sleep(3000);
            CheckConnection();
            LocalServiceClient.Close();
            MessageBox.Show("Server in Shutdown-Mode.Closing...");
            Environment.Exit(0);
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.Shutdown();
        }

        public void SendStarSystem(StarSystems System)
        {
            XmlHandler.SerializeSystem(System);
        }

        public void KickUser()
        {
            Thread.Sleep(3000);
            CheckConnection();
            MainWindow.CurrentInstance.UserGetsKicked();
            LocalServiceClient.Close();
            MessageBox.Show("You were kicked from the Server");
            Environment.Exit(0);
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.Shutdown();
        }

        public void SendDateTime(DateTime Increment)
        {
            var CurrentDateTime = SettingHandler.GetCurrentDateTime();
        }

        #endregion
    }
}