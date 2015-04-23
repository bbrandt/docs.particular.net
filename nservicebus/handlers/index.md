---
title: Handling a Message
summary: Write a class to handle messages in NServiceBus.
tags: []
redirects:
- nservicebus/how-do-i-handle-a-message
---

To handle a message, write a class that implements `IMessageHandler<T>` where `T` is the message type:

```C#
public class H1 : IMessageHandler<MyMessage>
{
     public void Handle(MyMessage message)
     {
        //do something relevant with the message
     }
}
```

To handle messages of all types:

1.  Set up the [unobtrusive message configuration](/nservicebus/messaging/unobtrusive-mode.md) to designate which classes are messages. This example uses a namespace match.
2.  Create a handler of type `Object`. This handler will be executed for all messages that are delivered to the queue for this endpoint.

Since this class is setup to handle type `Object`, every message arriving in the queue will trigger it.

```C#
public class GenericMessageHandler : IHandleMessages<Object>
{
    static ILog Logger = LogManager.GetLogger(typeof(GenericMessageHandler));
    
    public void Handle(Object message)
    { 
        Logger.Info(string.Format("I just received a message of type {0}.", message.GetType().Name));
        Console.WriteLine("*********************************************************************************");
    }
}
```

If you are using the Request-Response or Full Duplex pattern, your handler will probably do the work it needs to do, such as updating a database or calling a web service, then creating and sending a response message. See [How to Reply to a Message](/nservicebus/messaging/reply-to-a-message.md).

If you are handling a message in a publish-and-subscribe scenario, see [How to Publish/Subscribe to a Message](/nservicebus/messaging/publish-subscribe/).

## What happens when there are no handlers for a message?

Receiving a message for which there is no message handlers is considered an error and the received message will be forwarded to the configured error queue. 

Note that this behavior was slightly different in version 3 where the message would only end up in the error queue if running in debug mode. If not in debug mode a version 3 endpoint would log a warning but still consider the message successfully processed and therefore moving it to the configured error queue.
