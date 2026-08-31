using BoxxAccess.Application.Abstractions;
using BoxxAccess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxxAccess.Infrastructure.Persistence.Repositories;

public sealed class MemberRepository(BoxxAccessDbContext dbContext) : IMemberRepository
{
    public Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        dbContext.Members.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

    public Task<Member?> GetByDeviceUserIdAsync(string deviceUserId, CancellationToken cancellationToken) =>
        dbContext.Members.FirstOrDefaultAsync(m => m.DeviceUserId == deviceUserId, cancellationToken);

    public async Task AddAsync(Member member, CancellationToken cancellationToken) =>
        await dbContext.Members.AddAsync(member, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);
}
