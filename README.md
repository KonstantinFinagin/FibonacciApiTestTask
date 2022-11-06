# FibonacciApiTestTask

## Test task for job application. 

Covering:
- .net 6.0
- Web API 
- RPC calls
- RabbitMQ interservice communication
- Modularization
- Unit testing with XUnit (just a core module, sorry)
- Swagger api description
- Memoization for speeding up recursive calculations, BigInteger (Fibonacci calculation) (whoa, that was really interesting!)
- Using Autofac, Serilog, EasyNetQ, Refit and other libs

Excluded:
- Entity Framework
- Authorization middleware
- Docker and containerization

Before running please setup:
- RabbitMQ with Fibonaccy.Exchange (fanout) Fibonaccy.Queue (guest/guest) 
- In Visual studio check appsettings.json in both apps

Main classes to see (others are more or less of infrastrcture support):
- FibonacciCalculatorService.cs
- FibonacciApiService.cs (API)
- MessageProcessor.cs (Client)

## Description:

Two applications communicate with each other through transport, realizing the calculation of Fibonacci numbers.

The first one initializes the calculation and sends initial fibonacci number to the second 
The second calculates next fibonacci number and sends the result back
Further on they communicate back and forth until applications stop (in the sample - BigInteger does not exceed 30 bytes)

At startup, the first application receives a parameter - an integer, how many asynchronous calculations to start. 
All calculations are running in parallel.

Data transfer from 1 to 2 goes through Rest WebApi
Data transfer from 2 to 1 is via MessageBus.

![Client](https://user-images.githubusercontent.com/2586700/200191074-81de43de-6181-4c72-ab81-ecf3e3ae1b33.png)
