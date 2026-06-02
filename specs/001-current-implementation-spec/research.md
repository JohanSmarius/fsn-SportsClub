# Phase 0 Research: Sports Catalog And Daily Schedule

## Decision 1: Keep the current layered three-project architecture
- Decision: Retain separate API, WebAssembly frontend, and shared-model projects.
- Rationale: This matches the constitution, keeps boundaries clear, and avoids mixing UI and server concerns.
- Alternatives considered: Merging projects into a single app. Rejected because it would violate layering and increase coupling.

## Decision 2: Keep repository abstraction with in-memory implementation for now
- Decision: Continue using `ISportsClubRepository` and `SportsClubInMemoryRepository` as the current data access model.
- Rationale: Fits demo scope, keeps async controller flow stable, and supports future storage swap without controller changes.
- Alternatives considered: Direct seed-data access in controllers. Rejected because it breaks repository pattern and reduces replaceability.

## Decision 3: Maintain DTO conversion boundary between entities and API output
- Decision: Preserve conversion from `Lesson`, `Workout`, and `Location` entities into shared DTOs through conversion extensions.
- Rationale: Keeps API contract stable and prevents leaking internal entity models over HTTP.
- Alternatives considered: Returning entity models directly. Rejected because it violates constitution and risks breaking consumers when entities change.

## Decision 4: Keep date-range schedule retrieval as the canonical scheduling pattern
- Decision: Define schedule retrieval contract around a `dateStart` and `dateEnd` range, with today's schedule as a convenience endpoint.
- Rationale: Existing implementation already supports this and it cleanly generalizes to other time windows.
- Alternatives considered: Only exposing a fixed "today" endpoint. Rejected because it reduces flexibility and duplicates filtering logic.

## Decision 5: Document current behavior for enrollment as confirmation-only
- Decision: Treat enrollment as UI confirmation feedback with no persistence in this feature baseline.
- Rationale: Existing UI already provides toast confirmation and no server-side enrollment state is implemented.
- Alternatives considered: Adding server-side enrollment persistence now. Rejected because it expands scope beyond current implementation.

## Decision 6: Track shared DTO consistency as a follow-up improvement
- Decision: Accept current baseline where one shared DTO (`WorkoutDto`) is a class while recording future alignment to record-only preference.
- Rationale: Planning goal is to document existing behavior first; this deviation is non-blocking for current flows.
- Alternatives considered: Blocking plan completion until DTO refactor. Rejected because documentation should represent reality before refactoring.
