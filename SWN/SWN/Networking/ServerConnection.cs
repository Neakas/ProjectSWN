﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SWN.Controller;
using SWN.Forms;
using SWN.SWNServiceReference;

namespace SWN.Networking
{
    [CallbackBehavior( ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false )]
    public class ServerConnection : SwnServiceCallback
    {
        public ServerConnection()
        {
            if (CurrentInstance == null)
            {
                CurrentInstance = this;
            }
        }

        public static SwnServiceClient LocalServiceClient { get; set; }

        public static ServerConnection CurrentInstance { get; set; }

        public void SendErrorCode( string errorCode )
        {
            if (errorCode == null)
            {
                throw new ArgumentNullException(nameof(errorCode));
            }
            Application.Current.Dispatcher.BeginInvoke((Action) delegate { Login.CurrentInstance.Errormessage.Text = errorCode; });
        }

        public void SendImage( FileMessage fileMsg )
        {
            var ok = true;
            foreach (var img in SettingHandler.ImageList.Where(img => img == fileMsg.FileName))
            {
                ok = false;
            }
            if (ok)
            {
                MainWindow.CurrentInstance.UpdateFileReceive();
                try
                {
                    var fileStrm = new FileStream(XmlHandler.GrabXmlValue(SettingHandler.GrabSettingFile(), "PicFilePath") + @"\" + fileMsg.FileName, FileMode.Create, FileAccess.ReadWrite);
                    fileStrm.Write(fileMsg.Data, 0, fileMsg.Data.Length);
                    fileStrm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MainWindow.CurrentInstance.CreateNotification(new Uri(XmlHandler.GrabXmlValue(SettingHandler.GrabSettingFile(), "PicFilePath") + @"\" + fileMsg.FileName));

            var picnotinlist = true;
            foreach (var img in SettingHandler.ImageList.Where(img => fileMsg.FileName == img))
            {
                picnotinlist = false;
            }
            if (picnotinlist)
            {
                SettingHandler.ImageList.Add(fileMsg.FileName);
            }
        }

        public void SendFile( FileMessage fileMsg )
        {
            MainWindow.CurrentInstance.UpdateFileReceive();
            try
            {
                var fileStrm = new FileStream(XmlHandler.GrabXmlValue(SettingHandler.GrabSettingFile(), "DataFilePath") + fileMsg.FileName, FileMode.Create, FileAccess.ReadWrite);
                fileStrm.Write(fileMsg.Data, 0, fileMsg.Data.Length);
                fileStrm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MainWindow.CurrentInstance.UpdateFileReceive(true);
        }

        public async Task<bool> TryLogin( Client client )
        {
            Login.CurrentInstance.Errormessage.Text = "";
            try
            {
                if (LocalServiceClient == null)
                {
                    LocalServiceClient = new SwnServiceClient(new InstanceContext(this), "netTcpBinding", "net.tcp://" + SettingHandler.GetIpPort() + "/SwnService");
                }
                if (LocalServiceClient.State != CommunicationState.Opened)
                {
                    LocalServiceClient.Open();
                }
                var successfulLogin = await LocalServiceClient.ConnectAsync(client);
                return successfulLogin;
            }
            catch (FaultException exception)
            {
                Login.CurrentInstance.Errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);

                return false;
            }
            catch (CommunicationException)
            {
                Login.CurrentInstance.Errormessage.Text = "Server not Responding";
                CloseOrAbortServiceChannel(LocalServiceClient);

                return false;
            }
            catch (TimeoutException exception)
            {
                Login.CurrentInstance.Errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);

                return false;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
        }

        public async Task<bool> TryReg( Client client )
        {
            try
            {
                if (LocalServiceClient == null)
                {
                    LocalServiceClient = new SwnServiceClient(new InstanceContext(this), "netTcpBinding", "net.tcp://" + SettingHandler.GetIpPort() + "/SwnService");
                }
                if (LocalServiceClient.State != CommunicationState.Opened)
                {
                    LocalServiceClient.Open();
                }
                var successfulreg = await LocalServiceClient.RegisterAsync(client);
                return successfulreg;
            }
            catch (FaultException exception)
            {
                Login.CurrentInstance.Errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
            catch (CommunicationException)
            {
                Login.CurrentInstance.Errormessage.Text = "Server not Responding";
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
            catch (TimeoutException exception)
            {
                Login.CurrentInstance.Errormessage.Text = "Got " + exception.GetType();
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                CloseOrAbortServiceChannel(LocalServiceClient);
                return false;
            }
        }

        public void SendMessageToServer( string message, string username )
        {
            var m = new Message
            {
                Sender = username,
                Time = DateTime.Now,
                Content = message
            };
            LocalServiceClient.SendMessage(m);
        }

        public List<string> GrabLoggedInUsers()
        {
            return LocalServiceClient.RequestOnlineUsersList();
        }

        #region Server Callbacks

        public void RefreshClients( List<string> clients )
        {
            if (MainWindow.CurrentInstance != null)
            {
                MainWindow.CurrentInstance.UpdateUserOnline(clients);
            }
        }

        public void UserJoin( Client client )
        {
            if (MainWindow.CurrentInstance != null)
            {
                MainWindow.CurrentInstance.UpdateChatWindow("Server: " + client.UserName + " has Joined!");
            }
        }

        public void UserLeft( Client client )
        {
            MainWindow.CurrentInstance.UpdateChatWindow("Server: " + client.UserName + " has Left!");
        }

        public void Receive( Message message )
        {
            MainWindow.CurrentInstance.UpdateChatWindow(message.Sender + ": " + message.Content);
        }

        public void CloseOrAbortServiceChannel( ICommunicationObject communicationObject )
        {
            var isClosed = false;

            if (communicationObject == null || communicationObject.State == CommunicationState.Closed)
            {
                return;
            }

            try
            {
                if (communicationObject.State == CommunicationState.Faulted)
                {
                    return;
                }
                communicationObject.Close();
                isClosed = true;
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
            finally
            {
                // If State was Faulted or any exception occurred while doing the Close(), then do an Abort()
                if (!isClosed)
                {
                    AbortServiceChannel(communicationObject);
                }
            }
        }

        private static void AbortServiceChannel( ICommunicationObject communicationObject )
        {
            try
            {
                communicationObject.Abort();
                LocalServiceClient = null;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool CheckConnection()
        {
            if (LocalServiceClient == null)
            {
                return false;
            }
            if (LocalServiceClient.State != CommunicationState.Faulted && LocalServiceClient.State != CommunicationState.Closed && LocalServiceClient.State != CommunicationState.Closing &&
                LocalServiceClient.InnerDuplexChannel.State != CommunicationState.Faulted)
            {
                return true;
            }
            HandleProxy();
            return false;
        }

        public void HandleProxy()
        {
            if (LocalServiceClient != null)
            {
                switch (LocalServiceClient.State)
                {
                    case CommunicationState.Closed:
                        LocalServiceClient = null;
                        MainWindow.CurrentInstance.LbUserOnline.Items.Clear();
                        MessageBox.Show("Connection to Server Closed");
                        break;
                    case CommunicationState.Closing:
                        break;
                    case CommunicationState.Created:
                        break;
                    case CommunicationState.Faulted:
                        LocalServiceClient.Abort();
                        LocalServiceClient = null;
                        MainWindow.CurrentInstance.LbUserOnline.ItemsSource = null;
                        MainWindow.CurrentInstance.LbUserOnline.Items.Clear();
                        MessageBox.Show("Connection to Server Lost");
                        MainWindow.CurrentInstance.Close();
                        break;
                    case CommunicationState.Opened:
                        MessageBox.Show("Connected");
                        break;
                    case CommunicationState.Opening:
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
        }

        public void SendStarSystem( StarSystems system )
        {
            XmlHandler.SerializeSystem(system);
        }

        public void KickUser()
        {
            Thread.Sleep(3000);
            CheckConnection();
            MainWindow.CurrentInstance.UserGetsKicked();
            LocalServiceClient.Close();
            MessageBox.Show("You were kicked from the Server");
            Environment.Exit(0);
        }

        public void SendDateTime( DateTime increment )
        {
            SettingHandler.GetCurrentDateTime();
        }

        #endregion
    }
}