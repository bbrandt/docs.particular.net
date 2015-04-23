﻿using NServiceBus;
using NServiceBus.Persistence;
using Raven.Client.Document;
using Raven.Client.Document.DTC;

class ConfiguringTransactionRecoveryStorage
{
    public void Foo()
    {
        #region ConfiguringTransactionRecoveryStorage

        string transactionRecoveryPath = "path to transaction recovery storage";
        DocumentStore myDocumentStore = new DocumentStore
        {
            TransactionRecoveryStorage = new LocalDirectoryTransactionRecoveryStorage(transactionRecoveryPath)
        };

        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.UsePersistence<RavenDBPersistence>()
            .SetDefaultDocumentStore(myDocumentStore);

        #endregion
    }
}
