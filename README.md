# NetGoodPractices

This repository demonstrates best practices in .NET, focusing on:

- **Multithreading**: Efficiently managing concurrent operations.
- **Session Management**: Proper handling of user sessions in web applications.

## Repository Structure

The repository contains the following projects:

1. **AsyncTest**
   - *Description*: Showcases proper and improper handling of asynchronous operations to prevent issues like deadlocks.
   - *Key Features*:
     - Examples of using `async` and `await` correctly.
     - Demonstrations of common pitfalls, such as using `.Result` or `.Wait()` leading to deadlocks.

2. **ContextItemsExample**
   - *Description*: Illustrates the use of `HttpContext.Items` for storing data per request, ensuring thread safety.
   - *Key Features*:
     - Middleware implementation accessing `HttpContext.Items`.
     - Examples of setting and retrieving items within the request pipeline.

3. **SessionStorageExample**
   - *Description*: Demonstrates managing session data appropriately in ASP.NET Core applications.
   - *Key Features*:
     - Configuring session middleware.
     - Storing and retrieving session data securely.
