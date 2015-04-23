---
title: Exception Caveats
summary: Certain types of exceptions cannot be handled nativity by NServiceBus.
tags:
- Exceptions
- Error Handling
---

Certain types of exceptions are special in their behavior and may require custom handling. 

#### AccessViolationException

If an [AccessViolationException](https://msdn.microsoft.com/en-us/library/system.accessviolationexception.aspx) is thrown then the endpoint will terminate. The reason is that a standard `try catch`, which NServiceBus uses does not catch an  `AccessViolationException` as such it will bubble out of he handler and terminate the endpoint.

While you can explicitly handle these exceptions (using a [HandleProcessCorruptedStateExceptionsAttribute](https://msdn.microsoft.com/en-us/library/system.runtime.exceptionservices.handleprocesscorruptedstateexceptionsattribute.aspx)) it is explicitly recommended MS not to do it. 

> Corrupted process state exceptions are exceptions that indicate that the state of a process has been corrupted. We do not recommend executing your application in this state.

For more information see [Handling Corrupted State Exceptions](https://msdn.microsoft.com/en-us/magazine/dd419661.aspx#id0070035)
 
#### StackOverflowException

NServiceBus can't handle [StackOverflowExceptions](https://msdn.microsoft.com/en-us/library/system.stackoverflowexception.aspx) since .net does not allow it.

> A StackOverflowException object cannot be caught by a try-catch block and the corresponding process is terminated by default. Consequently, users are advised to write their code to detect and prevent a stack overflow. For example, if your application depends on recursion, use a counter or a state condition to terminate the recursive loop. Note that an application that hosts the common language runtime (CLR) can specify that the CLR unload the application domain where the stack overflow exception occurs and let the corresponding process continue.

#### OutOfMemoryException

While [OutOfMemoryException](https://msdn.microsoft.com/en-us/library/system.outofmemoryexception.aspx) will be caught by NServiceBus and handled in the standard NServiceBus manner, there is no guarantee that there will be enough memory available to handle the exception appropriately. 
