using NServiceBus;

public class EndpointConfig : 
    IConfigureThisEndpoint, 
    AsA_Server, 
    IWantCustomInitialization
{
    public void Init()
    {
        Configure.Serialization.Json();
        Configure configure = Configure.With();
        configure.DefineEndpointName("Samples.Logging.HostDefault");
        configure.DefaultBuilder();
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
    }
}