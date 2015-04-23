---
title: Centralizing Unobtrusive Declarations
summary: Don't repeat yourself when writing unobtrusive declarations
tags:
- Unobtrusive
redirects:
- nservicebus/how-do-i-centralize-all-unobtrusive-declarations
---

When working with NServiceBus in unobtrusive mode you may feel that you are repeating the conventions over and over again on all the endpoints. 

Since message conventions are not usually dynamic in nature, one way to centralize these conventions would be to define these at configuration time.

Another option is to configure conventions before the bus starts.

<!-- import UnobtrusiveConventions -->

For more details on this approach, refer to the [Unobtrusive Sample](/samples/unobtrusive/).

