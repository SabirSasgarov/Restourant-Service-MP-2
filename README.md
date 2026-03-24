# Restaurant Service App

A monolithic C# .NET 8 console application for managing restaurant menu items, customer orders, and tracking order history.

## Architecture

This project strictly follows an N-Tier architecture to separate concerns and improve maintainability:

1. **RestourantServiceApp.Core**: Contains the core domain models (`MenuItem`, `Order`, `OrderItem`), base entities, and Enumerations (`Category`). No outer dependencies.
2. **RestourantServiceApp.DataAccsessLayer**: Integrates Entity Framework Core 8 (`EF Core`), acts as the repository layer containing `RestourantDbContext`, complex `IEntityTypeConfiguration` definitions, model seeding, and Code-First Migrations.
3. **RestourantServiceApp.BLogicLayer**: Houses the core business logic through Service classes (`MenuItemService`, `OrderService`). Provides APIs for querying and mutating application state.
4. **RestourantServiceApp.PL**: The Presentation Layer (Console entry point, `Program.cs`) providing an interactive terminal UI for the restaurant staff to manage operations.

## Domain Models

- **MenuItem**: Items available on the menu, featuring unique names, decimal pricing (`Decimal(18,2)`), and categorized by an enum (e.g., Pizza, Burger, Option, Salad).
- **Order**: Represents a customer's order. Tracks an auto-generated Date and the Total Amount for the entire check.
- **OrderItem**: The mapping/join table for the Many-to-Many relationship between `Order` and `MenuItem`. Uses a Composite Primary Key (`OrderId`, `MenuItemId`) and tracks item quantity (`Count`).

## Core Services

The business logic relies on robust services inside `BLogicLayer/Services` handling Entity Framework operations:

### `MenuItemService`
Manages the restaurant's menu catalog. Key capabilities include:
- Adding, editing, and removing menu items.
- Retrieving menu items with various robust filters:
  - By category.
  - By specific price ranges.
  - Using string search by item name.

### `OrderService`
Manages customer receipts and total calculations. Key operations include:
- Generating new orders by combining multiple items, mapping the `OrderItem` relationships, and automatically calculating the `TotalAmount` dynamically based on prices.
- Removing entire orders.
- Generating query reports and historic records:
  - Lookup by exact order ID.
  - Filter orders by specific Date or a Date Interval.
  - Filter orders within a given price constraint interval.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (or SQL Server Express / LocalDB)

## Getting Started

### 1. Database Configuration

The standard connection string is located in your DB Context: `RestourantServiceApp.DataAccsessLayer/Contexts/RestourantDbContext.cs`

Double-check it matches your SQL Server instance constraint. Example:
```csharp
"Server=.;Database=RestourantServiceDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

### 2. Migrations and Database Seeding

To create your database schema and apply the seed data (which includes some pre-populated Menu Items and Orders for testing), run the EF Core CLI tools command from the solution root:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project RestourantServiceApp.DataAccsessLayer --startup-project RestourantServiceApp.PL
```

### 3. Build & Run

To build everything and launch the interactive Presentation Layer:

```bash
dotnet build
dotnet run --project RestourantServiceApp.PL
```