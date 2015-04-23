﻿using System;
using NServiceBus;
using NServiceBus.Persistence;
using NServiceBus.Persistence.Legacy;

class Program
{
    static void Main()
    {
        BusConfiguration busConfig = new BusConfiguration();
        busConfig.EndpointName("Samples.Versioning.V1Subscriber");
        busConfig.UseSerialization<JsonSerializer>();
        busConfig.UsePersistence<InMemoryPersistence>();
        busConfig.UsePersistence<MsmqPersistence>()
            .For(Storage.Subscriptions);
        busConfig.EnableInstallers();

        using (IStartableBus bus = Bus.Create(busConfig))
        {
            bus.Start();
            Console.WriteLine("\r\nPress any key to stop program\r\n");
            Console.ReadKey();
        }
    }
}