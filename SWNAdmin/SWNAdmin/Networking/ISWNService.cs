﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SWNAdmin.Utility;

namespace SWNAdmin
{
    [ServiceContract(Name = "SWNService",Namespace = "SWNAdmin", SessionMode = SessionMode.Required,CallbackContract = typeof(ISWNServiceCallback))]
    interface ISWNService
    {
        [OperationContract(IsInitiating = true)]
        bool Connect(Client client);

        [OperationContract(IsInitiating = true)]
        bool Register(Client client);

        [OperationContract]
        void Disconnect(Client client);

        [OperationContract]
        void SendMessage(Message msg);

        [OperationContract]
        List<string> RequestOnlineUsersList();

        [OperationContract]
        Character GetBlankCharacter(Client client);

        [OperationContract]
        List<Advantages> RequestAdvantages(Client client);

        [OperationContract]
        List<Disadvantages> RequestDisadvantages(Client client);

        [OperationContract]
        List<Requirements> RequestRequirements(Client client);

        [OperationContract]
        List<Skills> RequestSkills(Client client);

        [OperationContract]
        List<Character> RequestSavedCharacters(Client client);

        [OperationContract]
        bool SaveCharacter(Client client, Character c);
    }
}
