---
title: RabbitMQ connection strings
summary: Detailed connection string information for RabbitMQ.
tags:
- Connection strings
- Transports
---

RabbitMQ uses the [AMQP URI Specification](https://www.rabbitmq.com/uri-spec.html)

```xml
<connectionStrings>
   <!-- RabbitMQ -->
   <add name="NServiceBus/Transport"
        connectionString="host=localhost"/>
</connectionStrings>
```

### Remote Host

For remote host provide username and password because remote hosts don't accept default guest credentials

```xml
<connectionStrings>
   <!-- RabbitMQ -->
   <add name="NServiceBus/Transport"
        connectionString="host=myremoteserver;
                          username=myusername;
                          password=mypassowrd"/>
</connectionStrings>
```

### For clustered 

```xml
<connectionStrings>
   <!-- RabbitMQ -->
   <add name="NServiceBus/Transport" 
			connectionString="host=rabbitNode1,rabbitNode2,rabbitNode3;
				username=myuser;
				password=password" />
</connectionStrings>
```
