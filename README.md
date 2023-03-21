UserManagementMicroservice/
│
├── src/
│   ├── UserManagement.API/
│   │   ├── Controllers/
│   │   ├── Migrations/
│   │   ├── Properties/
│   │   ├── appsettings.json
│   │   └── Program.cs
│   │
│   ├── UserManagement.Application/
│   │   ├── Contracts/
│   │   ├── DTOs/
│   │   ├── Exceptions/
│   │   ├── Interfaces/
│   │   └── Services/
│   │
│   ├── UserManagement.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── Events/
│   │   ├── Exceptions/
│   │   ├── Specifications/
│   │   └── ValueObjects/
│   │
│   └── UserManagement.Infrastructure/
│       ├── Configuration/
│       ├── Identity/
│       ├── Mappings/
│       ├── Persistence/
│       ├── Repositories/
│       └── Services/
│
├── tests/
│   ├── UserManagement.API.Tests/
│   ├── UserManagement.Application.Tests/
│   ├── UserManagement.Domain.Tests/
│   └── UserManagement.Infrastructure.Tests/
│
├── .gitignore
├── docker-compose.yml
├── Dockerfile
└── README.md