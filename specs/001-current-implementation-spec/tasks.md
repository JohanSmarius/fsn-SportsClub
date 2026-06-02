# Tasks: Sports Catalog And Daily Schedule

**Input**: Design documents from `/specs/001-current-implementation-spec/`

**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: Automated tests are not explicitly requested in the feature specification; this task list focuses on implementation and manual validation tasks.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., [US1], [US2], [US3])
- Include exact file paths in descriptions

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Align project and documentation scaffolding for the current implementation baseline.

- [ ] T001 Verify feature pointer is set to specs/001-current-implementation-spec in .specify/feature.json
- [ ] T002 Ensure planning artifacts exist and are current in specs/001-current-implementation-spec/plan.md, specs/001-current-implementation-spec/research.md, specs/001-current-implementation-spec/data-model.md, specs/001-current-implementation-spec/quickstart.md
- [ ] T003 [P] Ensure API contract baseline is present in specs/001-current-implementation-spec/contracts/schedule-api.yaml
- [ ] T004 [P] Ensure agent context points to this feature plan in .github/copilot-instructions.md

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Baseline cross-story architecture and contract consistency before user-story refinement.

**⚠️ CRITICAL**: No user story work can begin until this phase is complete.

- [ ] T005 Audit API route consistency and response shapes against contract in SportsClub.Api/Controllers/ScheduleController.cs and specs/001-current-implementation-spec/contracts/schedule-api.yaml
- [ ] T006 [P] Audit repository abstraction coverage for required schedule/workout/location retrieval in SportsClub.Api/Repositories/ISportsClubRepository.cs and SportsClub.Api/Repositories/SportsClubInMemoryRepository.cs
- [ ] T007 [P] Audit DTO conversion boundary and null-safety assumptions in SportsClub.Api/Extensions/DtoConversions.cs and SportsClub.SharedModels/Dtos/
- [ ] T008 Align shared model contract notes with implementation in SportsClub.SharedModels/Dtos/WorkoutDto.cs and SportsClub.SharedModels/Dtos/LessonDto.cs
- [ ] T009 Confirm frontend API base address and endpoint usage alignment in SportsClub.Web/Program.cs, SportsClub.Web/Pages/Home.razor, and SportsClub.Web/Pages/TodaySchedulePage.razor

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel.

---

## Phase 3: User Story 1 - Browse Sports Offer (Priority: P1) 🎯 MVP

**Goal**: Users can browse all workouts and open workout details from the home view.

**Independent Test**: Open the home page and verify workout cards and details drawer behavior without using schedule pages.

### Implementation for User Story 1

- [ ] T010 [US1] Ensure workout catalog endpoint returns expected DTO fields in SportsClub.Api/Controllers/ScheduleController.cs
- [ ] T011 [P] [US1] Ensure workout-to-DTO field mapping is complete and stable in SportsClub.Api/Extensions/DtoConversions.cs
- [ ] T012 [P] [US1] Ensure workout DTO shape supports card/detail rendering in SportsClub.SharedModels/Dtos/WorkoutDto.cs
- [ ] T013 [US1] Refine home-page loading, success, and empty-state behavior in SportsClub.Web/Pages/Home.razor
- [ ] T014 [P] [US1] Refine workout card summary display behavior in SportsClub.Web/Shared/WorkoutCard.razor
- [ ] T015 [US1] Refine workout details drawer behavior and close flow in SportsClub.Web/Shared/WorkoutDetails.razor
- [ ] T016 [US1] Verify navigation path to schedule from home page remains available in SportsClub.Web/Pages/Home.razor

**Checkpoint**: User Story 1 should be fully functional and independently testable.

---

## Phase 4: User Story 2 - Review Today's Schedule (Priority: P2)

**Goal**: Users can view today's lessons and inspect lesson details.

**Independent Test**: Open the today schedule page directly and verify list and details behavior for lessons.

### Implementation for User Story 2

- [ ] T017 [US2] Ensure date-range schedule endpoint behavior is preserved in SportsClub.Api/Controllers/ScheduleController.cs
- [ ] T018 [P] [US2] Ensure today's date boundary logic is consistent in SportsClub.Api/Extensions/DateTimeExtensions.cs and SportsClub.Api/Controllers/ScheduleController.cs
- [ ] T019 [P] [US2] Ensure lesson composition (lesson + workout + location) is stable in SportsClub.Api/Extensions/DtoConversions.cs
- [ ] T020 [US2] Refine today schedule page loading, success, and empty-state behavior in SportsClub.Web/Pages/TodaySchedulePage.razor
- [ ] T021 [P] [US2] Refine lesson card schedule metadata display in SportsClub.Web/Shared/LessonCard.razor
- [ ] T022 [US2] Refine lesson details drawer content and close flow in SportsClub.Web/Shared/LessonDetails.razor
- [ ] T023 [US2] Ensure schedule navigation route consistency in SportsClub.Web/Layout/NavMenu.razor and SportsClub.Web/Pages/TodaySchedulePage.razor

