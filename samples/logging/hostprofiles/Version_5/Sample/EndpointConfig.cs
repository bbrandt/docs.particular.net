using NServiceBus;

public class EndpointConfig : 
    IConfigureThisEndpoint, 
    AsA_Server
{
    public void Customize(BusConfiguration busConfiguration)
    {
        busConfiguration.EndpointName("Samples.Logging.HostProfiles");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.EnableInstallers();
        busConfiguration.UsePersistence<InMemoryPersistence>();
    }
}