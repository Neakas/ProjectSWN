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
    [ServiceBehavior( ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = false )]
    public class SwnService : ISwnService
    {
        public static SwnService CurrentService;
        public static int LoggedInUsers = 0;

        private readonly Dictionary<Client, ISwnServiceCallback> _clientsDict = new Dictionary<Client, ISwnServiceCallback>();

        private readonly object _syncObj = new object();

        public SwnService()
        {
            CurrentService = this;
        }

        public ISwnServiceCallback CurrentCallback => OperationContext.Current.GetCallbackChannel<ISwnServiceCallback>();

        private bool SearchClientsByName( string username )
        {
            return _clientsDict.Keys.Any(c => c.UserName == username);
        }

        #region ISwnService Members

        public bool Connect( Client client )
        {
            lock (_syncObj)
            {
                if (!_clientsDict.ContainsValue(CurrentCallback) && !SearchClientsByName(client.UserName))
                {
                    if (LoginHandler.LoginCheck(client))
                    {
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": New Connection Attempt from: " + GetClientIpAddress());
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" + client.UserName + "' logged in");
                        MainWindow.CurrentInstance.UpdateUserOnline(client.UserName, false);
                        _clientsDict.Add(client, CurrentCallback);
                        foreach (var key in _clientsDict.Keys)
                        {
                            var callback = _clientsDict[key];
                            try
                            {
                                callback.RefreshClients(( from c in _clientsDict.Keys select c.UserName ).ToList());
                                callback.UserJoin(client);
                            }
                            catch (Exception)
                            {
                                _clientsDict.Remove(key);
                                CurrentCallback.SendErrorCode("Server send Abort-Request!");
                            }
                        }
                        return true;
                    }
                    CurrentCallback.SendErrorCode("The User: " + client.UserName + " does not exist or the Password is Incorrect!");
                }
                else
                {
                    CurrentCallback.SendErrorCode("The User: " + client.UserName + " is already Logged-In! Wait a few Seconds for Cleanup...");
                    InvalidateUser(client);
                }
                return false;
            }
        }

        private void InvalidateUser( Client client )
        {
            if (LoginHandler.LoginCheck(client))
            {
                //client is Ok. client wanted Access, but is allready Online. Can only be a Crash related Stuck Client + Callback
                Disconnect(client);
            }
        }

        public bool Register( Client client )
        {
            lock (_syncObj)
            {
                if (_clientsDict.ContainsValue(CurrentCallback) || SearchClientsByName(client.UserName))
                {
                    return false;
                }
                var rh = new RegistrationHandler();
                if (!rh.RegistrationCheck(client))
                {
                    return false;
                }
                MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" + client.UserName + "' registered with the Server");
                return true;
            }
        }

        public void Disconnect( Client client )
        {
            lock (_syncObj)
            {
                foreach (var c in _clientsDict.Keys.Where(c => client.UserName == c.UserName))
                {
                    lock (_syncObj)
                    {
                        _clientsDict.Remove(c);
                        MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": The User '" + client.UserName + "' logged out");
                        MainWindow.CurrentInstance.UpdateUserOnline(client.UserName, true);
                        foreach (var callback in _clientsDict.Values)
                        {
                            callback.RefreshClients(( from ce in _clientsDict.Keys select ce.UserName ).ToList());
                            callback.UserLeft(client);
                        }
                    }
                    return;
                }
            }
        }

        private static string GetClientIpAddress()
        {
            var ip = "";
            var context = OperationContext.Current;
            var prop = context.IncomingMessageProperties;
            var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpoint != null)
            {
                ip = endpoint.Address;
            }
            return ip == "::1" ? "localhost" : ip;
        }

        public Character GetBlankCharacter( Client client )
        {
            return CharacterController.GetBlankCharacter(client);
        }

        public List<Advantages> RequestAdvantages( Client client )
        {
            var context = new Db1Entities();
            var advlist = ( from c in context.Advantages where c.isEnabled select c ).ToList();
            return advlist;
        }

        public List<Disadvantages> RequestDisadvantages( Client client )
        {
            var context = new Db1Entities();
            var disAdvlist = ( from c in context.Disadvantages where c.isEnabled select c ).ToList();
            return disAdvlist;
        }

        public List<Requirements> RequestRequirements( Client client )
        {
            var context = new Db1Entities();
            var reqlist = ( from c in context.Requirements select c ).ToList();
            return reqlist;
        }

        public List<Skills> RequestSkills( Client client )
        {
            var context = new Db1Entities();
            var skillList = ( from c in context.Skills where c.isEnabled select c ).ToList();
            return skillList;
        }

        public void SendMessage( Message m )
        {
            lock (_syncObj)
            {
                foreach (var callback in _clientsDict.Values)
                {
                    callback.Receive(m);
                }
            }
            MainWindow.CurrentInstance.UpdateChatWindow(m.Content, m.Sender);
        }

        public void SendSystem( StarSystems ssystem )
        {
            lock (_syncObj)
            {
                foreach (var callback in _clientsDict.Values)
                {
                    callback.SendStarSystem(ssystem);
                }
            }
        }

        public void SendImage( byte[] imageByteArray = null )
        {
            Stream strm = null;
            if (imageByteArray == null)
            {
                try
                {
                    var fileDialog = new OpenFileDialog
                    {
                        Filter = @"All Files (*.*)|*.*|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif| JPEG files(*.jpeg)|*.jpeg",
                        Multiselect = false
                    };

                    var result = fileDialog.ShowDialog();

                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                    MainWindow.CurrentInstance.UpdateImageWindow(new Uri(fileDialog.FileName, UriKind.Absolute));
                    strm = fileDialog.OpenFile();

                    var buffer = new byte[(int) strm.Length];

                    var i = strm.Read(buffer, 0, buffer.Length);

                    if (i <= 0)
                    {
                        return;
                    }
                    var fMsg = new FileMessage
                    {
                        FileName = fileDialog.SafeFileName,
                        Sender = "Dummy",
                        Data = buffer
                    };

                    lock (_syncObj)
                    {
                        foreach (var callback in _clientsDict.Values)
                        {
                            callback.SendImage(fMsg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    strm?.Close();
                }
            }
            else
            {
                try
                {
                    var buffer = imageByteArray;

                    var fMsg = new FileMessage();
                    var dice = new Dice();
                    fMsg.FileName = "Image" + dice.Rng(200000);
                    fMsg.Sender = "Dummy";
                    fMsg.Data = buffer;

                    lock (_syncObj)
                    {
                        foreach (var callback in _clientsDict.Values)
                        {
                            callback.SendImage(fMsg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public void SendFile()
        {
            Stream strm = null;
            try
            {
                var fileDialog = new OpenFileDialog
                {
                    Multiselect = false
                };
                var result = fileDialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }
                strm = fileDialog.OpenFile();
                {
                    var buffer = new byte[(int) strm.Length];

                    var i = strm.Read(buffer, 0, buffer.Length);

                    if (i <= 0)
                    {
                        return;
                    }
                    var fMsg = new FileMessage
                    {
                        FileName = fileDialog.SafeFileName,
                        Sender = "Dummy",
                        Data = buffer
                    };
                    lock (_syncObj)
                    {
                        foreach (var callback in _clientsDict.Values)
                        {
                            callback.SendFile(fMsg);
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
                strm?.Close();
            }
        }

        public void ServerSendMessage( string message, string userName )
        {
            var m = new Message
            {
                Sender = "Server",
                Time = DateTime.Now,
                Content = message
            };
            MainWindow.CurrentInstance.UpdateChatWindow(message, userName);
            lock (_syncObj)
            {
                foreach (var callback in _clientsDict.Values)
                {
                    callback.Receive(m);
                }
            }
        }

        public void ServerIsInShutdownMode()
        {
            lock (_syncObj)
            {
                if (_clientsDict != null)
                {
                    foreach (var callback in _clientsDict.Values)
                    {
                        callback.ServiceIsShuttingDown();
                    }
                }
            }
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": Server - Informing Clients");
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": Server - Waiting for Signoffs");
        }

        public void KickSelectedUser( Client c )
        {
            lock (_syncObj)
            {
                foreach (var client in _clientsDict.Keys.Where(client => client.UserName == c.UserName))
                {
                    ISwnServiceCallback toKickUser;
                    _clientsDict.TryGetValue(client, out toKickUser);
                    toKickUser?.KickUser();
                }
            }
            MainWindow.CurrentInstance.UpdateConsole(DateTime.Now.ToString("HH:mm:ss") + ": User: " + c.UserName + " kicked from the Server");
            MainWindow.CurrentInstance.UpdateUserOnline(c.UserName, true);
        }

        public List<string> RequestOnlineUsersList()
        {
            return ( from c in _clientsDict.Keys select c.UserName ).ToList();
        }

        public bool SaveCharacter( Client client, Character c )
        {
            using (var context = new Db1Entities())
            {
                context.Character.Add(c);
                context.SaveChanges();
            }
            return true;
        }

        public List<Character> RequestSavedCharacters( Client c )
        {
            using (var context = new Db1Entities())
            {
                return ( from q in context.Character where q.PlayerName == c.UserName select q ).ToList();
            }
        }
    }

    #endregion
}