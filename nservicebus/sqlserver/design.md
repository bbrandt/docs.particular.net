---
title: SQL Server Transport
summary: The design of SQL Server transport
tags:
- SQL Server
---

The SQL Server transport implements a message queueing mechanism on top of a Microsoft SQL Server database. It uses tables to model queues. It does not make any use of ServiceBroker, a messaging technology built into the SQL Server, mainly due to cumbersome API and difficult maintenance. 

## How it works?

The SQL Server transport is a hybrid queueing system which is neither store-and-forward (like MSMQ) nor a broker (like RabbitMQ). It treats the SQL Server only as a storage infrastructure for the queues. The queueing logic itself is implemented and executed as part of the transport code running in an NServiceBus endpoint. 

## Pros & cons

### Pros

 * Nearly all Microsoft stack organizations already have SQL Server installed (no license cost) and have knowledge required to run it (no training cost)
 * Most developers are familiar with SQL Server
 * Tooling (SSMS) is great
 * Maximum throughput for any given endpoint is on par with MSMQ
 * Queues support competing consumers (multiple instances of same endpoint feeding off of same queue) so there is no need for distributor in order to scale out the processing

### Cons

 * No local store-and-forward mechanism meaning if SQL Server instance is down, endpoint cannot send nor receive messages
 * In centralized deployment maximum throughput applies for whole system, not individual endpoints, so if SQL Server on a given hardware can handle 2000 msg/s, each one of 10 endpoints can only receive 200 msg/s (on average).

## Single database

In the simplest form, SQL Server transport uses a single instance of the SQL Server to maintain all the queues for all endpoints of a system. In order to send a message, an endpoint needs to connect to the (usually remote) database server and execute a SQL command. The message is delivered directly to the destination queue without any store-and-forward mechanism. Such a simplistic approach can only be only used for small-to-medium size systems because of the need to store everything in a single database. Using schemas to distingiush logical data stores might be a good compromise for mid-size projects. The upside is it does not require Distributed Transaction Coordinator (MS DTC). Another advantage is the ability to take a snapshot of entire system state (all the queues) by backing up a database. This is even more useful if the business data are stored in the same database.

## Database-per-endpoint

In a more complex scenario endpoints use separate databases and DTC is involved. Due to lack of store-and-forward mechanism, if a remote endpoint's database or DTC infrastructure is down, our endpoint cannot send messages to it. This potentially renders our endpoint (and all endpoints transitively depending on it) unavailable. 

## Database-per-endpoint with store-and-forward

In order to overcome this limitation a higher level store-and-forward mechanism needs to be used. The Outbox feature can be used to effectively implement a distributed decoupled architecture where:
 * Each endpoint has its own database where it stores both the queues and the user data
 * Messages are not sent immediately when calling `Bus.Send()` but are added to the *outbox* that is stored in the endpoint's own database. After completing the handling logic the messages in the *outbox* are forwarded to their destination databases
 * Should one of the forward operations fail, it will be retried by means of standard NServiceBus retry mechanism (both first-level and second-level). This might result in some messages being sent more than once but it is not a problem because the outbox automatically handles the deduplication of incoming messages based on their ID.

#### Further reading

 * [More on usage scenarios](usage.md)
 * [Multi database support](multiple-databases.md)
 * [Table-based queue implementation](configuration.md)
 * [Concurrency model](concurrency.md)

#### Samples

 * [SQL Server / NHibernate](../../samples/sqltransport-nhpersistence)
 * [SQL Server / NHibernate / Outbox](../../samples/sqltransport-nhpersistence-outbox)
 * [SQL Server / NHibernate / EntityFramework / Outbox](../../samples/sqltransport-nhpersistence-outbox-ef)
