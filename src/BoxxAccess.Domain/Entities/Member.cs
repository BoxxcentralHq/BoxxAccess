using BoxxAccess.Domain.Common;

namespace BoxxAccess.Domain.Entities;

public sealed class Member : Entity
{
    public string FullName { get; private set; }
    public string? ExternalReferenceId { get; private set; }
    public string? DeviceUserId { get; private set; }
    public bool IsActive { get; private set; }

    private Member()
    {
        FullName = null!;
    }

    public Member(string fullName, string? externalReferenceId = null)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Member full name is required.", nameof(fullName));
        }

        FullName = fullName;
        ExternalReferenceId = externalReferenceId;
        IsActive = true;
    }

    public void LinkToDevice(string deviceUserId)
    {
        if (string.IsNullOrWhiteSpace(deviceUserId))
        {
            throw new ArgumentException("Device user id is required.", nameof(deviceUserId));
        }

        DeviceUserId = deviceUserId;
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}
