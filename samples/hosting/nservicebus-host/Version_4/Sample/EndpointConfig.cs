using NServiceBus;
using NServiceBus.Features;

#region nservicebus-host

public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
{
    public void Init()
    {
        Configure.Serialization.Json();
        Configure.Features.Enable<Sagas>();

        Configure configure = Configure.With();

        configure.DefineEndpointName("Samples.NServiceBus.Host");
        configure.Log4Net();
        configure.DefaultBuilder();
        configure.InMemorySagaPersister();
        configure.UseInMemoryTimeoutPersister();
        configure.InMemorySubscriptionStorage();
        configure.UseTransport<Msmq>();
    }
}

#endregion