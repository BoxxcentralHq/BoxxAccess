# CLAUDE.md - Core Project Context & Agent Constraints

## 🎯 Project Overview
* **What:** BoxxAccess — a local-first access-control platform. Runs at a single venue,
  manages members/devices/access events, and syncs with BoxxCentral. It talks to a
  physical ZKTeco ProFace biometric terminal.
* **Tech Stack:** .NET 8, ASP.NET Core (MVC Controllers), EF Core + SQLite, xUnit, Clean
  Architecture. `ui/` is a separate Next.js app with its own conventions (see
  `ui/AGENTS.md`) — not covered by the .NET stack below.
* **Core Principle:** Prioritize **simplicity and clarity** over clever abstractions. Do
  the minimum required to solve the task. This is safety-adjacent software (it can touch
  door hardware and biometric data) — when simplicity and caution conflict, caution wins.

## 🚫 Hard Boundaries & Safety Constraints
Violating these is a design bug, not a style nit.

* **Only `BoxxAccess.Zkteco` may reference the ZKTeco SDK.** No other project, including
  `BoxxAccess.Api`, may take a dependency on vendor types.
* **The browser UI never talks to the terminal directly.** Every device interaction goes
  through `BoxxAccess.Api`.
* **`BoxxAccess.DeviceProbe` is read-only.** It connects, reads identity/status, listens
  for events, and disconnects. It must never create users, edit access policies, enrol
  biometrics, or unlock doors. If a task asks for more than that from this tool, stop and
  confirm with the user first — this constraint is intentional, not an oversight.
* **Never fake a successful device connection.** If the ZKTeco SDK isn't available, the
  adapter throws a clear error. It does not simulate success — that would hide real
  hardware problems from whoever is running the probe.
* **Biometric data (face templates, event photos) is sensitive.** Collect, retain,
  export, and delete it only under an approved policy. Never log it. Never log secrets
  (device comm passwords, external API credentials) either — see `docs/security.md`.

## 🛠️ Critical Commands
Execute these using standard terminal execution tools. Do not guess commands.
* **Restore:** `dotnet restore`
* **Build:** `dotnet build`
* **Run Api:** `dotnet run --project src/BoxxAccess.Api`
* **Run DeviceProbe:** `dotnet run --project tools/BoxxAccess.DeviceProbe`
* **Run Tests:** `dotnet test`
* **Single Test:** `dotnet test --filter "FullyQualifiedName~<Name>"`
* **Format & Lint:** `dotnet format`
* **Add EF Migration:** `dotnet ef migrations add <Name> --project src/BoxxAccess.Infrastructure --startup-project src/BoxxAccess.Api`

## 🧠 Behavior & Engineering Discipline
* **Think First:** Analyze the file structure and relevant layer before writing code.
  Explicitly state assumptions if requirements are ambiguous.
* **Surgical Edits:** Modifying code must be precise. Fix only what is requested. Never
  refactor or touch adjacent, working code to make it "cleaner" unless requested.
* **No Ghost Dependencies:** When deleting or refactoring code, aggressively remove its
  unused usings, variables, and NuGet package references.
* **Deterministic Tools Only:** Never manually reformat or fix style by hand. Always
  defer to `dotnet format`.
* **Reuse Over Duplication:** Before writing a new helper, check whether Domain or
  Application already expresses the rule you need. The read-only device-probe flow
  (connect → read identity → listen for one event → disconnect) is implemented once in
  `BoxxAccess.Application` and reused by both the `DeviceProbe` tool and, later, the API
  — if two entry points need the same orchestration, that orchestration belongs in
  Application, and the entry points stay thin.
* **No Premature Abstraction:** Don't add a factory, strategy, or plugin point for a
  single implementation "in case we need it later." Add the abstraction when a second
  real use case shows up, not before.
* **No Fabricated Device Behavior:** Never invent how the terminal behaves. If it's not
  confirmed in `docs/device-setup.md`, say so instead of guessing.

## 📐 Code Style & Conventions
* Match existing patterns in the codebase even if you prefer an alternative style.
* **Nullable reference types are strict.** Never suppress a nullability warning with `!`
  unless you've actually proven non-null — prefer restructuring so the compiler can tell.
* Keep methods small, modular, and single-purpose (< 50 lines preferred).
* **No comments**, except a rare one-liner for a genuinely non-obvious *why* (a vendor
  SDK quirk, a workaround for a specific hardware bug). Never comment *what* the code
  does — name things well enough that it's obvious. Never reference the current task or
  a ticket in code — that belongs in the commit message.
* File-scoped namespaces, one type per file, filename matches the type name.
* **Constructor injection only.** No service locators, no static singletons for
  application state.
* **Async all the way down.** Suffix async methods with `Async`, accept and propagate
  `CancellationToken` on anything that does I/O.
* **Records for immutable data** — DTOs, value objects, query results. Reach for a
  `class` only when something has identity or mutable state (entities, EF Core-tracked
  types).
