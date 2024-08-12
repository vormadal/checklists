using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories;

internal class ChecklistRepository : RepositoryBase<Checklist>, IChecklistRepository
{
    public ChecklistRepository(ChecklistDbContext context) : base(context)
    {
    }

    public override IQueryable<Checklist> FindAll(CancellationToken token = default)
    {
        return base.FindAll(token).Include(x => x.Items);
    }

    public override IQueryable<Checklist> FindByCondition(Expression<Func<Checklist, bool>> expression)
    {
        return base.FindByCondition(expression).Include(x => x.Items);
    }
}
