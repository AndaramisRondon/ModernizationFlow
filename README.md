# Modernization Flow

[Versão em Português](LEIAME.md)

## About the Author

**Andaramis Bezerra**  
Senior Software Developer / Software Architect

Software professional with extensive experience in enterprise systems, focusing on C#/.NET, ASP.NET Core, ASP.NET MVC, REST APIs, SQL Server, application architecture and legacy system modernization.

This project demonstrates an incremental modernization strategy, showing how an existing .NET application can evolve toward a React frontend without requiring a full rewrite.

### Contact

- **Email:** [andaramis@gmail.com](mailto:andaramis@gmail.com)
- **LinkedIn:** [linkedin.com/in/andaramis](https://www.linkedin.com/in/andaramis/)

---



Modernization Flow is a reference project that demonstrates an **incremental modernization strategy for legacy ASP.NET MVC applications**, moving the presentation layer toward React while preserving business rules and backend capabilities.

The project was designed to simulate a common enterprise modernization scenario:

```text
Legacy MVC
    ↓
Application / Use Cases
    ↓
ASP.NET Core REST API
    ↓
React
```

Instead of rewriting the entire application at once, the architecture allows features to be migrated gradually.

---

## Project Goals

The main goals of this project are:

- Demonstrate incremental modernization of a legacy MVC application.
- Separate business rules from the presentation layer.
- Expose application capabilities through REST APIs.
- Introduce React without requiring a complete backend rewrite.
- Keep workflow rules centralized in the backend.
- Use a modern and maintainable frontend architecture.
- Demonstrate a realistic end-to-end enterprise workflow.

---

## Architecture

The solution follows a layered architecture.

```text
┌───────────────────────────────┐
│           React UI            │
│                               │
│ React + TypeScript            │
│ React Router                  │
│ TanStack Query                │
│ React Hook Form + Zod         │
└───────────────┬───────────────┘
                │
                │ HTTP / JSON
                ▼
┌───────────────────────────────┐
│       ASP.NET Core API        │
│                               │
│ Controllers                   │
│ REST Endpoints                │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│      Application Layer        │
│                               │
│ Commands / Use Cases          │
│ Application Handlers          │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│          Domain Layer         │
│                               │
│ Entities                      │
│ Workflow Rules                │
│ Business Rules                │
└───────────────┬───────────────┘
                │
                ▼
┌───────────────────────────────┐
│      Infrastructure Layer     │
│                               │
│ Entity Framework Core         │
│ Repositories                  │
│ SQL Server                    │
└───────────────────────────────┘
```

The frontend does not contain the workflow business rules themselves.

The React application decides which actions should be displayed based on the current state, while the backend remains responsible for validating whether a workflow transition is valid.

---

## Request Workflow

The main business object in version 1 is a `Request`.

The workflow is:

```text
                  ┌───────────┐
                  │   Draft   │
                  └─────┬─────┘
                        │
                        │ Submit
                        ▼
                ┌───────────────┐
                │  UnderReview  │
                └───────┬───────┘
                        │
               ┌────────┴────────┐
               │                 │
               │ Approve         │ Reject
               ▼                 ▼
        ┌────────────┐     ┌────────────┐
        │  Approved  │     │  Rejected  │
        └────────────┘     └────────────┘
```

A request can be edited only while it is in `Draft`.

After submission, the request moves to `UnderReview` and can then be approved or rejected.

---

## Version 1 Features

Version `1.0.0` implements the complete basic request workflow.

### Requests

- List requests.
- View request details.
- Create a new request.
- Edit requests while they are in Draft.
- Submit a request for review.
- Approve requests under review.
- Reject requests under review.

### Frontend

- Client-side routing.
- Server-state management with TanStack Query.
- Forms with React Hook Form.
- Validation with Zod.
- Loading states.
- API error handling.
- Cache invalidation after mutations.
- Confirmation dialogs with SweetAlert2.
- Currency formatting using Brazilian Real.
- Date formatting using `pt-BR`.

### Backend

- REST API using ASP.NET Core.
- Layered architecture.
- Application handlers.
- Domain workflow validation.
- Repository abstraction.
- Entity Framework Core.
- SQL Server persistence.
- CORS configuration for the React frontend.
- Automated domain and application tests.

---

## Technical Status vs. Display Status

Workflow status values are kept independent from the text presented to users.

For example:

```json
{
  "status": "UnderReview",
  "statusDescription": "Em análise"
}
```

The technical status is used by application logic:

```ts
request.status === 'UnderReview'
```

while the localized description is used by the UI:

```tsx
request.statusDescription
```

This separation prepares the application for future internationalization without changing workflow rules.

Current mappings:

| Technical Status | PT-BR |
|---|---|
| `Draft` | Rascunho |
| `UnderReview` | Em análise |
| `Approved` | Aprovado |
| `Rejected` | Reprovado |

---

## Technology Stack

### Backend

- C#
- .NET / ASP.NET Core
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- xUnit

### Frontend

- React
- TypeScript
- Vite
- React Router
- TanStack Query
- React Hook Form
- Zod
- SweetAlert2
- CSS

### Development

- Visual Studio
- Visual Studio Code
- Git
- GitHub
- npm

---

## Repository Structure

```text
ModernizationFlow
│
├── backend
│   ├── ModernizationFlow.Api
│   ├── ModernizationFlow.Application
│   ├── ModernizationFlow.Domain
│   ├── ModernizationFlow.Infrastructure
│   └── ModernizationFlow.Tests
│
├── frontend
│   └── modernization-flow-web
│
├── docs
│
├── .github
│
└── README.md
```

### Backend Projects

#### `ModernizationFlow.Domain`

Contains the core domain model and business rules.

Responsibilities include:

- Request entity.
- Workflow states.
- Workflow transition validation.
- Domain behavior.

#### `ModernizationFlow.Application`

Contains application use cases.

Responsibilities include:

- Commands.
- Handlers.
- DTOs.
- Application orchestration.

#### `ModernizationFlow.Infrastructure`

Contains infrastructure concerns.

Responsibilities include:

- Entity Framework Core.
- SQL Server integration.
- Repository implementations.
- Persistence configuration.

#### `ModernizationFlow.Api`

Exposes application capabilities through REST endpoints.

Responsibilities include:

- Controllers.
- HTTP contracts.
- Dependency injection.
- CORS.
- Swagger.

#### `ModernizationFlow.Tests`

Contains automated tests for domain and application behavior.

---

## Frontend Structure

The React application uses a feature-oriented structure.

```text
src
│
├── api
│   └── httpClient.ts
│
├── components
│   ├── common
│   │   └── dialogs
│   │       └── confirmDialog.ts
│   │
│   └── layout
│
├── features
│   └── requests
│       ├── api
│       ├── components
│       ├── pages
│       ├── schemas
│       └── types
│
├── pages
│
├── routes
│
├── App.tsx
└── main.tsx
```

Code identifiers are written in English, while version 1 user-facing content is presented in Portuguese.

---

## REST API

The current Request API exposes the following endpoints:

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/requests` | List requests |
| `GET` | `/api/requests/{id}` | Get request details |
| `POST` | `/api/requests` | Create request |
| `PUT` | `/api/requests/{id}` | Update request |
| `POST` | `/api/requests/{id}/submit` | Submit request for review |
| `POST` | `/api/requests/{id}/approve` | Approve request |
| `POST` | `/api/requests/{id}/reject` | Reject request |

---

## Running the Project

### Prerequisites

Install:

- .NET SDK
- SQL Server
- Node.js
- npm
- Git

---

## Backend

From the repository root:

```bash
cd backend/ModernizationFlow.Api
```

Restore dependencies:

```bash
dotnet restore
```

Run the API:

```bash
dotnet run
```

During development the HTTPS API is configured at:

```text
https://localhost:7184
```

Swagger is available at:

```text
https://localhost:7184/swagger
```

---

## Database

The backend uses SQL Server through Entity Framework Core.

Create or update the database using EF Core migrations.

Example using Visual Studio Package Manager Console:

```powershell
Update-Database -StartupProject ModernizationFlow.Api
```

Or using the .NET CLI according to the local EF Core configuration.

Database configuration should be defined in the ASP.NET Core application settings for the development environment.

---

## Frontend

Navigate to:

```bash
cd frontend/modernization-flow-web
```

Install dependencies:

```bash
npm install
```

Create:

```text
.env.development
```

with:

```env
VITE_API_BASE_URL=https://localhost:7184/api
```

Start the development server:

```bash
npm run dev
```

The application will normally be available at:

```text
http://localhost:5173
```

---

## Production Build

To validate and build the React application:

```bash
npm run build
```

Run static analysis:

```bash
npm run lint
```

Version 1 was validated successfully with both commands.

---

## Automated Tests

Backend tests can be executed from Visual Studio Test Explorer or through the .NET CLI.

Example:

```bash
dotnet test
```

Version 1 currently contains automated tests covering important workflow and application scenarios, including:

- Request creation.
- Submit from Draft.
- Approval workflow.
- Invalid approval transitions.
- Rejection workflow.

At the completion of version 1:

```text
Tests: 5
Passed: 5
Failed: 0
```

---

## End-to-End Flow

A typical successful flow is:

```text
React Form
    ↓
React Hook Form
    ↓
Zod Validation
    ↓
TanStack Mutation
    ↓
HTTP Client
    ↓
ASP.NET Core Controller
    ↓
Application Handler
    ↓
Domain
    ↓
Repository
    ↓
Entity Framework Core
    ↓
SQL Server
```

After mutations, TanStack Query invalidates the corresponding cache and retrieves the current state from the API.

For example:

```text
Edit Request
    ↓
PUT /api/requests/{id}
    ↓
Database updated
    ↓
Invalidate ['requests']
Invalidate ['requests', id]
    ↓
Updated UI
```

---

## Modernization Strategy

One of the main ideas demonstrated by this repository is that modernization does not necessarily require a full rewrite.

A legacy application can evolve incrementally:

```text
Step 1
Legacy MVC
Business logic coupled to the application

        ↓

Step 2
Extract business rules and use cases

        ↓

Step 3
Expose use cases through REST APIs

        ↓

Step 4
Introduce React for selected features

        ↓

Step 5
Gradually migrate additional screens
```

This approach allows legacy and modern features to coexist during the migration period.

Benefits include:

- Lower migration risk.
- Smaller deployment increments.
- Easier rollback.
- Progressive team adoption.
- Business continuity.
- Ability to prioritize high-value screens.
- Reuse of existing backend knowledge and business rules.

---

## Design Decisions

Some intentional decisions in version 1 include:

**TanStack Query instead of global Redux state**

Server data is managed as server state and cached by TanStack Query. Redux is therefore unnecessary for the current scope.

**React Hook Form + Zod**

Forms and validation remain strongly typed while keeping UI code relatively small.

**Workflow validation remains in the backend**

The frontend controls the user experience, but the backend remains the source of truth for valid state transitions.

**Technical status separated from display status**

Workflow values remain stable regardless of UI language.

**Feature-oriented frontend structure**

Request-related pages, API hooks, schemas and types remain grouped within the same feature.

**Reusable confirmation dialog**

SweetAlert2 is encapsulated behind the application's own `confirmDialog` helper instead of being coupled directly to every page.

---

## Version 1 Scope

The goal of version 1 is intentionally limited.

Included:

- Request CRUD flow.
- Workflow transitions.
- React/API integration.
- SQL Server persistence.
- Form validation.
- Query caching.
- Automated backend tests.
- Localized status descriptions.
- Reusable confirmation UI.

Not included in version 1:

- Authentication.
- Authorization and role-based access.
- Rejection reason capture.
- Audit history.
- Dynamic internationalization.
- Dashboard.
- Notifications.
- Production deployment pipeline.
- Full legacy MVC coexistence demonstration.

These capabilities can be introduced incrementally in future versions.

---

## Possible Next Steps

Future versions may explore:

- Authentication and authorization.
- User roles for requester and approver.
- Rejection reason and workflow history.
- Audit logging.
- Full i18n support for PT-BR, EN-US and ES.
- Pagination, filtering and sorting.
- Dashboard and workflow metrics.
- Notifications.
- Automated frontend tests.
- Integration tests.
- CI/CD with GitHub Actions or Azure DevOps.
- Containerization with Docker.
- Observability and structured logging.
- Migration of additional legacy MVC modules.
- Side-by-side legacy MVC and React operation.

---

## Version

Current stable version:

```text
v1.0.0
```

Version 1 establishes the technical foundation for incremental migration from an ASP.NET-based application toward a modern React frontend while preserving a structured .NET backend.

---

## Author

**Andaramis Bezerra**

Senior Software Developer / Software Architect

Main areas of experience:

- C# / .NET
- ASP.NET Core
- ASP.NET MVC
- REST APIs
- SQL Server
- Enterprise application architecture
- React
- Application modernization