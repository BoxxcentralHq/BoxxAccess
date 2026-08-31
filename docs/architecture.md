# Architecture

BoxxAccess is a standalone, venue-local product. It may integrate with BoxxCentral, but must remain independently deployable and operable.

```text
Local browser UI
        |
        | HTTP to localhost
        v
BoxxAccess.Api
        |
        v
BoxxAccess.Application
   |                 |
   v                 v
ZKTeco adapter    Infrastructure
   |                 |
   v                 v
ProFace terminal  SQLite + external APIs
```

## Projects

- `BoxxAccess.Domain`: business entities, enums, and value objects. No framework or vendor dependencies.
- `BoxxAccess.Application`: use cases and interfaces. It coordinates domain rules without knowing how data or hardware is implemented.
- `BoxxAccess.Contracts`: request and response DTOs used by the local API.
- `BoxxAccess.Infrastructure`: SQLite persistence, resilient queues, configuration, and external HTTP clients.
- `BoxxAccess.Zkteco`: the hardware boundary. It owns the ZKTeco SDK/COM interop and translates terminal data into application-facing models.
- `BoxxAccess.Api`: ASP.NET Core host, local API endpoints, authentication, and background workers.
- `BoxxAccess.DeviceProbe`: a manual, read-only diagnostic executable for verifying a physical device connection.

## Boundaries

- The UI never uses the ZKTeco SDK or terminal IP directly.
- Vendor-specific types never leave `BoxxAccess.Zkteco`.
- The API depends on application interfaces; it does not depend on ZKTeco implementation details.
- The local SQLite queue preserves unsynchronised events while an external API is unavailable.
