namespace BoxxAccess.Domain.ValueObjects;

public sealed record DeviceIdentity(string SerialNumber, string FirmwareVersion, string SdkVersion);
