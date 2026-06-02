# Quickstart: Sports Catalog And Daily Schedule

## Prerequisites
- .NET 10 SDK installed
- HTTPS development certificates trusted for local ASP.NET Core execution

## 1. Start the API
1. Open a terminal at repository root.
2. Run:

```bash
dotnet run --project SportsClub.Api
```

3. Confirm API starts and listens on configured HTTPS endpoint.

## 2. Start the Blazor WebAssembly frontend
1. Open a second terminal at repository root.
2. Run:

```bash
dotnet run --project SportsClub.Web
```

3. Open the frontend URL shown in terminal output.

## 3. Validate User Story 1 (Browse Sports Offer)
1. Open home page (`/`).
2. Verify activity cards render with title, category, duration, and image.
3. Select an activity card and verify detail drawer opens with description and optional price.

## 4. Validate User Story 2 (Review Today's Schedule)
1. Navigate to today's schedule using the home-page button.
2. Verify lesson cards are listed for current day.
3. Select a lesson card and verify details show start time, location, duration, and description.

## 5. Validate User Story 3 (Confirm Enrollment Intent)
1. In workout details, trigger schedule/enroll action and verify success toast appears.
2. In lesson details, trigger enroll action and verify success toast appears.

## 6. Validate API Contract Endpoints
- `GET /api/Schedule/GetWorkouts`
- `GET /api/Schedule/GetLessonsBetween?dateStart=<ISO>&dateEnd=<ISO>`
- `GET /api/Schedule/GetTodaysLessons`

Use [contracts/schedule-api.yaml](./contracts/schedule-api.yaml) as the expected interface reference.
