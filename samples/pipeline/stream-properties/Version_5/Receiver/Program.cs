using System;
using NServiceBus;

class Program
{

    static void Main()
    {
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Sample.PipelineStream.Receiver");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.SetStreamStorageLocation("..\\..\\..\\storage");
        busConfiguration.EnableInstallers();
        using (IStartableBus bus = Bus.Create(busConfiguration))
        {
            bus.Start();
            Console.WriteLine("\r\nPress enter key to stop program\r\n");
            Console.Read();
        }
    }
}