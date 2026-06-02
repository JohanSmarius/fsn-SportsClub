# Data Model: Sports Catalog And Daily Schedule

## Entity: Workout
- Description: Catalog item representing a sports activity users can browse.
- Fields:
  - `Id` (int, required): Unique workout identifier.
  - `Title` (string, required): User-visible workout name.
  - `Description` (string, required): Detailed workout explanation.
  - `Category` (string, required): Workout classification.
  - `Image` (string?, optional): Image URL shown in cards/details.
  - `Duration` (int, required): Duration in minutes.
  - `Price` (decimal?, optional): Optional price shown in details.
- Validation rules:
  - `Title`, `Description`, and `Category` must be non-empty.
  - `Duration` should be positive.
  - `Price`, when present, should be non-negative.

## Entity: Location
- Description: Physical venue where lessons are hosted.
- Fields:
  - `Id` (int, required): Unique location identifier.
  - `Name` (string, required): Location display name.
  - `Address` (string, required): Address details.
  - `Capacity` (int, required): Maximum participant count.
  - `IsOutside` (bool, required): Indicates indoor/outdoor setting.
- Validation rules:
  - `Name` and `Address` must be non-empty.
  - `Capacity` should be greater than zero.

## Entity: Lesson
- Description: Scheduled occurrence of a workout at a specific location and time.
- Fields:
  - `Id` (int, required): Unique lesson identifier.
  - `WorkOutId` (int, required): Foreign key to `Workout.Id`.
  - `LocationId` (int, required): Foreign key to `Location.Id`.
  - `Instructor` (string, required): Instructor display value.
  - `StartDateTime` (DateTime, required): Start date-time of the lesson.
- Validation rules:
  - `WorkOutId` must reference an existing workout.
  - `LocationId` must reference an existing location.
  - `Instructor` should be present; current seed data allows empty values for some lessons.

## API Contract Models

### WorkoutDto
- Purpose: User-facing workout data for overview cards and workout details.
- Source mapping: `Workout`.
- Notes: Current implementation uses a class with mutable properties.

### LessonDto
- Purpose: User-facing scheduled lesson data that combines lesson, workout, and location fields.
- Source mapping: Joined view of `Lesson` + `Workout` + `Location`.
- Notes: Current implementation uses a positional record.

## Relationships
- One `Workout` can be referenced by many `Lesson` records.
- One `Location` can host many `Lesson` records.
- Each `Lesson` references exactly one `Workout` and one `Location`.

## State Transitions
- `Workout`: Read-only within current scope.
- `Location`: Read-only within current scope.
- `Lesson`: Read-only within current scope.
- Enrollment action: UI confirmation only; no persisted state transition exists in current implementation.
