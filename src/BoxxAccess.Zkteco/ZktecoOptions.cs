namespace BoxxAccess.Zkteco;

public sealed class ZktecoOptions
{
    public const string SectionName = "Zkteco";

    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string CommPassword { get; set; } = string.Empty;
}
