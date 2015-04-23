---
title: Encryption
summary: How to encrypt message data.
tags:
- Encryption
redirects:
- nservicebus/encryption-sample
---

## Run the solution.

You will see two console applications start up.

### Endpoint1 

Which outputs

```
MessageWithSecretData sent. 
```

### Endpoint2 

Which outputs

```
I know your secret - it's 'betcha can't guess my secret'
SubSecret: My sub secret
CreditCard: 312312312312312 is valid to 3/11/2015 5:21:59 AM
CreditCard: 543645546546456 is valid to 3/11/2016 5:21:59 AM
```

## Code walk-through

### The message contract

Starting with the Shared project, open the `MessageWithSecretData.cs` file and look at the following code:

<!-- import Message -->

### How is encryption configured. 

Open either one of the `Program.cs`. You will notice the line 

    busConfiguration.RijndaelEncryptionService();

This code indicates that encryption should be enabled.

The key is then configured using 

<!-- import ConfigureEncryption --> 


### The message on the wire

Now run `Endpoint1` on its own (i.e. don't start `Endpoint2`).

Go to the server queue (called `EncryptionSampleEndpoint1`) and examine the message in it. Read how to do this in the
[FAQ](/nservicebus/msmq/viewing-message-content-in-msmq.md).

Your message will look like this:

```XML
<?xml version="1.0" ?>
<Messages xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
          xmlns="http://tempuri.net/Messages">
  <MessageWithSecretData>
    <Secret>
      <EncryptedValue>
         <EncryptedBase64Value>+eeBont5Lzlre4cxDi8QT/M6EbAGxTerniqywbpLBVA=</EncryptedBase64Value>
         <Base64Iv>u8n8ds0Ssf/AdJCxpOG7AQ==</Base64Iv>
      </EncryptedValue>
    </Secret>
  </MessageWithSecretData>
</Messages>
```

See also [Encryption](/nservicebus/security/encryption.md)