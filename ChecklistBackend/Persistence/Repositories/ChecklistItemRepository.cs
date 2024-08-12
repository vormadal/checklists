using Domain;
using Domain.Repositories;

namespace Persistence.Repositories;

internal class ChecklistItemRepository : RepositoryBase<ChecklistItem>, IChecklistItemRepository
{
    public ChecklistItemRepository(ChecklistDbContext context) : base(context)
    {
    }
}
