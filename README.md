# Reminder API

A .NET 8 REST API for managing reminders with email notifications via Brevo.

## Getting started

Prerequisites:
- .NET 8 SDK
- A Brevo API key configured in `appsettings.json` (see `example.appsettings.json` for required settings)

Run locally:
1. Open the solution in Visual Studio 2022 or use the command line.
2. Restore and build: `dotnet restore && dotnet build`
3. Start the API: `dotnet run --project ./` (or press F5 in Visual Studio)
4. The API listens on the ports configured in `appsettings.json`

API usage:
- Use the `RemindersController` endpoints to create and manage reminders.
- A background worker (`BackgroundServices/ReminderWorker.cs`) checks for due reminders and triggers `BrevoEmailService` to send emails.

## Design & approach

- Layered architecture: Controllers -> Services -> Repositories to keep responsibilities separated and testable.
- Dependency injection: All services and repositories are registered in `Program.cs` for loose coupling and easier testing.
- Background processing: A hosted service polls for due reminders and delegates sending to `IEmailService`.
- DTOs & mapping: DTOs separate API contracts from domain models; `ReminderResponseMapper` handles mapping.
- External integration: `BrevoEmailService` encapsulates Brevo-specific logic; swap implementations of `IEmailService` to change providers.

## Configuration

- Store Brevo credentials and provider settings in `appsettings.json` or use user secrets for development.
- Configure logging and ports in `appsettings.json` as needed.

## Notes

- This README is intentionally concise. For implementation details, review `Program.cs`, `Controllers/RemindersController.cs`, `Services/ReminderService.cs`, `Repositories/ReminderRepository.cs`, and `BackgroundServices/ReminderWorker.cs`.
