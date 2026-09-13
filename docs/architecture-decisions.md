# Architectural Decisions

This document records the main architectural decisions adopted in the
ModernizationFlow project.

The goal is not only to describe what was implemented, but also to explain
why each decision was made, including the trade-offs involved.

---

## ADR-001 — Repository contracts belong to the Application layer

### Context

The application needs to access and persist `Request` entities.

A common approach in layered applications is to place repository interfaces
inside the Infrastructure layer together with their concrete implementations.

However, doing so would cause the Application layer to depend on
Infrastructure in order to execute its use cases.

That dependency direction would couple business orchestration to persistence
technology.

### Decision

Repository contracts required by the application are defined in the
Application layer.

For example:

ModernizationFlow.Application
└── Interfaces
    └── IRequestRepository

The Infrastructure layer provides the concrete implementation:

ModernizationFlow.Infrastructure
└── Repositories
    └── RequestRepository : IRequestRepository

The dependency direction is:

Application ───────► Domain

Infrastructure ───► Application
Infrastructure ───► Domain

The Application layer does not reference Infrastructure.

Why

The Application layer owns the use cases and defines the operations it needs
from external dependencies.

Infrastructure adapts technologies such as Entity Framework Core and SQL Server
to those contracts.

This follows the Dependency Inversion Principle.

Instead of depending directly on:

Entity Framework Core
DbContext
SQL Server

the Application layer depends only on an abstraction:

IRequestRepository

Benefits
- Lower coupling between application logic and persistence technology.
- Improved unit testability.
- Clearer dependency direction.
- Easier replacement of persistence implementations.
- Use cases remain independent from how data is stored.

The same Application layer could work with different implementations, such as:

SQL Server + Entity Framework Core
PostgreSQL
Dapper
In-memory repository for tests
External persistence service

without changing the use cases.

Trade-off

This introduces an additional abstraction and requires dependency injection
configuration.

For a very small CRUD application this could be unnecessary complexity.

In ModernizationFlow, this abstraction is intentional because the project is
designed to demonstrate a maintainable architecture suitable for enterprise
applications and incremental modernization scenarios.

Status

Accepted

---

## ADR-002 — Organize the Application layer by use case instead of a large service class

### Context

Enterprise applications often evolve toward large service or manager classes
that centralize many responsibilities.

For example, a single `RequestManager` could eventually contain operations for:

- creating requests;
- searching requests;
- updating requests;
- submitting requests;
- approving requests;
- rejecting requests;
- generating reports;
- handling workflow rules.

Although this approach may initially seem simple, these classes tend to grow
over time and become harder to maintain, test, and understand.

### Decision

The Application layer is organized by use case.

Each business operation is represented by a small and focused handler.

Example:

Requests
├── Create
│   └── CreateRequestHandler
├── GetById
│   └── GetRequestByIdHandler
├── Search
│   └── SearchRequestsHandler
├── Update
│   └── UpdateRequestHandler
├── Submit
│   └── SubmitRequestHandler
├── Approve
│   └── ApproveRequestHandler
└── Reject
    └── RejectRequestHandler

Each handler is responsible for orchestrating one application use case.

Why

This keeps each class focused on a single responsibility and avoids the
progressive growth of large service or manager classes.

The Application layer is responsible for orchestrating the use case, while
business rules remain inside the Domain whenever appropriate.

For example:

ApproveRequestHandler
        |
        v
request.Approve()
        |
        v
Domain validates the transition

The handler coordinates the operation, but it does not duplicate the domain
rule.

Benefits
 - Smaller and more focused classes.
 - Easier navigation and maintenance.
 - Better separation of responsibilities.
 - Improved unit testability.
 - Lower risk of creating large service classes.
 - New use cases can be added without increasing the complexity of unrelated
 - operations.
 - The project structure reflects business capabilities instead of technical
 - utility classes.
 - Trade-off

This approach creates more files and classes than a single service class.

For very small applications, a traditional service class may be sufficient.

In ModernizationFlow, this additional structure is intentional because the
project demonstrates an architecture designed to evolve safely as business
requirements increase.

Status

Accepted

---

## ADR-003 — Do not expose Domain entities directly through the API

### Context

The Domain layer contains the core business model of the application.

These entities are designed to protect business rules and internal state,
not to serve as public API contracts.

If API endpoints return Domain entities directly, the external contract of the
application becomes coupled to the internal business model.

Changes in the Domain could then unintentionally break API consumers.

### Decision

The Application layer exposes DTOs for data returned by use cases.

For example:


Domain Entity
    |
    v
Application DTO
    |
    v
API Response

The API does not serialize Domain entities directly.

Example:
Request
   |
   v
RequestDto
   |
   v
GET /api/requests

Why

The Domain model and the API contract evolve for different reasons.

The Domain may change because of business rules.

The API contract may change because of:

frontend requirements;
versioning;
security;
compatibility;
response shape;
localization;
performance.

Keeping these models separate avoids unnecessary coupling.

Benefits
 - Domain entities remain internal to the application.
 - API contracts can evolve independently.
 - Reduced risk of exposing internal properties accidentally.
 - Easier API versioning.
 - Better control over serialization.
 - Frontend requirements do not force changes in the Domain model.
 - Domain refactoring is less likely to break API consumers.
 - Example

Instead of returning: Domain.Entities.Request
the Application layer maps it to: RequestDto

The API then returns the DTO as JSON.

Trade-off

This approach introduces mapping code and additional DTO classes.

For very small applications, returning Domain entities directly may appear
simpler.

In ModernizationFlow, the separation is intentional because the project is
designed to demonstrate maintainable boundaries between business logic and
external API contracts.

Status

Accepted