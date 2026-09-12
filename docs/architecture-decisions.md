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

