using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Windows;

namespace SWNAdmin
{
    //Dieses ServiceBehavior sagt WPF das immer eine Neue Instanz des Services Erstellt werden soll, wenn immer der Service abgerufen wird.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    //Hier wird das IService Interface an die Klasse ServiceDefinition gebunden
    public class ServiceDefinition : IService
    {
        public int UserLogin(string UserName, string eMail, string encPassword)
        {
            int RegSuccessful = -1;
            int LoginSuccessful = -1;
            int Handshake = -1;

            if (eMail != null)
            {
                RegistrationHandler RH = new RegistrationHandler();
                RegSuccessful = RH.RegistrationCheck(UserName, eMail, encPassword);
                Handshake = RegSuccessful;
            }
            else
            {
                LoginHandler LH = new LoginHandler();
                LoginSuccessful = LH.LoginCheck(UserName, encPassword);
                Handshake = LoginSuccessful;
            }
           
            if (LoginSuccessful == 1)
            {
                MainWindow.UpdateConsole(UserName + " hat sich eingeloggt!");
            }
            if (RegSuccessful == 1)
            {
                MainWindow.UpdateConsole(UserName + " hat sich registriert!");
            }
            return Handshake;
        }
        public CharacterController StoreUserCharacter(string UserName, CharacterController UserCharacter)
        {
            // Store in DB
            MainWindow.UpdateConsole(UserName + " | Character: " + UserCharacter.Name + " registriert!");
            return UserCharacter;
        }
        
    }
}
