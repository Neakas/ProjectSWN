using SWNAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SWNAdmin
{
    public interface ISWNServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UserJoin(Client client);

        [OperationContract(IsOneWay = true)]
        void UserLeft(Client client);

        [OperationContract(IsOneWay = true)]
        void RefreshClients(List<string> clients);

        [OperationContract(IsOneWay = true)]
        void Receive(Message msg);

        [OperationContract(IsOneWay = true)]
        void SendErrorCode(string ErrorMessage);

        [OperationContract(IsOneWay = true)]
        void ServiceIsShuttingDown();

        [OperationContract(IsOneWay = true)]
        [ReferencePreservingDataContractFormat]
        void SendStarSystem(SWNAdmin.Utility.StarSystems StarSystem);

        [OperationContract(IsOneWay = true)]
        Task SendImage(FileMessage fMsg);

        [OperationContract(IsOneWay = true)]
        Task SendFile(FileMessage fMsg);

        [OperationContract(IsOneWay = true)]
        void KickUser();

        [OperationContract(IsOneWay = true)]
        void SendDateTime(DateTime Increment);
    }
}
