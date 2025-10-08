# WOMS - .NET 9 Web API with Clean Architecture & CQRS

A complete .NET 9 Web API solution implementing Clean Architecture principles with CQRS (Command Query Responsibility Segregation) pattern.

## 🏗️ Architecture

This solution follows Clean Architecture with the following layers:

```
src/
├── WOMS.Api              → Presentation layer (Controllers, Middleware, DI, Swagger)
├── WOMS.Application      → CQRS logic (Commands, Queries, DTOs, Validators, Handlers, Interfaces)
├── WOMS.Domain           → Core domain (Entities, Enums, ValueObjects, Domain Events)
└── WOMS.Infrastructure   → EF Core, Repositories, Persistence, External Services

tests/
├── WOMS.UnitTests        → Unit tests
└── WOMS.IntegrationTests → Integration tests
```

## 🚀 Features

- **Clean Architecture**: Separation of concerns with clear layer boundaries
- **CQRS Pattern**: Commands and Queries with MediatR
- **Entity Framework Core**: Code First approach with SQL Server
- **AutoMapper**: Object-to-object mapping
- **FluentValidation**: Input validation
- **Serilog**: Structured logging
- **Swagger/OpenAPI**: API documentation
- **Global Exception Handling**: Centralized error handling
- **Unit & Integration Tests**: Comprehensive test coverage

## 📦 Core Packages

- **MediatR** (13.0.0) - CQRS mediator
- **FluentValidation** (12.0.0) - Validation
- **AutoMapper** (12.0.1) - Object mapping
- **Microsoft.EntityFrameworkCore.SqlServer** (9.0.9) - Database
- **Serilog** (4.3.0) - Logging
- **Swashbuckle.AspNetCore** (9.0.6) - Swagger
- **xUnit, Moq, FluentAssertions** - Testing

## 🗄️ Database

The solution uses SQL Server with Entity Framework Core Code First approach. The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WOMS;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

## 🎯 User Feature

The solution includes a complete **Users** feature demonstrating CQRS:

### Entity
- **User**: Id, FirstName, LastName, Email, CreatedAt

### Commands
- **CreateUserCommand**: Creates a new user

### Queries
- **GetUserByIdQuery**: Returns user details by Id

### DTOs
- **UserDto**: Id, FullName, Email, CreatedAt
- **CreateUserDto**: FirstName, LastName, Email

### Validation
- Email is required and must be valid
- FirstName and LastName are required
- Email must be unique

## 🔧 Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Running the Application

1. **Clone and navigate to the solution**:
   ```bash
   cd C:\Projects\WOMS
   ```

2. **Restore packages**:
   ```bash
   dotnet restore
   ```

3. **Build the solution**:
   ```bash
   dotnet build
   ```

4. **Update the database** (optional - will be created automatically):
   ```bash
   cd src\WOMS.Infrastructure
   dotnet ef database update --startup-project ..\WOMS.Api\WOMS.Api.csproj
   ```

5. **Run the API**:
   ```bash
   cd src\WOMS.Api
   dotnet run
   ```

6. **Access Swagger UI**:
   - Navigate to `https://localhost:7000/swagger`
   - Or `http://localhost:5000/swagger` for HTTP

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/WOMS.UnitTests
dotnet test tests/WOMS.IntegrationTests
```

## 📡 API Endpoints

### Users

- **POST** `/api/users` - Create a new user
- **GET** `/api/users/{id}` - Get user by ID

### Example Requests

**Create User**:
```http
POST /api/users
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com"
}
```

**Get User**:
```http
GET /api/users/{id}
```

## 🏛️ Clean Architecture Principles

1. **Dependency Inversion**: High-level modules don't depend on low-level modules
2. **Separation of Concerns**: Each layer has a single responsibility
3. **Testability**: Easy to unit test with dependency injection
4. **Maintainability**: Clear boundaries and interfaces
5. **Scalability**: Easy to add new features and modify existing ones

## 🔄 CQRS Implementation

- **Commands**: Handle write operations (Create, Update, Delete)
- **Queries**: Handle read operations (Get, List, Search)
- **Handlers**: Process commands and queries
- **MediatR**: Mediates between controllers and handlers
- **Validation**: FluentValidation for input validation
- **Pipeline Behaviors**: Cross-cutting concerns like validation

## 📝 Logging

The application uses Serilog for structured logging with:
- Console output
- File logging (in `logs/` directory)
- Configurable log levels
- Request/response logging

## 🧪 Testing

- **Unit Tests**: Test individual components in isolation
- **Integration Tests**: Test the complete API endpoints
- **Mocking**: Moq for mocking dependencies
- **Assertions**: FluentAssertions for readable test assertions

## 🚀 Production Considerations

- Configure proper connection strings for production
- Set up proper logging configuration
- Implement authentication and authorization
- Add health checks
- Configure CORS policies
- Set up monitoring and alerting
- Implement caching strategies
- Add rate limiting

## 📚 Additional Resources

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [MediatR](https://github.com/jbogard/MediatR)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/)
- [FluentValidation](https://fluentvalidation.net/)
- [Serilog](https://serilog.net/)

---

**Note**: This solution is a complete, production-ready template that can be extended with additional features and business logic as needed.
