﻿using System;
using System.Threading;
using NServiceBus;

public class CriticalError
{
    public void DefineCriticalErrorAction()
    {

        #region DefineCriticalErrorAction

        // Configuring how NServicebus handles critical errors
        Configure.With().DefineCriticalErrorAction((message, exception) =>
        {
            string output = string.Format("We got a critical exception: '{0}'\r\n{1}", message, exception);
            Console.WriteLine(output);
            // Perhaps end the process??
        });

        #endregion
    }

    public void RaiseCriticalErrorAction()
    {
        Exception theException = null;
        #region RaiseCriticalError

        // Configuring how NServicebus handles critical errors
        Configure.With().RaiseCriticalError("The message", theException);

        #endregion
    }

    public void DefineCriticalErrorActionForAzureHost()
    {

        #region DefineCriticalErrorActionForAzureHost

        
        Configure.With().DefineCriticalErrorAction((message, exception) =>
        {
            string errorMessage = string.Format("We got a critical exception: '{0}'\r\n{1}", message, exception);

            if (Environment.UserInteractive)
            {
                Thread.Sleep(10000); // so that user can see on their screen the problem
            }
            
            Environment.FailFast(String.Format("The following critical error was encountered by NServiceBus:\n{0}\nNServiceBus is shutting down.", errorMessage), exception);
        });

        #endregion
    }
}