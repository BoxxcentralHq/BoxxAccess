using BoxxAccess.Domain.Common;

namespace BoxxAccess.Domain.Entities;

public sealed class DeviceProfile : Entity
{
    public string Name { get; private set; }
    public string Host { get; private set; }
    public int Port { get; private set; }
    public string SerialNumber { get; private set; }
    public bool IsEnabled { get; private set; }

    private DeviceProfile()
    {
        Name = null!;
        Host = null!;
        SerialNumber = null!;
    }

    public DeviceProfile(string name, string host, int port, string serialNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Device name is required.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(host))
        {
            throw new ArgumentException("Device host is required.", nameof(host));
        }

        if (port is <= 0 or > 65535)
        {
            throw new ArgumentOutOfRangeException(nameof(port), port, "Port must be between 1 and 65535.");
        }

        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            throw new ArgumentException("Device serial number is required.", nameof(serialNumber));
        }

        Name = name;
        Host = host;
        Port = port;
        SerialNumber = serialNumber;
        IsEnabled = true;
    }

    public void Disable() => IsEnabled = false;

    public void Enable() => IsEnabled = true;
}
