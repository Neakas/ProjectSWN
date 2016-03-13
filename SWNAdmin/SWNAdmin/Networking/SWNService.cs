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
using System.ServiceModel.Channels;
using System.Windows.Media.Imaging;

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

            if (!clients.ContainsValue(CurrentCallback) && !SearchClientsByName(client.UserName))
            {
                lock (syncObj)
                {
                    if (client.eMail != null)
                    {
                        RegistrationHandler RH = new RegistrationHandler();
                        RegSuccessful = RH.RegistrationCheck(client.UserName, client.eMail, client.encPassword);
                        Handshake = RegSuccessful;
                    }
                    else
                    {
                        LoginHandler LH = new LoginHandler();
                        LoginSuccessful = LH.LoginCheck(client.UserName, client.encPassword);
                        Handshake = LoginSuccessful;
                        if (LoginSuccessful == 1 && RegSuccessful == -1)
                        {
                            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": New Connection Attempt from: " + getClientIpAddress(client));
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

        private string getClientIpAddress(Client c)
        {
            
            string ip = "";
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            ip = endpoint.Address;
            return ip;
        }

        public Character GetBlankCharacter(Client client)
        {
            return Controller.CharacterController.GetBlankCharacter(client);
        }

        public List<Advantages> RequestAdvantages(Client client)
        {
            List<Advantages> Advlist = new List<Advantages>();
            var context = new Db1Entities();
            Advlist = (from c in context.Advantages where c.isEnabled == true select c).ToList();
            return Advlist;
        }

        public List<Disadvantages> RequestDisadvantages(Client client)
        {
            List<Disadvantages> disAdvlist = new List<Disadvantages>();
            var context = new Db1Entities();
            disAdvlist = (from c in context.Disadvantages where c.isEnabled == true select c).ToList();
            return disAdvlist;
        }

        public List<Requirements> RequestRequirements(Client client)
        {
            List<Requirements> reqlist = new List<Requirements>();
            var context = new Db1Entities();
            reqlist = (from c in context.Requirements select c).ToList();
            return reqlist;
        }

        public List<Skills> RequestSkills(Client client)
        {
            List<Skills> SkillList = new List<Skills>();
            var context = new Db1Entities();
            SkillList = (from c in context.Skills where c.isEnabled == true select c).ToList();
            return SkillList;
        }

        public void SendMessage(Message m)
        {
            lock (syncObj)
            {
                foreach (ISWNServiceCallback callback in clients.Values)
                {
                    callback.Receive(m);
                }
            }
            MainWindow.CurrentInstance.UpdateChatWindow(m.Content, m.Sender);
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

        public void SendImage(byte[] ImageByteArray = null)
        {
                Stream strm = null;
            if (ImageByteArray == null)
            {
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
            else
            {
                try
                {
                  
                    byte[] buffer = ImageByteArray;

                    //int i = strm.Read(buffer, 0, buffer.Length);

                    //if (i > 0)
                    //{
                        FileMessage fMsg = new FileMessage();
                            UniverseGeneration.Dice dice = new UniverseGeneration.Dice();
                        fMsg.FileName = "Image" + dice.rng(200000);
                        fMsg.Sender = "Dummy";
                        fMsg.Data = buffer;

                        foreach (ISWNServiceCallback callback in clients.Values)
                        {
                            callback.SendImage(fMsg);
                        }
                    //}
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
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": Server - Informing Clients");
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": Server - Waiting for Signoffs");
        }

        public void KickSelectedUser(Client c)
        {
            lock (syncObj)
            {
                foreach (Client client in clients.Keys)
                {
                    if (client.UserName == c.UserName)
                    {
                        ISWNServiceCallback toKickUser;
                        clients.TryGetValue(client, out toKickUser);
                        toKickUser.KickUser();
                    }
                }
            }
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": User: " + c.UserName + " kicked from the Server");
            MainWindow.CurrentInstance.UpdateUserOnline(c.UserName, true);
        }

        public List<string> RequestOnlineUsersList()
        {
            return MainWindow.CurrentInstance.GetUsersOnline();
        }
        
        public bool SaveCharacter(Client client, Character c)
        {
            bool success = false;

            using (var Context = new Db1Entities())
            {
                Context.Character.Add(c);
                Context.SaveChanges();
                success = true;
            }
            return success;
        }

        public List<Character> RequestSavedCharacters(Client c)
        {
            using (var Context = new Db1Entities())
            { 
                return (from q in Context.Character where q.PlayerName == c.UserName select q).ToList();
            }
        }
    }

    #endregion
}