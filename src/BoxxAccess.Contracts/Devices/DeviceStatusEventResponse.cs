namespace BoxxAccess.Contracts.Devices;

public sealed record DeviceStatusEventResponse(string VerificationMode, string Result, DateTimeOffset OccurredAt);
