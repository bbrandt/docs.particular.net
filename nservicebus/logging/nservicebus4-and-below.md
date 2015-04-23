---
title: Logging in NServiceBus 4 and below
summary: Logging in NServiceBus 4 and below
tags: 
- Logging
redirects:
- nservicebus/logging-in-nservicebus4-and-below
---

INFO: This is relevant to versions 4 and below. For newer versions, see [Logging in NServiceBus](./).

## Logging basics

Start to log with NServiceBus:

    NServiceBus.Configure.With().Log4Net();

WARNING: In Version 4 only the Host has logging enable by default. In any other hosting scenario (eg self hosting or hosting in a website) you are required to enable and configure logging.  

This makes use of `ConsoleAppender`, which sets the logging threshold to Debug. All logging statements performed by NServiceBus or the application at a level at or above Debug (i.e., Warn, Error, and Fatal) are sent to the console for output.

To make use of the standard Log4Net configuration found in the application configuration file, make the following call before the call to `NServiceBus.Configure.With()`:

    NServiceBus.SetLoggingLibrary.Log4Net(log4net.Config.XmlConfigurator.Configure);

NOTE: When using `XmlConfigurator` you need to make sure you project references the `log4net` library. If not, run

```
Install-Package log4net -version 1.2.10
```

in the package management console.

Include a Log4Net configuration section in the application configuration file that results in the Debug threshold with the `ConsoleAppender`, as shown:

```
<log4net debug="false">
	<appender name="console" type="log4net.Appender.ConsoleAppender">
		<layout type="log4net.Layout.PatternLayout">
		<param name="ConversionPattern" value="%d [%t] %-5p %c [%x] <%X{auth}> - %m%n"/>
		</layout>
	</appender>
	<root>
		<level value="DEBUG"/>
		<appender-ref ref="console"/>
	</root>
</log4net>
```

For more information about standard Log4Net functionality, see the [Log4Net home page](http://logging.apache.org/log4net/index.html).

## Customized logging

You can tell NServiceBus to use any of the built-in Log4Net appenders, specifying additional properties of the chosen appender using the following API:

    NServiceBus.Configure.With().Log4Net(a => a.From = "no-reply@YourApp.YourCompany.com");

This example shows all logging calls sent using SMTP from the email address `no-reply@YourApp.YourCompany.com`.

If there isn't a built-in appender for the technology you want to use for logging, write a class that inherits from `AppenderSkeleton`, as follows:

    public class YourAppender : log4net.Appender.AppenderSkeleton
    {
	    public string YourProperty { get; set; }
	    protected override void Append(LoggingEvent loggingEvent)
	    {
	  	  //call your logging technology here
	    }
    }

Then plug your appender into NServiceBus like this:

    NServiceBus.Configure.With().Log4Net(a => a.YourProperty = "value");

## Logging Threshold

The logging threshold can be controlled using the following in you `app.config.

```
<configSections>
	<section name="Logging" type="NServiceBus.Config.Logging, NServiceBus.Core" />
</configSections>
<Logging Threshold="WARN" />
```

The 'Threshold' value attribute of the 'Logging' element can be any of the standard Log4Net entries: ALERT, ALL, CRITICAL, DEBUG, EMERGENCY, ERROR, FATAL, FINE, FINER, FINEST, INFO, NOTICE, OFF, SEVERE, TRACE, VERBOSE, and WARN. Make sure you use all caps for these entries.

NOTE: If you set this value in code, the configuration value is ignored.

The production profile only logs to a file, unless you are running within Visual Studio. See [Profiles](/nservicebus/hosting/nservicebus-host/profiles.md) for more detail.