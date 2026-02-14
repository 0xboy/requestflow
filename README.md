# RequestFlow

A full-stack internal request and approval system built with ASP.NET Core. Users submit requests (leave, equipment, training, etc.), and managers approve or reject them with full audit history. Built as a portfolio project to showcase clean architecture, role-based auth, and real-world CRUD + workflow patterns.

---

## What's in it

- **Role-based access**: User, Manager, Admin — each sees the right screens and actions
- **Request lifecycle**: Draft → Submit for approval → Approved / Rejected, with status history
- **Dashboard**: Role-specific summaries (e.g. pending count for managers, own requests for users)
- **List & filters**: Pagination, search by title, filter by status and date
- **Clean code**: Enums and constants for statuses, priorities, roles — no magic strings

---

## Tech stack

| Layer | Choices |
|-------|---------|
| **Backend** | .NET 8, ASP.NET Core MVC |
| **Data** | Entity Framework Core (Code First), SQL Server |
| **Auth** | ASP.NET Core Identity (cookie-based) |
| **Front** | Razor Views, Bootstrap 5 |

---

## Architecture

The solution is structured as **Onion / Clean Architecture**: domain at the center, dependencies pointing inward.

```
Presentation (MVC)  →  Application (services, DTOs)  →  Domain (entities, enums)
                              ↑
Infrastructure (EF, repos, implementations)  ──────────┘
```

- **RequestFlow** — Controllers, views, view models  
- **RequestFlow.Application** — Interfaces, DTOs, application services  
- **RequestFlow.Domain** — Entities and domain enums  
- **RequestFlow.Infrastructure** — Service implementations  
- **RequestFlow.Persistence** — DbContext, repositories, migrations, seeding  
- **RequestFlow.Shared** — Constants, shared enums  

---

## Getting started

**Prerequisites:** .NET 8 SDK, SQL Server (or LocalDB — default in the repo).

```bash
git clone https://github.com/YOUR_USERNAME/RequestFlow.git
cd RequestFlow
dotnet restore RequestFlow.sln
dotnet run --project src/RequestFlow/RequestFlow.csproj
```

First run applies migrations and seeds the database. Open the URL from the console (e.g. `https://localhost:7xxx`).

### Demo logins

| Role    | Email            | Password  |
|---------|------------------|-----------|
| User    | user@test.com    | Test123!  |
| Manager | manager@test.com | Test123!  |
| Admin   | admin@test.com   | Test123!  |

---

## Project layout

```
RequestFlow/
├── src/
│   ├── RequestFlow/                 # Web app (MVC)
│   ├── RequestFlow.Core/
│   │   ├── RequestFlow.Application/
│   │   └── RequestFlow.Domain/
│   ├── RequestFlow.Infrastructure/
│   │   ├── RequestFlow.Infrastructure/
│   │   └── RequestFlow.Persistence/
│   └── RequestFlow.Shared/
├── RequestFlow.sln
└── README.md
```

---

## Database

Default connection uses LocalDB. To use another SQL Server instance, change the connection string in `src/RequestFlow/appsettings.json`. Migrations run automatically on startup; no manual `dotnet ef` step needed for local run.

---

*Part of my portfolio — feedback and suggestions welcome.*
