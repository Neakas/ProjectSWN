using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows.Forms;
using SWNAdmin.Classes;
using SWNAdmin.Controller;
using SWNAdmin.Utility;
using UniverseGeneration.Utility;
using Message = SWNAdmin.Classes.Message;
using MessageBox = System.Windows.MessageBox;

namespace SWNAdmin.Networking
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single,
        UseSynchronizationContext = false)]
    public class SWNService : ISWNService
    {
        public static SWNService CurrentService;
        public static int LoggedInUsers = 0;

        private readonly Dictionary<Client, ISWNServiceCallback> clientsDict =
            new Dictionary<Client, ISWNServiceCallback>();

        private readonly object syncObj = new object();

        public SWNService()
        {
            CurrentService = this;
        }

        public ISWNServiceCallback CurrentCallback
        {
            get { return OperationContext.Current.GetCallbackChannel<ISWNServiceCallback>(); }
        }

        private bool SearchClientsByName(string username)
        {
            foreach (var c in clientsDict.Keys)
            {
                if (c.UserName == username)
                {
                    return true;
                }
            }
            return false;
        }

        #region ISWNService Members

        public bool Connect(Client client)
        {
            if (!clientsDict.ContainsValue(CurrentCallback) && !SearchClientsByName(client.UserName))
            {
                lock (syncObj)
                {
                    var LH = new LoginHandler();
                    if (LoginHandler.LoginCheck(client))
                    {
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") +
                                                                 ": New Connection Attempt from: " +
                                                                 getClientIpAddress(client));
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" +
                                                                 client.UserName + "' logged in");
                        MainWindow.CurrentInstance.UpdateUserOnline(client.UserName, false);
                        clientsDict.Add(client, CurrentCallback);
                        foreach (var key in clientsDict.Keys)
                        {
                            var callback = clientsDict[key];
                            try
                            {
                                callback.RefreshClients((from c in clientsDict.Keys select c.UserName).ToList());
                                callback.UserJoin(client);
                            }
                            catch (Exception)
                            {
                                clientsDict.Remove(key);
                                CurrentCallback.SendErrorCode("Server send Abort-Request!");
                            }
                        }
                        return true;
                    }
                    CurrentCallback.SendErrorCode("The User: " + client.UserName +
                                                  " does not exist or the Password is Incorrect!");
                }
            }
            else
            {
                CurrentCallback.SendErrorCode("The User: " + client.UserName +
                                              " is already Logged-In! Wait a few Seconds for Cleanup...");
                InvalidateUser(client);
            }
            return false;
        }

        private void InvalidateUser(Client client)
        {
            var LH = new LoginHandler();
            if (LoginHandler.LoginCheck(client))
            {
                //client is Ok. client wanted Access, but is allready Online. Can only be a Crash related Stuck Client + Callback
                Disconnect(client);
            }
        }

        public bool Register(Client client)
        {
            if (!clientsDict.ContainsValue(CurrentCallback) && !SearchClientsByName(client.UserName))
            {
                lock (syncObj)
                {
                    var RH = new RegistrationHandler();
                    if (RH.RegistrationCheck(client))
                    {
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" +
                                                                 client.UserName + "' registered with the Server");
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }


        public void Disconnect(Client client)
        {
            foreach (var c in clientsDict.Keys)
            {
                if (client.UserName == c.UserName)
                {
                    lock (syncObj)
                    {
                        clientsDict.Remove(c);
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" +
                                                                 client.UserName + "' logged out");
                        MainWindow.CurrentInstance.UpdateUserOnline(client.UserName, true);
                        foreach (var callback in clientsDict.Values)
                        {
                            callback.RefreshClients((from ce in clientsDict.Keys select ce.UserName).ToList());
                            callback.UserLeft(client);
                        }
                    }
                    return;
                }
            }
        }

        private string getClientIpAddress(Client c)
        {
            var ip = "";
            var context = OperationContext.Current;
            var prop = context.IncomingMessageProperties;
            var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            ip = endpoint.Address;
            if (ip == "::1")
            {
                return "localhost";
            }
            return ip;
        }

        public Character GetBlankCharacter(Client client)
        {
            return CharacterController.GetBlankCharacter(client);
        }

        public List<Advantages> RequestAdvantages(Client client)
        {
            var Advlist = new List<Advantages>();
            var context = new Db1Entities();
            Advlist = (from c in context.Advantages where c.isEnabled select c).ToList();
            return Advlist;
        }

        public List<Disadvantages> RequestDisadvantages(Client client)
        {
            var disAdvlist = new List<Disadvantages>();
            var context = new Db1Entities();
            disAdvlist = (from c in context.Disadvantages where c.isEnabled select c).ToList();
            return disAdvlist;
        }

        public List<Requirements> RequestRequirements(Client client)
        {
            var reqlist = new List<Requirements>();
            var context = new Db1Entities();
            reqlist = (from c in context.Requirements select c).ToList();
            return reqlist;
        }

        public List<Skills> RequestSkills(Client client)
        {
            var SkillList = new List<Skills>();
            var context = new Db1Entities();
            SkillList = (from c in context.Skills where c.isEnabled select c).ToList();
            return SkillList;
        }

        public void SendMessage(Message m)
        {
            lock (syncObj)
            {
                foreach (var callback in clientsDict.Values)
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
                foreach (var callback in clientsDict.Values)
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
                    var fileDialog = new OpenFileDialog();
                    fileDialog.Filter =
                        "All Files (*.*)|*.*|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif| JPEG files(*.jpeg)|*.jpeg";
                    fileDialog.Multiselect = false;

                    var Result = fileDialog.ShowDialog();

                    if (Result == DialogResult.OK)
                    {
                        MainWindow.CurrentInstance.UpdateImageWindow(new Uri(fileDialog.FileName, UriKind.Absolute));
                        strm = fileDialog.OpenFile();

                        if (strm != null)
                        {
                            var buffer = new byte[(int) strm.Length];

                            var i = strm.Read(buffer, 0, buffer.Length);

                            if (i > 0)
                            {
                                var fMsg = new FileMessage();
                                fMsg.FileName = fileDialog.SafeFileName;
                                fMsg.Sender = "Dummy";
                                fMsg.Data = buffer;

                                foreach (var callback in clientsDict.Values)
                                {
                                    callback.SendImage(fMsg);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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
                    var buffer = ImageByteArray;

                    //int i = strm.Read(buffer, 0, buffer.Length);

                    //if (i > 0)
                    //{
                    var fMsg = new FileMessage();
                    var dice = new Dice();
                    fMsg.FileName = "Image" + dice.rng(200000);
                    fMsg.Sender = "Dummy";
                    fMsg.Data = buffer;

                    foreach (var callback in clientsDict.Values)
                    {
                        callback.SendImage(fMsg);
                    }
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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
                var fileDialog = new OpenFileDialog();
                fileDialog.Multiselect = false;
                var Result = fileDialog.ShowDialog();

                if (Result == DialogResult.OK)
                {
                    strm = fileDialog.OpenFile();
                    if (strm != null)
                    {
                        var buffer = new byte[(int) strm.Length];

                        var i = strm.Read(buffer, 0, buffer.Length);

                        if (i > 0)
                        {
                            var fMsg = new FileMessage();
                            fMsg.FileName = fileDialog.SafeFileName;
                            fMsg.Sender = "Dummy";
                            fMsg.Data = buffer;
                            foreach (var callback in clientsDict.Values)
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
                MessageBox.Show(ex.ToString());
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
            var m = new Message();
            m.Sender = "Server";
            m.Time = DateTime.Now;
            m.Content = Message;
            MainWindow.CurrentInstance.UpdateChatWindow(Message, UserName);
            foreach (var callback in clientsDict.Values)
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
                foreach (var callback in clientsDict.Values)
                {
                    callback.ServiceIsShuttingDown();
                }
            }
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": Server - Informing Clients");
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") +
                                                     ": Server - Waiting for Signoffs");
        }

        public void KickSelectedUser(Client c)
        {
            lock (syncObj)
            {
                foreach (var client in clientsDict.Keys)
                {
                    if (client.UserName == c.UserName)
                    {
                        ISWNServiceCallback toKickUser;
                        clientsDict.TryGetValue(client, out toKickUser);
                        toKickUser.KickUser();
                    }
                }
            }
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": User: " + c.UserName +
                                                     " kicked from the Server");
            MainWindow.CurrentInstance.UpdateUserOnline(c.UserName, true);
        }

        public List<string> RequestOnlineUsersList()
        {
            return (from c in clientsDict.Keys select c.UserName).ToList();
        }

        public bool SaveCharacter(Client client, Character c)
        {
            var success = false;

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