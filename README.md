
# IntegrationTest Project

Welcome to my project that I use for playing with technologies that I would like to explorer them more. 
Feel free to download the project and run in your enverionment.

## Requirements for running in your enverionment
- [.NET 5](https://dotnet.microsoft.com/download/dotnet)
- [Visual Studio](https://visualstudio.microsoft.com/vs/community/) or [Visual Studio Code](https://code.visualstudio.com/)

**Optional**

- See the code with an [online code editor](https://github1s.com/MarcosCostaDev/IntegrationTest)


## Techonologies used in this project so far

- .NET 5
- ASP.NET WEB API 
- Swagger/Swagger UI
- Hangfire
- MediatR
- AutoMapper
- SQLite

**Unit Tests / Integration Tests**

- XUnit

## Design Patterns 

- Domain Driven Design (DDD)
- Repository Pattern
- Message Bus
- Fluent Validation

## Strategies

- Command Query Responsability Segregation (CQRS)  


## After Run

**URLS**

- Hangfire: https://localhost:44393/hangfire
- Swagger UI: https://localhost:44393/swagger/index.html


## Expected Behaviors 

Two background queue are created. They are named `generate_fake_products` and `generate_fake_invoices`
- **generate_fake_products**: Products are generate automatically in every 30 seconds
- **generate_fake_invoices**: Invoices are generate automatically in every 1 seconds
- An event is trigger for every product created.
- An event is trigger for every invoice created.



<!-- 
# Migration Database
- IntegrationTest.Infra
dotnet ef migrations add IdentityCreate --context MyDbContext --project IntegrationTest.Infra

# Update Database

- IntegrationTest.Infra
dotnet ef database update --context MyDbContext --project IntegrationTest.Infra
-->