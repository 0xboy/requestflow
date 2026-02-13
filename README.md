# RequestFlow

Internal request creation, approval, and tracking system. Role-based ASP.NET MVC web application.

---

## 📋 Project Overview

| Item | Value |
|------|-------|
| **Project Name** | RequestFlow |
| **Architecture** | 3-Tier (Presentation / Business / Data) |
| **Type** | Portfolio / Technical Assessment |

---

## 🛠 Tech Stack

| Component | Technology |
|-----------|------------|
| Framework | ASP.NET MVC (.NET Core 6+) |
| ORM | Entity Framework Core (Code First) |
| Database | MS SQL Server |
| View Engine | Razor Views |
| CSS Framework | Bootstrap 5 |
| Authentication | ASP.NET Core Identity / Cookie-based |

---

## 👥 Roles & Permissions

| Role | Permissions |
|------|-------------|
| **User** | Create and edit requests, view own requests only |
| **Manager** | View all requests, approve or reject |
| **Admin** (optional) | User & role management |

---

## 🗺 Development Roadmap

### Phase 0: Setup (Day 0)
- [ ] Project structure (Solution, projects)
- [ ] Git repository, `.gitignore` configuration
- [ ] Constants and enums (no magic strings)
- [ ] Base folder structure (Controllers, Services, Repositories, Models)

### Phase 1: Infrastructure & Authentication (Day 1)
- [ ] ASP.NET Core MVC project setup
- [ ] Entity Framework Core + SQL Server connection
- [ ] ASP.NET Core Identity integration
- [ ] Role definitions (User, Manager, Admin)
- [ ] Login / Logout
- [ ] Role-based authorization (Authorize attribute)
- [ ] Unauthorized access page (403)
- [ ] Session/Cookie-based authentication

### Phase 2: Data Model & Request Module (Day 2)
- [ ] Entity models (Code First):
  - `User`, `Request`, `RequestStatusHistory`, `RequestType`, `Priority`
- [ ] Migrations and database creation
- [ ] Request fields:
  - Request No (auto-generated), Title, Description
  - Request Type (dropdown), Priority (Low/Medium/High)
  - Created By, Created Date
  - Status (Draft, Pending Approval, Approved, Rejected)
- [ ] Repository pattern / Unit of Work (optional)
- [ ] Request service layer
- [ ] Business rules:
  - Users see only their own requests
  - Managers see all requests
  - Approved requests cannot be updated

### Phase 3: Request CRUD & Approval Flow (Day 3)
- [ ] Request creation form
- [ ] Request edit (with status validation)
- [ ] Request listing page
- [ ] Request detail page
- [ ] Approve / Reject modal or page
- [ ] Rejection requires explanation
- [ ] Request status history (RequestStatusHistory) tracking

### Phase 4: Listing, Filtering & Dashboard (Day 4)
- [ ] Request list filters:
  - Filter by date
  - Filter by status
  - Search by title
- [ ] Pagination
- [ ] Manager Dashboard:
  - Total request count
  - Pending approval count
  - Last 5 requests
- [ ] User Dashboard:
  - Own request status summary
  - Recently added requests

### Phase 5: Admin Module & Polish (Day 5)
- [ ] Admin: User management (optional)
- [ ] Admin: Role management (optional)
- [ ] UI/UX improvements (Bootstrap)
- [ ] Error handling and validation
- [ ] README update (setup instructions)
- [ ] Seed data (test users, request types)
- [ ] Final testing and commits

---

## 📐 Architecture

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                     │
│  (Controllers, Razor Views, ViewModels)                   │
└─────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────┐
│                    Business Layer                         │
│  (Services, DTOs, Business Rules)                         │
└─────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────┐
│                      Data Layer                            │
│  (DbContext, Entities, Repositories)                      │
└─────────────────────────────────────────────────────────┘
```

---

## 📄 Required Screens

| # | Screen | Description |
|---|---------|-------------|
| 1 | Login | User authentication |
| 2 | Dashboard | Role-based overview |
| 3 | Request Create Form | New request submission |
| 4 | Request List | Filtering, pagination |
| 5 | Request Detail | Single request view |
| 6 | Approve / Reject | Modal or dedicated page |
| 7 | Unauthorized | 403 page |

---

## 📁 Project Structure

```
assignment/
├── src/
│   └── RequestFlow/
│       ├── Controllers/
│       ├── Models/
│       ├── Views/
│       ├── Services/
│       ├── Data/
│       │   ├── Entities/
│       │   ├── DbContext/
│       │   └── Migrations/
│       ├── Constants/
│       └── wwwroot/
├── README.md
└── .gitignore
```

---

## ✅ Checklist

- [ ] Working project (runs locally)
- [ ] README: Setup steps, architecture overview
- [ ] Clean code: Constants, enums, no magic strings
- [ ] Meaningful Git commit messages

---

## 📌 Constants & Enums (Example)

```csharp
// RequestStatus.cs
public enum RequestStatus { Draft, PendingApproval, Approved, Rejected }

// Priority.cs  
public enum Priority { Low, Medium, High }

// RoleNames.cs
public static class RoleNames { User, Manager, Admin }
```

---

## 🚀 Setup (Coming Soon)

*Installation steps will be added once the project is complete.*
