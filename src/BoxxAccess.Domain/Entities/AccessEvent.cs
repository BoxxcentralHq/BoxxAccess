using BoxxAccess.Domain.Common;
using BoxxAccess.Domain.Enums;

namespace BoxxAccess.Domain.Entities;

public sealed class AccessEvent : Entity
{
    public Guid? MemberId { get; private set; }
    public string DeviceSerialNumber { get; private set; }
    public VerificationMode VerificationMode { get; private set; }
    public AccessResult Result { get; private set; }
    public DateTimeOffset OccurredAt { get; private set; }
    public bool SyncedToBoxxCentral { get; private set; }

    private AccessEvent()
    {
        DeviceSerialNumber = null!;
    }

    public AccessEvent(
        string deviceSerialNumber,
        VerificationMode verificationMode,
        AccessResult result,
        DateTimeOffset occurredAt,
        Guid? memberId = null)
    {
        if (string.IsNullOrWhiteSpace(deviceSerialNumber))
        {
            throw new ArgumentException("Device serial number is required.", nameof(deviceSerialNumber));
        }

        DeviceSerialNumber = deviceSerialNumber;
        VerificationMode = verificationMode;
        Result = result;
        OccurredAt = occurredAt;
        MemberId = memberId;
    }

    public void MarkSynced() => SyncedToBoxxCentral = true;
}
