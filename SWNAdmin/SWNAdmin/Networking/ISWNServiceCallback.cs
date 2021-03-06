﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using SWNAdmin.Classes;
using SWNAdmin.Utility;

namespace SWNAdmin.Networking
{
    [ServiceContract]
    public interface ISwnServiceCallback
    {
        [OperationContract( IsOneWay = true )]
        void UserJoin( Client client );

        [OperationContract( IsOneWay = true )]
        void UserLeft( Client client );

        [OperationContract( IsOneWay = true )]
        void RefreshClients( List<string> clients );

        [OperationContract( IsOneWay = true )]
        void Receive( Message msg );

        [OperationContract( IsOneWay = true )]
        void SendErrorCode( string errorMessage );

        [OperationContract( IsOneWay = true )]
        void ServiceIsShuttingDown();

        [OperationContract( IsOneWay = true )]
        [ReferencePreservingDataContractFormat]
        void SendStarSystem( StarSystems starSystem );

        [OperationContract( IsOneWay = true )]
        Task SendImage( FileMessage fMsg );

        [OperationContract( IsOneWay = true )]
        Task SendFile( FileMessage fMsg );

        [OperationContract( IsOneWay = true )]
        void KickUser();

        [OperationContract( IsOneWay = true )]
        void SendDateTime( DateTime increment );
    }
}