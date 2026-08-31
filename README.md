# BoxxAccess

BoxxAccess is a local-first access-control platform for managing members, devices, access events, and synchronisation with external business systems such as BoxxCentral.

It runs at a venue on Windows, connects to access-control hardware over the local network, and provides a browser UI for staff.

## Repository layout

```text
src/       Production .NET projects
tests/     Automated unit and integration tests
tools/     Manual operational tools, including the safe device connection probe
ui/        Local browser user interface
docs/      Architecture, setup, and security documentation
scripts/   Installation and operational scripts
```

## Architecture

```text
Browser UI -> local BoxxAccess API -> application services -> device/cloud adapters
```

Only `BoxxAccess.Zkteco` may reference the ZKTeco SDK. The browser UI never connects directly to a terminal.

## First milestone

`tools/BoxxAccess.DeviceProbe` will provide a read-only connection test for a ProFace terminal. It must not create users, edit access policies, enrol biometrics, or unlock doors.

See [docs/architecture.md](docs/architecture.md) and [docs/device-setup.md](docs/device-setup.md).
