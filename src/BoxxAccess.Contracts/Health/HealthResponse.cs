namespace BoxxAccess.Contracts.Health;

public sealed record HealthResponse(string Status, DateTimeOffset CheckedAtUtc);
