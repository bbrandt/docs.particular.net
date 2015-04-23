﻿namespace MyServer.Injection
{
    using System.Net.Mail;
    using NServiceBus;

    public class Injection
    {
        public void ConfigurePropertyInjectionForHandler()
        {
            BusConfiguration busConfiguration = new BusConfiguration();

            #region ConfigurePropertyInjectionForHandler 5.2

            busConfiguration.InitializeHandlerProperty<EmailHandler>("SmtpAddress", "10.0.1.233");
            busConfiguration.InitializeHandlerProperty<EmailHandler>("SmtpPort", 25);

            #endregion
        }

        #region PropertyInjectionWithHandler 5.2

        public class EmailHandler : IHandleMessages<EmailMessage>
        {
            public string SmtpAddress { get; set; }
            public int SmtpPort { get; set; }

            public void Handle(EmailMessage message)
            {
                SmtpClient client = new SmtpClient(SmtpAddress, SmtpPort);
                // ...
            }
        }

        #endregion
    }

    public class EmailMessage
    {
    }
}