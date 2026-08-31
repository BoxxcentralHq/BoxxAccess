namespace BoxxAccess.Contracts.Devices;

public sealed record DeviceStatusResponse(
    string SerialNumber,
    string FirmwareVersion,
    string SdkVersion,
    DeviceStatusEventResponse? LastEvent);
