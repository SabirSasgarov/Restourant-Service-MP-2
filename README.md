# Restaurant Service App

A monolithic C# `.NET 8` console application for managing restaurant menu items and customer orders.

## Architecture

This solution follows an N-Tier structure:

1. **`RestourantServiceApp.Core`**
   - Domain models: `MenuItem`, `Order`, `OrderItem`
   - Base entities and enums (for example `Category`)

2. **`RestourantServiceApp.DataAccsessLayer`**
   - `Entity Framework Core` context (`RestourantDbContext`)
   - Repository implementation (`IRepository<T>`, `Repository<T>`)
   - Entity configurations, seeds, and migrations

3. **`RestourantServiceApp.BLogicLayer`**
   - Business services: `MenuItemService`, `OrderService`
   - DTOs and AutoMapper profiles
   - Custom exceptions

4. **`RestourantServiceApp.PL`**
   - Console presentation (`Program.cs`)
   - Interactive menu/order management flow

## Project Structure (Quick Navigation)

```text
Restourant Service App/
├── README.md
├── RestourantServiceApp.sln
├── RestourantServiceApp.Core/
│   ├── Models/
│   │   ├── Common/
│   │   │   └── BaseEntity.cs
│   │   ├── MenuItem.cs
│   │   ├── Order.cs
│   │   └── OrderItem.cs
│   └── Enums/
│       └── Category.cs
├── RestourantServiceApp.DataAccsessLayer/
│   ├── Contexts/
│   │   └── RestourantDbContext.cs
│   ├── Interfaces/
│   │   └── IRepository.cs
│   ├── Concretes/
│   │   └── Repository.cs
│   ├── Configurations/
│   │   ├── MenuItemConfiguration.cs
│   │   ├── OrderConfiguration.cs
│   │   ├── OrderItemConfiguration.cs
│   │   └── Seeds/
│   └── Migrations/
├── RestourantServiceApp.BLogicLayer/
│   ├── Dtos/
│   │   ├── MenuItemDtos/
│   │   └── OrderDtos/
│   ├── Interfaces/
│   │   ├── IMenuItemService.cs
│   │   └── IOrderService.cs
│   ├── Services/
│   │   ├── MenuItemService.cs
│   │   └── OrderService.cs
│   ├── Mappers/
│   │   └── MapProfile.cs
│   └── Exceptions/
└── RestourantServiceApp.PL/
    └── Program.cs
```

## Domain Model Summary

- **`MenuItem`**: menu name, price, category
- **`Order`**: order date, total amount, collection of order items
- **`OrderItem`**: quantity (`Count`) and foreign keys to an order and menu item

## Core Features

### Menu Operations
- Add menu item
- Update menu item
- Remove menu item
- List all menu items
- Filter by category
- Filter by price range
- Search by name

### Order Operations
- Create order from selected menu items
- Cancel order
- List all orders
- Filter by date interval
- Filter by total price interval
- Filter by date
- Show selected order details

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (or SQL Server Express / LocalDB)

## Getting Started

### 1) Configure database connection

Connection string is currently configured in:

- `RestourantServiceApp.DataAccsessLayer/Contexts/RestourantDbContext.cs`

Example:

```csharp
"Data Source=.\\MSSQLSERVER01;Initial Catalog=RestourantServiceDb;Integrated Security=True;Trust Server Certificate=True;"
```

### 2) Apply migrations

From solution root:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update --project RestourantServiceApp.DataAccsessLayer --startup-project RestourantServiceApp.PL
```

### 3) Build and run

```bash
dotnet build
dotnet run --project RestourantServiceApp.PL
```