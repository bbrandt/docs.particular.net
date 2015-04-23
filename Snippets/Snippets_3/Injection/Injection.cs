﻿namespace MyServer.Injection
{
    using System.Net.Mail;
    using NServiceBus;

    public class Injection
    {
        public void ConfigurePropertyInjectionForHandler()
        {
            #region ConfigurePropertyInjectionForHandlerBefore

            Configure.With()
                .DefaultBuilder()
                .Configurer
                    .ConfigureProperty<EmailHandler>(handler => handler.SmtpAddress, "10.0.1.233")
                    .ConfigureProperty<EmailHandler>(handler => handler.SmtpPort, 25);

            #endregion
        }

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
    }

    public class EmailMessage
    {
    }
}