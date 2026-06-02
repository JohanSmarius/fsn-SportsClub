# Implementation Plan: Sports Catalog And Daily Schedule

**Branch**: `[001-current-implementation-spec]` | **Date**: 2026-06-02 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `/specs/001-current-implementation-spec/spec.md`

## Summary

Document and formalize the current SportsClub implementation that allows users to browse workouts, review today's lessons, and receive enrollment feedback. The plan focuses on preserving the existing layered architecture (Blazor WebAssembly frontend, ASP.NET Core API backend, shared DTO models), documenting the API contract, and capturing data and flow constraints for subsequent task planning.

## Technical Context

**Language/Version**: C# with .NET 10 (`net10.0`) across API, WebAssembly frontend, and shared models.

**Primary Dependencies**: ASP.NET Core Web API, Microsoft.AspNetCore.OpenApi, Blazor WebAssembly, System.Net.Http.Json.

**Storage**: In-memory seed data (`SportClubSeedData`) through repository abstraction.

**Testing**: No automated test project currently present; validation is currently manual via API endpoints and UI flows.

**Target Platform**: Browser-based WebAssembly client plus local ASP.NET Core API server.

**Project Type**: Multi-project web application (`SportsClub.Web` + `SportsClub.Api` + `SportsClub.SharedModels`).

**Performance Goals**: User-facing data retrieval and feedback interactions should complete quickly enough to support success criteria (for example, visible enrollment feedback within 2 seconds).

**Constraints**: Preserve current layered architecture, repository boundary, and DTO-based API contract; avoid speculative persistence or auth additions.

**Scale/Scope**: Demo-scale sports schedule and catalog browsing for anonymous users, with read-only data and non-persistent enrollment confirmation.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Pre-Phase 0 Gate Review

- **Layered Architecture**: PASS. Current codebase keeps API, Web, and SharedModels in separate projects with expected dependency directions.
- **Contract-First Shared Models**: PASS WITH NOTE. API boundary uses shared DTOs. Existing `WorkoutDto` is currently a class instead of record, which is a known implementation deviation to track for future hardening.
- **Repository Pattern**: PASS. Controllers delegate data access through `ISportsClubRepository` and async methods.
- **Simplicity First (YAGNI)**: PASS. In-memory repository and explicit CORS list are consistent with current scope.
- **.NET Modern Practices**: PASS. Projects target .NET 10 with nullable and implicit usings enabled.

**Gate Result**: PASS. No blocking constitution violations for planning documentation.

## Project Structure

### Documentation (this feature)

```text
specs/001-current-implementation-spec/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── schedule-api.yaml
└── tasks.md             # Created later by /speckit.tasks
```

### Source Code (repository root)

```text
SportsClub.Api/
├── Controllers/
├── Data/
├── Entities/
├── Extensions/
└── Repositories/

SportsClub.SharedModels/
└── Dtos/

SportsClub.Web/
├── Layout/
├── Pages/
├── Services/
└── Shared/
```

**Structure Decision**: Keep the existing three-project web application structure. This directly aligns with constitution layering and minimizes implementation risk.

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| Shared-model rule preference (`record`) currently not fully met for one DTO (`WorkoutDto` class) | Reflects current implementation baseline being documented in this feature | Blocking planning on this would prevent documenting the existing system; remediation can be tracked as a follow-up implementation task |

## Post-Design Constitution Check

- **Layered Architecture**: PASS. Artifacts preserve existing API/Web/SharedModels boundaries.
- **Contract-First Shared Models**: PASS WITH NOTE. Contract docs model shared DTO boundaries; existing `WorkoutDto` class deviation remains documented.
- **Repository Pattern**: PASS. Plan and contracts retain repository-mediated data retrieval.
- **Simplicity First (YAGNI)**: PASS. No new persistence/auth complexity added.
- **.NET Modern Practices**: PASS. No design decision conflicts with .NET 10 practices.

**Post-Design Gate Result**: PASS.
