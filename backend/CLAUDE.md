# Xpanse Backend

ASP.NET Core 10.0 Web API for the Xpanse personal expense tracking application.

## Project Info

- **Solution file**: `backend.slnx`
- **Project file**: `server.csproj` (namespace: `server`)
- **Framework**: .NET 10.0
- **Port**: 5093 (http), see `Properties/launchSettings.json`

## Project Structure

```
backend/
├── Controllers/        # API controllers
├── Models/             # Entity models (User, Expense, Category)
├── Properties/         # Launch settings
├── Program.cs          # App entry point and DI configuration
├── appsettings.json    # App configuration
└── server.csproj       # Project file
```

## Models

- `User` — app user with email/password auth
- `Expense` — expense record linked to a User and Category
- `Category` — expense category

## Commands

```bash
# Run the API
dotnet run --project server.csproj

# Build
dotnet build

# Add a NuGet package
dotnet add package <PackageName>

# EF Core migrations (once EF is set up)
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Conventions

- Namespace: `server` (matches project name in `server.csproj`)
- Models live in `Models/` with namespace `server.Models`
- Controllers live in `Controllers/` with namespace `server.Controllers`
- Use `DateTime.UtcNow` for timestamps
- Primary keys are `int Id`
- Nullable reference types are enabled — use `= null!` for required navigation properties and `= string.Empty` for required strings

## Stack

- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Auth**: JWT bearer tokens (Microsoft.AspNetCore.Authentication.JwtBearer)
