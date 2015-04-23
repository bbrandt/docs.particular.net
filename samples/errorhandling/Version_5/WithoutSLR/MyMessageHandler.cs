﻿using System;
using System.Collections.Concurrent;
using NServiceBus;
#region Handler
public class MyMessageHandler : IHandleMessages<MyMessage>
{
    static ConcurrentDictionary<Guid, string> Last = new ConcurrentDictionary<Guid, string>();

    public IBus Bus { get; set; }

    public void Handle(MyMessage message)
    {
        IMessageContext context = Bus.CurrentMessageContext;
        Console.WriteLine("ReplyToAddress: {0} MessageId:{1}", context.ReplyToAddress, context.Id);
        
        string numOfRetries;
        if (context.Headers.TryGetValue(Headers.Retries, out numOfRetries))
        {
            string value;
            Last.TryGetValue(message.Id, out value);

            if (numOfRetries != value)
            {
                Console.WriteLine("This is second level retry number {0}", numOfRetries);
                Last.AddOrUpdate(message.Id, numOfRetries, (key, oldValue) => numOfRetries);
            }
        }

        throw new Exception("An exception occurred in the handler.");
    }
}
#endregion