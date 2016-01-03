using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SWNAdmin.Utility;
using System.Windows;
using System.IO;
using System.Windows.Forms;
using SWNAdmin;

namespace SWNAdmin
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false)]
    public class SWNService : ISWNService
    {
        Dictionary<Client, ISWNServiceCallback> clients = new Dictionary<Client, ISWNServiceCallback>();
        List<Client> clientList = new List<Client>();
        public static SWNService CurrentService;
        public static int LoggedInUsers = 0;

        object syncObj = new object();

        public SWNService()
        {
            CurrentService = this;
        }

        public ISWNServiceCallback CurrentCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<ISWNServiceCallback>();
            }
        }

        private bool SearchClientsByName(string username)
        {
            foreach (Client c in clients.Keys)
            {
                if (c.UserName == username)
                {
                    return true;
                }
            }
            return false;
        }

        #region ISWNService Members

        public int Connect(Client client)
        {
            int RegSuccessful = -1;
            int LoginSuccessful = -1;
            int Handshake = -1;

            if (client.eMail != null)
            {
                RegistrationHandler RH = new RegistrationHandler();
                RegSuccessful = RH.RegistrationCheck(client.UserName, client.eMail, client.encPassword);
                Handshake = RegSuccessful;
            }

            if (!clients.ContainsValue(CurrentCallback) && !SearchClientsByName(client.UserName))
            {
                lock (syncObj)
                {
                    LoginHandler LH = new LoginHandler();
                    LoginSuccessful = LH.LoginCheck(client.UserName, client.encPassword);
                    Handshake = LoginSuccessful;
                    if (LoginSuccessful == 1)
                    {
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" + client.UserName + "' logged in");
                        MainWindow.CurrentInstance.UpdateUserOnline(client.UserName, false);
                        foreach (Client key in clients.Keys)
                        {
                            ISWNServiceCallback callback = clients[key];
                            try
                            {
                                callback.RefreshClients(clientList);
                                callback.UserJoin(client);
                            }
                            catch (Exception)
                            {
                                clients.Remove(key);
                                return Handshake;
                            }
                        }
                        clients.Add(client, CurrentCallback);
                        clientList.Add(client);
                    }
                    if (RegSuccessful == 1)
                    {
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" + client.UserName + "' registered with the Server");
                    }
                }
                return Handshake;
            }
            return Handshake;
        }

        public void Disconnect(Client client)
        {
            foreach (Client c in clients.Keys)
            {
                if (client.UserName == c.UserName)
                {
                    lock (syncObj)
                    {
                        this.clients.Remove(c);
                        this.clientList.Remove(c);
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" + client.UserName + "' logged out");
                        MainWindow.CurrentInstance.UpdateUserOnline(client.UserName, true);
                        foreach (ISWNServiceCallback callback in clients.Values)
                        {
                            callback.RefreshClients(this.clientList);
                            callback.UserLeft(client);
                        }
                    }
                    return;
                }
            }
        }

        public void SendMessage(string Message, string UserName)
        {
            Message m = new Message();
            m.Sender = UserName;
            m.Time = DateTime.Now;
            m.Content = Message;
            lock (syncObj)
            {
                foreach (ISWNServiceCallback callback in clients.Values)
                {
                    callback.Receive(m);
                }
            }
            MainWindow.CurrentInstance.UpdateChatWindow(Message, UserName);
        }

        public void SendSystem(StarSystems ssystem)
        {
            lock (syncObj)
            {
                foreach (ISWNServiceCallback callback in clients.Values)
                {
                    callback.SendStarSystem(ssystem);
                }
            }
        }

        public void SendImage()
        {
                Stream strm = null;
                try
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif| All files(*.*)|*.*";
                    fileDialog.Multiselect = false;

                    System.Windows.Forms.DialogResult Result = fileDialog.ShowDialog();

                    if (Result == System.Windows.Forms.DialogResult.OK)
                    {
                        MainWindow.CurrentInstance.UpdateImageWindow(new Uri(fileDialog.FileName,UriKind.Absolute));
                        strm = fileDialog.OpenFile();

                        if (strm != null)
                        {
                            byte[] buffer = new byte[(int)strm.Length];

                            int i = strm.Read(buffer, 0, buffer.Length);

                            if (i > 0)
                            {
                                FileMessage fMsg = new FileMessage();
                                fMsg.FileName = fileDialog.SafeFileName;
                                fMsg.Sender = "Dummy";
                                fMsg.Data = buffer;

                                foreach (ISWNServiceCallback callback in clients.Values)
                                {
                                    callback.SendImage(fMsg);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                System.Windows.MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (strm != null)
                    {
                        strm.Close();
                    }
                }
        }

        public void SendFile()
        {
            Stream strm = null;
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                System.Windows.Forms.DialogResult Result = fileDialog.ShowDialog();

                if (Result == System.Windows.Forms.DialogResult.OK)
                {
                   strm = fileDialog.OpenFile();
                    if (strm != null)
                    {
                        byte[] buffer = new byte[(int)strm.Length];

                        int i = strm.Read(buffer, 0, buffer.Length);

                        if (i > 0)
                        {
                            FileMessage fMsg = new FileMessage();
                            fMsg.FileName = fileDialog.SafeFileName;
                            fMsg.Sender = "Dummy";
                            fMsg.Data = buffer;
                            foreach (ISWNServiceCallback callback in clients.Values)
                            {
                                callback.SendFile(fMsg);
                            }
                            //_callbackList.ForEach(delegate (ISWNServiceCallback callback)
                            //{
                            //    callback.SendFile(fMsg);                            
                            //});
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (strm != null)
                {
                    strm.Close();
                }
            }
        }

        public void ServerSendMessage(string Message, string UserName)
        {
            Message m = new Message();
            m.Sender = "Server";
            m.Time = DateTime.Now;
            m.Content = Message;
            MainWindow.CurrentInstance.UpdateChatWindow(Message, UserName);
            foreach (ISWNServiceCallback callback in clients.Values)
            {
                callback.Receive(m);
            }

            //_callbackList.ForEach(delegate (ISWNServiceCallback callback)
            //{
            //    callback.NotifyNewMessage(Message, UserName);
            //});
        }

        public void ServerIsInShutdownMode()
        {
            lock (syncObj)
            {
                foreach (ISWNServiceCallback callback in clients.Values)
                {
                    callback.ServiceIsShuttingDown();
                }
            }
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Informing Clients");
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + " Server - Waiting for Signoffs");
        }

        public List<string> RequestOnlineUsersList()
        {
            return MainWindow.CurrentInstance.GetUsersOnline();
        }
    }
    #endregion
}