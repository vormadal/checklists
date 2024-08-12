using Domain.Repositories;

namespace Persistence.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ChecklistDbContext _context;

    public UnitOfWork(ChecklistDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
