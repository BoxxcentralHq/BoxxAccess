# ProFace device setup and connection probe

The first device target is a ZKTeco ProFace X [CH].

## Prerequisites

- A Windows PC connected to the same trusted LAN as the ProFace terminal.
- Terminal IP address, SDK communication port, and communication password.
- The SDK installed locally according to its licence. Do not commit its DLLs to this repository.
- No conflicting ZKTeco management application connected during the initial probe.

## Safe first test

`BoxxAccess.DeviceProbe` is a physical-device diagnostic, not an automated unit test. Its first version must only:

1. Connect to the configured terminal.
2. Read identity/status information such as serial number and SDK version.
3. Register for real-time attendance/access events.
4. Print a verification event after an already-enrolled face or card is presented.
5. Disconnect cleanly.

It must not modify users, templates, access groups, time zones, door relays, or device settings.

## Later phases

After the read-only test succeeds, BoxxAccess can add membership-to-device-user linking, event persistence, offline retries, and controlled user/access synchronisation.
