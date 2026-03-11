# 💰 SpendWise — Personal Finance Tracker

A full-stack personal finance application built with **ASP.NET Core** and **Next.js**. Track expenses, set budget limits, and visualize your spending habits with interactive charts and monthly reports.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-512BD4?style=flat-square&logo=dotnet)
![Next.js](https://img.shields.io/badge/Next.js-14-000000?style=flat-square&logo=nextdotjs)
![License](https://img.shields.io/badge/License-MIT-22c55e?style=flat-square)

---

## Overview

SpendWise gives you a clear picture of where your money goes. Add expenses, organize them into categories, set monthly budgets, and review your financial health through a clean dashboard with real-time summaries and charts.

**Key capabilities:**

- JWT-based authentication (register & login)
- Full CRUD for expenses with category tagging
- Monthly spending summaries and budget tracking
- Pagination, filtering, and date-range queries
- Interactive dashboard with charts and visual breakdowns

---

## Tech Stack

| Layer         | Technology                          |
| ------------- | ----------------------------------- |
| **API**       | ASP.NET Core 8 Web API, Entity Framework Core |
| **Auth**      | JWT Bearer tokens, BCrypt password hashing |
| **Database**  | SQL Server (or PostgreSQL)          |
| **Frontend**  | Next.js 14, React, Tailwind CSS     |
| **Charts**    | Recharts (or Chart.js)              |

---

## Project Structure

```
spendwise/
├── server/                       # ASP.NET Core API
│   ├── Controllers/
│   │   ├── AuthController.cs     # Register & login
│   │   ├── ExpensesController.cs # Expense CRUD
│   │   └── ReportsController.cs  # Monthly summaries
│   ├── Models/
│   │   ├── User.cs
│   │   ├── Expense.cs
│   │   └── Category.cs
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── Services/
│   │   ├── AuthService.cs
│   │   └── ExpenseService.cs
│   ├── DTOs/
│   ├── Middleware/
│   └── Program.cs
│
└── client/                       # Next.js frontend
    ├── app/
    │   ├── dashboard/
    │   ├── expenses/
    │   └── login/
    ├── components/
    │   ├── ExpenseForm.tsx
    │   ├── ExpenseTable.tsx
    │   ├── MonthlyChart.tsx
    │   └── BudgetMeter.tsx
    └── lib/
        └── api.ts
```

---

## Data Model

```
User
 ├── Id (guid)
 ├── Email
 ├── PasswordHash
 └── CreatedAt

Category
 ├── Id (int)
 ├── Name            # e.g. "Food", "Transport", "Entertainment"
 └── Color           # Hex code for chart display

Expense
 ├── Id (guid)
 ├── UserId  ──────► User
 ├── CategoryId ───► Category
 ├── Amount (decimal)
 ├── Description
 ├── Date
 └── CreatedAt
```

**Relationships:** Each User has many Expenses. Each Expense belongs to one Category. Categories are shared across users.

---

## API Endpoints

### Authentication

| Method | Route             | Description                | Auth |
| ------ | ----------------- | -------------------------- | ---- |
| POST   | `/auth/register`  | Create a new account       | No   |
| POST   | `/auth/login`     | Get a JWT token            | No   |

### Expenses

| Method | Route              | Description                        | Auth |
| ------ | ------------------ | ---------------------------------- | ---- |
| GET    | `/expenses`        | List expenses (paginated, filtered)| Yes  |
| POST   | `/expenses`        | Create a new expense               | Yes  |
| PUT    | `/expenses/{id}`   | Update an expense                  | Yes  |
| DELETE | `/expenses/{id}`   | Delete an expense                  | Yes  |

**Query parameters for `GET /expenses`:**

| Param        | Type   | Example            | Description               |
| ------------ | ------ | ------------------ | ------------------------- |
| `page`       | int    | `1`                | Page number               |
| `pageSize`   | int    | `20`               | Items per page            |
| `categoryId` | int    | `3`                | Filter by category        |
| `from`       | date   | `2025-01-01`       | Start date                |
| `to`         | date   | `2025-01-31`       | End date                  |
| `sortBy`     | string | `date`             | Sort field                |
| `order`      | string | `desc`             | Sort direction            |

### Reports

| Method | Route              | Description                        | Auth |
| ------ | ------------------ | ---------------------------------- | ---- |
| GET    | `/reports/monthly` | Monthly totals by category         | Yes  |

**Query parameters:** `month` (int), `year` (int)

**Response example:**

```json
{
  "month": 3,
  "year": 2025,
  "totalSpent": 1847.50,
  "budgetLimit": 2000.00,
  "remaining": 152.50,
  "byCategory": [
    { "category": "Food", "total": 620.00, "percentage": 33.6 },
    { "category": "Transport", "total": 410.00, "percentage": 22.2 },
    { "category": "Entertainment", "total": 275.50, "percentage": 14.9 }
  ]
}
```

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- SQL Server (or PostgreSQL with `Npgsql` provider)

### 1. Clone the repo

```bash
git clone https://github.com/your-username/spendwise.git
cd spendwise
```

### 2. Set up the API

```bash
cd server

# Configure your connection string and JWT secret
cp appsettings.Example.json appsettings.Development.json
# Edit appsettings.Development.json with your values

# Run migrations
dotnet ef database update

# Start the API
dotnet run
```

The API will be available at `https://localhost:5001`.

### 3. Set up the frontend

```bash
cd client

npm install

# Create your env file
cp .env.example .env.local
# Set NEXT_PUBLIC_API_URL=https://localhost:5001

npm run dev
```

The app will be available at `http://localhost:3000`.

---

## Environment Variables

**API (`appsettings.Development.json`):**

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=SpendWise;Trusted_Connection=true;"
  },
  "Jwt": {
    "Secret": "your-256-bit-secret-key-here",
    "Issuer": "SpendWise",
    "ExpiryMinutes": 1440
  }
}
```

**Frontend (`.env.local`):**

```
NEXT_PUBLIC_API_URL=https://localhost:5001
```

---

## Backend Concepts Practiced

This project is designed to cover practical backend fundamentals:

- **Entity relationships** — one-to-many between User → Expense and Category → Expense, configured with EF Core Fluent API
- **JWT authentication** — token generation, middleware validation, extracting the current user from claims
- **Pagination & filtering** — `IQueryable` composition with optional filters, skip/take pagination, and sort parameters
- **Aggregations** — LINQ `GroupBy` queries for monthly totals and category breakdowns
- **DTOs & mapping** — separating API contracts from database entities
- **Input validation** — data annotations and custom validation logic
- **Error handling** — global exception middleware with consistent error responses
- **Repository pattern** (optional) — abstracting data access behind interfaces

---

## Frontend Pages

| Page            | Route          | Description                                      |
| --------------- | -------------- | ------------------------------------------------ |
| Login / Register| `/login`       | Auth forms with JWT storage                      |
| Dashboard       | `/dashboard`   | Monthly summary, budget meter, spending chart    |
| Expenses        | `/expenses`    | Searchable/filterable expense table with CRUD    |
| Add Expense     | `/expenses/new`| Expense form with category picker and date input |

---

## Roadmap

- [ ] Recurring expenses
- [ ] CSV/PDF export
- [ ] Multi-currency support
- [ ] Dark mode
- [ ] Mobile-responsive PWA
- [ ] Notifications when approaching budget limit

---

## License

This project is licensed under the [MIT License](LICENSE).
