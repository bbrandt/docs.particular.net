﻿using System;
using Messages;
using NServiceBus.Saga;

namespace Receiver
{
    public class OrderLifecycleSaga : Saga<OrderLifecycleSagaData>, 
        IAmStartedByMessages<OrderSubmitted>,
        IHandleTimeouts<OrderTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderLifecycleSagaData> mapper)
        {
        }

        public void Handle(OrderSubmitted message)
        {
            Data.OrderId = message.OrderId;

            #region Timeout
            RequestTimeout<OrderTimeout>(TimeSpan.FromSeconds(5));
            #endregion
        }

        public void Timeout(OrderTimeout state)
        {
            Console.WriteLine("Got timeout");
        }
    }

    public class OrderTimeout
    {
    }
}