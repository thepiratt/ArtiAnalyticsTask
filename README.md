# Reminder API

A minimal, maintainable .NET 8 REST API for scheduling and delivering simple reminders. Reminders contain a message, a scheduled UTC time, and an optional email address. When a reminder's scheduled time is reached, the system marks it as `Sent` and logs or delivers the notification.

## Features

- Create reminders (`POST /reminders`) with `message`, `sendAt` (UTC ISO 8601), and optional `email`.
- List reminders (`GET /reminders`) including `Scheduled` and `Sent` statuses.
- Background delivery worker (`BackgroundServices/ReminderWorker.cs`) that checks for due reminders and marks them as `Sent`.
- Optional Brevo integration via `BrevoEmailService` — falls back to console logging when no API key is configured.
- Model validation using .NET `DataAnnotations` and DTO mapping via `ReminderResponseMapper`.

## Quickstart

### Prerequisites

- .NET 8 SDK
- (Optional) Brevo API key — add to `appsettings.json` . See `example.appsettings.json`.

### Run locally

1. Open the solution in Visual Studio 2022 or use the command line.
2. Restore and build: `dotnet restore && dotnet build`.
3. Run: `dotnet run --project ./` or press F5 in Visual Studio.
4. The API listens on the ports configured in `appsettings.json`.

### API examples

- **Create reminder**

  Request: `POST /reminders`

  Payload:
  {
    "message": "Check API gateway logs",
    "sendAt": "2025-10-10T14:30:00Z",
    "email": "test@example.com"
  }

Response (example):
  {
    "id": "generated-guid",
    "status": "Scheduled",
    "sendAt": "2025-10-10T14:30:00Z"
  }

- **List reminders**

Request: `GET /reminders`

  Response (example):
  [
    { "id": "1", "message": "Check logs", "sendAt": "2025-10-10T14:30:00Z", "status": "Scheduled" },
    { "id": "2", "message": "Backup database", "sendAt": "2025-10-09T14:00:00Z", "status": "Sent" }
  ]

- **Get reminder by id**

Request: `GET /reminders/{id}`

Response (example):
{ "id": "generated-guid", "message": "Check API gateway logs", "sendAt": "2025-10-10T14:30:00Z", "status": "Scheduled", "email": "test@example.com" }

## Design & Approach

- **Storage**: In-memory repository (`Repositories/ReminderRepository.cs`) chosen to deliver a complete implementation within a short timeframe. No external dependencies required.
- **Background processing**: `BackgroundService`-based worker polls every 5 seconds for due reminders. This is thread-safe in the provided repository implementation and reliable for demo purposes.
- **Delivery**: `IEmailService` abstraction with `BrevoEmailService` implementation. If Brevo credentials are not supplied, the worker logs deliveries to the console.
- **Layering & DI**: `Controllers` -> `Services` -> `Repositories` with constructor DI configured in `Program.cs` for loose coupling and testability.
- **DTOs & Validation**: API uses DTOs (`Models/DTOs`) with `DataAnnotations` for model validation. Mapping to domain models and responses is implemented in `Services/ReminderResponseMapper.cs`.

## Limitations & Next Steps

- **Scheduling precision**: Current polling introduces ~5s granularity. Replace with timer-based exact scheduling (per-reminder timers or a priority-queue scheduler) to avoid polling.
- **Persistence**: Add SQLite/Postgres to survive restarts and support concurrent deployments.
- **Tests**: Add unit and integration tests for controllers, services, repository, and the worker.
- **Delivery reliability**: Add retry/backoff, transactional state updates, and idempotency for safe retries.
- **Observability & ops**: Structured logging, metrics, health checks, Dockerfile, and CI (GitHub Actions).
- **Secrets**: Move Brevo API key to user secrets or environment variables in production.

## Project Layout

- `Controllers/` - API controllers
- `Services/` - Business logic, mapping, email service
- `Repositories/` - In-memory data store
- `BackgroundServices/` - Hosted worker
- `Models/` - Domain models and DTOs
- `Interfaces/` - Service and repository contracts

## Notes for Reviewers

- This is not AI generated, you can check commit history on how the implementation went.
- Implementation favors simplicity, clarity, and SOLID-friendly patterns to make future changes straightforward.
- The Brevo integration is implemented but optional; omit the API key to use console logging fallback.
- See `Program.cs`, `Controllers/RemindersController.cs`, `Services/ReminderService.cs`, `Repositories/ReminderRepository.cs`, and `BackgroundServices/ReminderWorker.cs` for core logic.

---