* **Interfaces live where they're consumed, not where they're implemented.** A
  repository interface lives in `Application` because Application is what calls it;
  `Infrastructure` implements it. Same pattern for `IAccessTerminalClient` — the
  interface is in Application, `Zkteco` implements it.

## 🗂️ Project Structure & Architecture

Clean Architecture, dependencies point inward only. An inner layer never references an
outer one — if you find yourself wanting to `using` an outer-layer namespace from an
inner one, the abstraction belongs in the inner layer instead.

```text
Browser UI  ->  BoxxAccess.Api  ->  BoxxAccess.Application  ->  BoxxAccess.Domain
                      |                     ^
                      v                     |
          BoxxAccess.Infrastructure   BoxxAccess.Contracts
                      |
          BoxxAccess.Zkteco  ->  ProFace terminal (vendor SDK)
```

| Project | Responsibility | May depend on |
|---|---|---|
| `BoxxAccess.Domain` | Entities, enums, value objects. Pure business rules. | nothing |
| `BoxxAccess.Application` | Use cases and interfaces (repositories, device client, queue). | Domain |
| `BoxxAccess.Contracts` | Request/response DTOs for the local API. | nothing (or Domain, for shared enums only) |
| `BoxxAccess.Infrastructure` | EF Core/SQLite persistence, resilient sync queue, config binding. | Application, Domain |
| `BoxxAccess.Zkteco` | The only place the ZKTeco SDK is referenced. | Application, Domain |
| `BoxxAccess.Api` | ASP.NET Core host: controllers, auth, background workers, composition root. | Application, Contracts, Infrastructure, Zkteco |
| `BoxxAccess.DeviceProbe` | Manual diagnostic console tool. Thin host around an Application use case. | Application, Zkteco |

Key locations:
* Domain entities → `src/BoxxAccess.Domain/Entities/`
* Application use cases & interfaces → `src/BoxxAccess.Application/` (`Abstractions/`, `DeviceDiagnostics/`)
* API controllers → `src/BoxxAccess.Api/Controllers/`
* EF Core persistence → `src/BoxxAccess.Infrastructure/Persistence/`
* Vendor SDK boundary → `src/BoxxAccess.Zkteco/` (only project allowed to reference it)
* Tests → `tests/`, one project per `src/` project, fakes under `Fakes/`
* `ui/` → separate Next.js app, see `ui/AGENTS.md`

Full tree:
```text
BoxxAccess/
  BoxxAccess.sln
  Directory.Build.props        # shared TFM/nullable/analyzer settings
  Directory.Packages.props     # centrally-managed NuGet versions
  .editorconfig
  src/
    BoxxAccess.Domain/
      Entities/
      Enums/
      ValueObjects/
    BoxxAccess.Application/
      Abstractions/             # interfaces: IAccessTerminalClient, I*Repository, I*Queue
      DeviceDiagnostics/        # the reusable probe use case
      DependencyInjection/
    BoxxAccess.Contracts/
      Devices/
      Health/
    BoxxAccess.Infrastructure/
      Persistence/
        Configurations/
        Repositories/
      Queue/
      DependencyInjection/
    BoxxAccess.Zkteco/
    BoxxAccess.Api/
      Controllers/
      Program.cs
  tests/
    BoxxAccess.Domain.Tests/
    BoxxAccess.Application.Tests/
      Fakes/                    # test doubles live here, never in src/
    BoxxAccess.Infrastructure.Tests/
    BoxxAccess.Api.Tests/
  tools/
    BoxxAccess.DeviceProbe/
  ui/                            # separate Next.js app — see ui/AGENTS.md
```

## 🧪 Testing
* xUnit, one test project per `src/` project, same layer boundaries apply to tests.
* Name tests `Should_<expected behavior>_When_<condition>`.
* Test doubles (fakes/stubs) live under `Fakes/` in the relevant test project — never
  shipped inside a production assembly.
* Prefer a hand-written fake over a mocking framework where the interface is small
  enough — it's more readable and doesn't hide behavior behind setup calls. Reach for
  Moq only when hand-writing the fake would be genuinely more code than it's worth.
* Infrastructure tests run against EF Core's InMemory provider unless the behavior under
  test is SQLite-specific, in which case use a real SQLite `:memory:` connection.

## 📝 Git Conventions
Conventional Commits, matching the sibling `boxxcentral` repo: `feat:`, `fix:`, `chore:`,
`refactor:`, `docs:`. Subject line describes the *why*/outcome, not a changelog of files
touched.

## ⚠️ Anti-Patterns to Avoid
* Business logic in `Infrastructure` or `Api` — they orchestrate and persist, they don't
  decide.
* Raw SQL or EF Core types leaking outside `Infrastructure`.
* Swallowed exceptions (empty `catch`, or `catch (Exception)` that just logs and
  continues as if nothing happened). Let unexpected failures surface.
* Any new project referencing the ZKTeco SDK other than `BoxxAccess.Zkteco`.
* Committing vendor SDK DLLs, device credentials, or `appsettings.Production.json`
  (already covered by `.gitignore` — don't fight it).