**Checkpoint**: User Stories 1 and 2 should both work independently.

---

## Phase 5: User Story 3 - Confirm Enrollment Intent (Priority: P3)

**Goal**: Users receive clear success confirmation when triggering enrollment-related actions from details drawers.

**Independent Test**: Trigger enrollment actions from workout and lesson detail drawers and verify confirmation toast visibility and content.

### Implementation for User Story 3

- [ ] T024 [US3] Standardize popup component behavior for confirmation visibility and dismissal in SportsClub.Web/Shared/Popup.razor
- [ ] T025 [P] [US3] Align workout enrollment confirmation message behavior in SportsClub.Web/Shared/WorkoutDetails.razor
- [ ] T026 [P] [US3] Align lesson enrollment confirmation message behavior in SportsClub.Web/Shared/LessonDetails.razor
- [ ] T027 [US3] Verify confirmation interactions do not break drawer open/close behavior in SportsClub.Web/Shared/WorkoutDetails.razor and SportsClub.Web/Shared/LessonDetails.razor

**Checkpoint**: All user stories should now be independently functional.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Cross-story cleanup, documentation consistency, and end-to-end validation.

- [ ] T028 [P] Update feature documentation to reflect final behavior in specs/001-current-implementation-spec/spec.md and specs/001-current-implementation-spec/plan.md
- [ ] T029 [P] Reconcile quickstart validation steps with final implementation in specs/001-current-implementation-spec/quickstart.md
- [ ] T030 Reconcile contract examples with final API behavior in specs/001-current-implementation-spec/contracts/schedule-api.yaml
- [ ] T031 Execute full manual validation pass for US1-US3 using specs/001-current-implementation-spec/quickstart.md

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately.
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories.
- **User Stories (Phase 3+)**: Depend on Foundational phase completion.
- **Polish (Phase 6)**: Depends on completion of desired user stories.

### User Story Dependencies

- **User Story 1 (P1)**: Starts after Foundational and serves as MVP slice.
- **User Story 2 (P2)**: Starts after Foundational; can proceed in parallel with late US1 refinement if no file conflicts.
- **User Story 3 (P3)**: Starts after Foundational; depends functionally on details drawers from US1/US2 being present.

### Within Each User Story

- API/contract alignment tasks before UI refinement tasks.
- Shared model or conversion updates before component-level behavior adjustments.
- Story-level manual validation at each checkpoint before moving on.

### Parallel Opportunities

- Phase 1: T003 and T004 can run in parallel.
- Phase 2: T006 and T007 can run in parallel.
- US1: T011 and T012 can run in parallel; T014 can run in parallel with API-side checks.
- US2: T018 and T019 can run in parallel; T021 can run in parallel with schedule page refinement.
- US3: T025 and T026 can run in parallel.
- Polish: T028 and T029 can run in parallel.

---

## Parallel Example: User Story 2

```bash
# Parallel contract/data-composition tasks for US2:
Task: "T018 [US2] Ensure today's date boundary logic is consistent in SportsClub.Api/Extensions/DateTimeExtensions.cs and SportsClub.Api/Controllers/ScheduleController.cs"
Task: "T019 [US2] Ensure lesson composition (lesson + workout + location) is stable in SportsClub.Api/Extensions/DtoConversions.cs"

# Parallel UI refinement tasks for US2:
Task: "T021 [US2] Refine lesson card schedule metadata display in SportsClub.Web/Shared/LessonCard.razor"
Task: "T022 [US2] Refine lesson details drawer content and close flow in SportsClub.Web/Shared/LessonDetails.razor"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup.
2. Complete Phase 2: Foundational.
3. Complete Phase 3: User Story 1.
4. Validate US1 independently through home-page browsing and details interaction.

### Incremental Delivery

1. Setup + Foundational establish stable architecture and contract baseline.
2. Deliver US1 (MVP), validate, and demo.
3. Deliver US2, validate schedule-specific behavior independently.
4. Deliver US3, validate confirmation UX independently.
5. Execute polish phase and final end-to-end quickstart pass.

### Parallel Team Strategy

1. Team collaborates on Setup + Foundational first.
2. After foundational checkpoint:
   - Developer A: US1 refinement tasks.
   - Developer B: US2 refinement tasks.
   - Developer C: US3 confirmation UX tasks.
3. Merge stories after independent validation checkpoints.

---

## Notes

- [P] tasks indicate no direct dependency and no expected file conflict.
- [US1]/[US2]/[US3] labels provide explicit traceability to user stories.
- Tasks are written so each story can be built and validated independently.
- Automated tests are intentionally omitted because they were not explicitly requested by the specification.
