<!--
SYNC IMPACT REPORT
==================
Version change: (none) → 1.0.0
Modified principles: N/A (initial creation)
Added sections: Core Principles, Technology Stack, Development Standards, Governance
Removed sections: N/A
Templates requiring updates:
  - .specify/templates/plan-template.md ✅ reviewed — Constitution Check section references this doc
  - .specify/templates/spec-template.md ✅ reviewed — no amendments needed
  - .specify/templates/tasks-template.md ✅ reviewed — no amendments needed
Deferred TODOs: none
-->

# SportsClub Constitution

## Core Principles

### I. Layered Architecture (NON-NEGOTIABLE)

The solution MUST maintain three distinct projects with clear responsibilities:

- **SportsClub.Api** — ASP.NET Core Web API: controllers, entities, repositories, domain logic only.
  No UI concerns; no direct reference to Blazor components.
- **SportsClub.SharedModels** — C# record DTOs shared between API and Web.
  This project MUST NOT contain business logic or EF entity types.
- **SportsClub.Web** — Blazor WebAssembly frontend: Razor components, pages, services, layout only.
  MUST NOT contain data access logic; all server calls go through `HttpClient` or a typed service.

Cross-layer dependencies are only allowed downward: `Api → SharedModels`, `Web → SharedModels`.
Direct references from `Web` to `Api` internals are forbidden.

### II. Contract-First Shared Models

All data crossing the API boundary MUST be represented as C# `record` types in `SportsClub.SharedModels`.

- DTOs MUST use C# `record` (not `class`) to enforce value equality and immutability.
- Renaming or removing DTO members is a breaking change and MUST trigger a version discussion.
- Entities in `SportsClub.Api.Entities` MUST NOT be returned directly from controllers;
  conversion to DTOs via extension methods (e.g., `DtoConversions`) is required.

### III. Repository Pattern

Data access MUST be abstracted behind an `ISportsClubRepository` interface.

- All repository methods MUST be `async Task<T>` regardless of current implementation (in-memory or future database).
- Controllers MUST NOT contain data-access logic; they delegate to the repository.
- New data concerns MUST be added to the repository interface first, before any implementation.
- Swapping implementations (e.g., in-memory → EF Core) MUST require zero changes to controllers.

### IV. Simplicity First (YAGNI)

Features MUST NOT be added speculatively. Start with the simplest implementation that satisfies the requirement.

- In-memory repository is the valid default for development and demos; do not prematurely introduce a database.
- CORS configuration MUST remain explicit (no wildcard origins in production-bound code).
- Razor components MUST NOT duplicate service-layer logic; light view-logic is acceptable inline,
  but anything reused across two or more components MUST be extracted to a service.
- Performance optimisations MUST be justified with measurement, not assumption.

### V. .NET Modern Practices

All projects target **.NET 10** and MUST use:

- `<Nullable>enable</Nullable>` — nullable reference types enforced at compile time.
- `<ImplicitUsings>enable</ImplicitUsings>` — no redundant `using` directives for BCL types.
- `async`/`await` for all I/O-bound operations; no blocking `.Result` or `.Wait()` calls.
- Constructor injection (primary constructor syntax preferred) for dependency injection.
- `record` types for DTOs; `class` types for EF entities and stateful services.

## Technology Stack

- **Runtime**: .NET 10
- **Backend**: ASP.NET Core Web API with OpenAPI (`Microsoft.AspNetCore.OpenApi`)
- **Frontend**: Blazor WebAssembly (`Microsoft.AspNetCore.Components.WebAssembly`)
- **Shared contracts**: C# record DTOs in `SportsClub.SharedModels`
- **Data access**: Repository pattern; current implementation is in-memory
  (`SportsClubInMemoryRepository`); future implementations MUST implement `ISportsClubRepository`
- **Auth**: none currently configured — any authentication addition MUST be agreed upon before
  implementation and documented in the relevant feature spec
- **CORS**: explicit allow-list; origins configured in `Program.cs`; no wildcard origins
- **API documentation**: OpenAPI served in Development mode only

## Development Standards

- **Branch strategy**: feature branches per spec (name format: `###-feature-name`).
  Direct commits to `main` are not allowed for feature work.
- **Code review**: all feature PRs require at least one peer review before merge.
- **Testing**: test projects are optional for now but SHOULD be added when the repository
  moves from in-memory to a persistent store. When tests exist, no PR merges with failing tests.
- **Linting**: compiler warnings MUST NOT be suppressed without a comment explaining the reason.
- **Secrets**: no secrets or connection strings in source code; use `appsettings.Development.json`
  (git-ignored) or environment variables for local dev; use a secret manager for production.

## Governance

This constitution supersedes all informal practices. Amendments require:

1. A concrete rationale (what changed and why).
2. Version bump following semantic versioning:
   - **MAJOR**: principle removed, redefined, or made incompatible with prior work.
   - **MINOR**: new principle or section added, or guidance materially expanded.
   - **PATCH**: clarifications, wording fixes, typo corrections.
3. Update of this file and re-validation of all templates listed in the Sync Impact Report.
4. A commit message referencing the new version (e.g., `docs: amend constitution to v1.1.0`).

All feature plans MUST include a Constitution Check gate (see `plan-template.md`) before
Phase 0 research begins.

**Version**: 1.0.0 | **Ratified**: 2026-05-23 | **Last Amended**: 2026-05-23
