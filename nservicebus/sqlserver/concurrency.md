---
title: Concurrency
summary: How concurrency is controlled inside the SQLServer transport.
tags:
- SQL Server
- Concurrency
- Threading
---

### Prior to 2.0

Prior to 2.0 there was no support for callbacks in the SQLServer transport. There is a single queue table for each logical endpoint, no matter how many actual instances of the endpoint were running. Each endpoint running SQLServer transport spins up a fixed number of threads (controlled by `MaximumConcurrencyLevel` property of `TransportConfig` section). Each thread runs a loop, polling the database for messages awaiting processing.

The disadvantage of this simple model is the fact that satellites (e.g. Second-Level Retries, Timeout Manager) share the same concurrency settings but usually have much lower throughput requirements. If both SLR and TM are enabled, setting `MaximumConcurrencyLevel` to 10 results in 40 threads in total, each polling the database even if there are no messages to be processed.

### 2.0

In released 2.0 we added support for callbacks. Callbacks are implemented by each endpoint instance having a unique [secondary queue](configuration.md#secondary-queues). The receive for the secondary queue does not use the `MaximumConcurrencyLevel` and defaults to 1 thread. This value can be adjusted via the config API.

### 2.1

In order to address the problem with excess number of unused polling threads, in version 2.1 we introduced an adaptive concurrency model. The transport adapts the number of polling threads based on the rate of messages coming in. The key concept in this new model is the *ramp up controller* which controls the ramping up of new threads and decommissioning of unnecessary threads. It uses the following algorithm:
 * if last receive operation yielded a message, it increments the *consecutive successes* counter and resets the *consecutive failures* counter
 * if last receive operation yielded no message, it increments the *consecutive failures* counter and resets the *consecutive successes* counter
 * if *consecutive successes* counter goes over a certain threshold and there is less polling threads than `MaximumConcurrencyLevel`, it starts a new polling thread and resets the *consecutive successes* counter
 * if *consecutive failures* counter goes over a certain threshold and there is more than one polling thread it kills one of the polling threads
