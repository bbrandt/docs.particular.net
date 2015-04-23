---
title: Message Handling Pipeline
summary: Overview of the message handling pipeline 
tags:
- Pipeline
redirects:
- nservicebus/nservicebus-pipeline-intro
---

NServiceBus has the concept of a "pipeline" which refers to the series of actions taken when an incoming message is process and an outgoing message is sent.

### Customizing the pipeline 

There are several ways to customize this pipeline with varying levels of complexity. 

 * [Custom behaviors](/nservicebus/pipeline/customising.md)
 * [Message Mutators](/nservicebus/pipeline/message-mutators.md)

### Features build on the pipeline

 * [DataBus](/nservicebus/messaging/databus.md)
 * [Encryption](/nservicebus/security/encryption.md)
 * [Second Level Retries](/nservicebus/errors/second-level-retries.md)
   
