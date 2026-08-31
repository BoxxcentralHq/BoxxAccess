using BoxxAccess.Domain.Entities;

namespace BoxxAccess.Application.Abstractions;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Member?> GetByDeviceUserIdAsync(string deviceUserId, CancellationToken cancellationToken);

    Task AddAsync(Member member, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
