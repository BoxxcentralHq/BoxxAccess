using BoxxAccess.Domain.Entities;
using BoxxAccess.Domain.ValueObjects;

namespace BoxxAccess.Application.DeviceDiagnostics;

public sealed record DeviceProbeResult(DeviceIdentity Identity, AccessEvent? FirstEvent);
