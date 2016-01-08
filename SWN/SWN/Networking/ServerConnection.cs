using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using amexus.Encryption;
using System.ServiceModel;
using System.Runtime.Serialization;
using SWNAdmin;
using System.Threading;
using SWNAdmin.Utility;
using System.IO;
using SWN.SWNServiceReference;

namespace SWN
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class ServerConnection : SWNServiceReference.SWNServiceCallback
    {
        public static ServerConnection CurrentConnection;
        public static SWNServiceReference.SWNServiceClient SWNClient;
        Login CurrentLoginWindow = Login.CurrentLoginWindow;
        List<Client> OnlineClients = new List<Client>();

        public ServerConnection()
        {
            CurrentConnection = this;
        }

        public int ClientReg(Client C)
        {
            try
            {
                SWNClient = new SWNServiceReference.SWNServiceClient(new InstanceContext(this), "netTcpBinding", "net.tcp://" + SettingHandler.GetIPPort() + "/SWNService");
                SWNClient.Open();
                int SuccessfulLogin = SWNClient.Connect(C);
                return SuccessfulLogin;
            }
            catch (FaultException exception)
            {
                CurrentLoginWindow.errormessage.Text = "Got " + exception.GetType().ToString();
                CloseOrAbortServiceChannel(SWNClient);

                return -2;
            }
            catch (CommunicationException)
            {
                CurrentLoginWindow.errormessage.Text = "Server not Responding";
                CloseOrAbortServiceChannel(SWNClient);

                return -2;
            }
            catch (TimeoutException exception)
            {
                CurrentLoginWindow.errormessage.Text = "Got " + exception.GetType().ToString();
                CloseOrAbortServiceChannel(SWNClient);

                return -2;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                CloseOrAbortServiceChannel(SWNClient);
                return -1;
                throw;
            }
        }

        public int ClientInit(Client C)
        {
            try
            {
                SWNClient = new SWNServiceReference.SWNServiceClient(new InstanceContext(this), "netTcpBinding", "net.tcp://" + SettingHandler.GetIPPort() + "/swnservice");
                SWNClient.Open();
                string eMail = null;
                C.eMail = eMail;
                int SuccessfulLogin = SWNClient.Connect(C);
                SettingHandler.SetUserName(C.UserName);
                return SuccessfulLogin;
            }
            catch (FaultException exception)
            {
                CurrentLoginWindow.errormessage.Text = "Got " + exception.GetType().ToString();
                CloseOrAbortServiceChannel(SWNClient);

                return -2;
            }
            catch (CommunicationException)
            {
                CurrentLoginWindow.errormessage.Text = "Server not Responding";
                CloseOrAbortServiceChannel(SWNClient);

                return -2;
            }
            catch (TimeoutException exception)
            {
                CurrentLoginWindow.errormessage.Text = "Got " + exception.GetType().ToString();
                CloseOrAbortServiceChannel(SWNClient);

                return -2;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                CloseOrAbortServiceChannel(SWNClient);
                return -1;
                throw;
            }
        }

        public void SendImage(SWNServiceReference.FileMessage fileMsg)
        {
            bool ok = true;
            foreach (string img in SettingHandler.ImageList)
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
                    FileStream fileStrm = new FileStream(XmlHandler.GrabXMLValue(SettingHandler.GrabSettingFile(), "PicFilePath") + @"\" + fileMsg.FileName, FileMode.Create,FileAccess.ReadWrite);
                    fileStrm.Write(fileMsg.Data, 0, fileMsg.Data.Length);
                    fileStrm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            MainWindow.CurrentInstance.UpdateImageWindow(new Uri(XmlHandler.GrabXMLValue(SettingHandler.GrabSettingFile(), "PicFilePath") + @"\" + fileMsg.FileName));

            bool picnotinlist = true;
            foreach (string img in SettingHandler.ImageList)
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

        public void SendFile(SWNServiceReference.FileMessage fileMsg)
        {
            MainWindow.CurrentInstance.UpdateFileReceive();
            try
            {
                FileStream fileStrm = new FileStream(XmlHandler.GrabXMLValue(SettingHandler.GrabSettingFile(), "DataFilePath") +
                           fileMsg.FileName, FileMode.Create,
                           FileAccess.ReadWrite);
                fileStrm.Write(fileMsg.Data, 0, fileMsg.Data.Length);
                fileStrm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            MainWindow.CurrentInstance.UpdateFileReceive(true);
        }

        public void SendMessageToServer(string Message, string Username)
        {
            SWNClient = new SWNServiceReference.SWNServiceClient(new InstanceContext(this), "netTcpBinding", "net.tcp://" + SettingHandler.GetIPPort() + "/SWNService");
            SWNClient.Open();
            Message m = new Message();
            m.Sender = Username;
            m.Time = DateTime.Now;
            m.Content = Message;
            SWNClient.SendMessage(m);
        }

        public List<string> GrabLoggedInUsers()
        {
            SWNClient = new SWNServiceReference.SWNServiceClient(new InstanceContext(this), "netTcpBinding", "net.tcp://" + SettingHandler.GetIPPort() + "/SWNService");
            SWNClient.Open();
            List<string> UserList = SWNClient.RequestOnlineUsersList();
            return UserList;
        }

        //für CharacterTransfer
        //public CharacterController TransferCharacter(string UserName, CharacterController UserCharacter)
        //{
        //    scf = new ChannelFactory<IService>(new NetTcpBinding(), "net.tcp://" + SettingHandler.GetIPPort());
        //    ServiceInterface = scf.CreateChannel();
        //    CharacterController returnedChar = ServiceInterface.StoreUserCharacter(UserName, UserCharacter);
        //    (ServiceInterface as ICommunicationObject).Close();
        //    return returnedChar;
        //}

        //public List<SWNAdmin.Utility.Advantages> GrabAdvantageListFromServer()
        //{
        //    scf = new ChannelFactory<IService>(new NetTcpBinding(), "net.tcp://" + SettingHandler.GetIPPort());
        //    ServiceInterface = scf.CreateChannel();
        //    var AdvantageList = ServiceInterface.ClientCallAdvantages();
        //    (ServiceInterface as ICommunicationObject).Close();
        //    return AdvantageList;
        //}

        //public void GrabDisadvantageListFromServer()
        //{
        //    scf = new ChannelFactory<IService>(new NetTcpBinding(), "net.tcp://" + SettingHandler.GetIPPort());
        //    ServiceInterface = scf.CreateChannel();
        //    var DisadvantageList = ServiceInterface.ClientCallDisadvantages();
        //    (ServiceInterface as ICommunicationObject).Close();
        //}

        //public void GrabCharacterBonusListFromServer()
        //{
        //    scf = new ChannelFactory<IService>(new NetTcpBinding(), "net.tcp://" + SettingHandler.GetIPPort());
        //    ServiceInterface = scf.CreateChannel();
        //    var CharacterBonusList = ServiceInterface.ClientCallCharacterBonus();
        //    (ServiceInterface as ICommunicationObject).Close();
        //}

        //public void GrabCharacterMalusListFromServer()
        //{
        //    scf = new ChannelFactory<IService>(new NetTcpBinding(), "net.tcp://" + SettingHandler.GetIPPort());
        //    ServiceInterface = scf.CreateChannel();
        //    var CharacterMalusList = ServiceInterface.ClientCallCharacterMalus();
        //    (ServiceInterface as ICommunicationObject).Close();
        //}

        #region Server Callbacks

        public void RefreshClients(List<Client> clients)
        {
            MainWindow.CurrentInstance.lbUserOnline.Items.Clear();
            OnlineClients.Clear();
            foreach (Client c in clients)
            {
                MainWindow.CurrentInstance.lbUserOnline.Items.Add(c.UserName);
                OnlineClients.Add(c);
            }
        }

        public void UserJoin(Client c)
        {
            MainWindow.CurrentInstance.UpdateChatWindow("The User: " + c.UserName + " has Joined!", c.UserName);
        }

        public void UserLeft(Client c)
        {
            MainWindow.CurrentInstance.UpdateChatWindow("The User: " + c.UserName + " has Left!", c.UserName);
        }

        public void Receive(Message m)
        {
            MainWindow.CurrentInstance.UpdateChatWindow(m.Content, m.Sender);
        }

        public void CloseOrAbortServiceChannel(ICommunicationObject communicationObject)
        {
            bool isClosed = false;

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
            }
            catch (Exception)
            {
                // An unexpected exception that we don't know how to handle.
                // If we are in this situation:
                // - we should NOT retry the Abort() because it has already failed and there is nothing to suggest it could be successful next time
                // - the abort may have partially succeeded
                // - the actual service call may have been successful
                //
                // The only thing we can do is hope that the channel's resources have been released.
                // Do not rethrow this exception because the actual service operation call might have succeeded
                // and an exception closing the channel should not stop the client doing whatever it does next.
                //
                // Perhaps write this exception out to log file for gathering statistics and support purposes...
            }
        }

        public void ServiceIsShuttingDown()
        {
            System.Threading.Thread.Sleep(3000);
            MainWindow.CurrentInstance.CheckConnection();
            SWNClient.Close();
            MessageBox.Show("Server in Shutdown-Mode.Closing...");
            Environment.Exit(0);
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Application.Current.Shutdown();
        }

        public void SendStarSystem(SWNServiceReference.StarSystems System)
        {
            XmlHandler.SerializeSystem(System);
        }


        #endregion
    }
}