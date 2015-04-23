﻿using NLog;
using NLog.Config;
using NLog.Targets;
using NServiceBus;

public class NLogConfig
{
    public void InCode()
    {
        #region NLogInCode

        LoggingConfiguration config = new LoggingConfiguration();

        ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget
        {
            Layout = "${level}|${logger}|${message}${onexception:${newline}${exception:format=tostring}}"
        };
        config.AddTarget("console", consoleTarget);
        config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, consoleTarget));

        LogManager.Configuration = config;

        NServiceBus.Logging.LogManager.Use<NLogFactory>();

        #endregion
    }
}