# Restaurant Service App

Restaurant Service App is a .NET 8 console application for managing restaurant menu items and customer orders. The solution uses a layered architecture with Entity Framework Core for SQL Server persistence, a business layer for application rules, and a console presentation layer for the interactive workflow.

> Note: the project and namespace names use `Restourant` in the codebase.

## Features

### Menu Management

- Add, edit, and remove menu items
- List all menu items
- Filter menu items by category
- Filter menu items by price range
- Search menu items by name

### Order Management

- Create orders from selected menu items
- Cancel existing orders
- List all orders
- Filter orders by date
- Filter orders by date interval
- Filter orders by total price interval
- View selected order details

## Solution Structure

```text
Restourant Service App/
|-- Restourant Service App.sln
|-- RestourantServiceApp.Core/
|-- RestourantServiceApp.DataAccsessLayer/
|-- RestourantServiceApp.BLogicLayer/
|-- RestourantServiceApp.PL/
`-- RestourantServiceAppBll.Test/
```

## Projects

| Project | Responsibility |
| --- | --- |
| `RestourantServiceApp.Core` | Domain models, base entities, and enums such as `MenuItem`, `Order`, `OrderItem`, and `Category`. |
| `RestourantServiceApp.DataAccsessLayer` | EF Core `DbContext`, repository abstractions, entity configurations, seed data, and migrations. |
| `RestourantServiceApp.BLogicLayer` | Menu and order services, DTOs, AutoMapper profiles, custom exceptions, and cached repository support. |
| `RestourantServiceApp.PL` | Console application entry point and interactive user flow. |
| `RestourantServiceAppBll.Test` | xUnit tests for the business layer using Moq and MockQueryable.Moq. |

## Tech Stack

- .NET 8
- C#
- Entity Framework Core
- SQL Server
- AutoMapper
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Caching.Memory
- xUnit, Moq, MockQueryable.Moq

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server, SQL Server Express, or LocalDB
- `dotnet-ef` for applying migrations

Install `dotnet-ef` if it is not already available:

```bash
dotnet tool install --global dotnet-ef
```

## Configuration

The database connection string is currently configured directly in:

```text
RestourantServiceApp.DataAccsessLayer/Contexts/RestourantDbContext.cs
```

Default connection string:

```csharp
Data Source=.\MSSQLSERVER01;Initial Catalog=RestourantServiceDb;Integrated Security=True;Trust Server Certificate=True;
```

Update this value if your SQL Server instance, authentication method, or database name is different.

## Getting Started

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update --project RestourantServiceApp.DataAccsessLayer --startup-project RestourantServiceApp.PL
```

Build the solution:

```bash
dotnet build
```

Run the console app:

```bash
dotnet run --project RestourantServiceApp.PL
```

## Testing

Run the test suite from the solution root:

```bash
dotnet test
```

## Domain Overview

| Entity | Description |
| --- | --- |
| `MenuItem` | Represents a restaurant menu item with a name, price, and category. |
| `Order` | Represents a customer order with an order date, total amount, and order items. |
| `OrderItem` | Represents a menu item inside an order, including the selected quantity. |

## Development Notes

- Keep migrations in `RestourantServiceApp.DataAccsessLayer/Migrations`.
- Keep business validation inside the business layer services.
- Use DTOs when data crosses the business layer boundary.
- Add or update tests in `RestourantServiceAppBll.Test` when service behavior changes.
