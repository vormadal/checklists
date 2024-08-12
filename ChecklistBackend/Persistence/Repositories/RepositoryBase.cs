using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories;

internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DbSet<T> _set => _context.Set<T>();
    protected readonly ChecklistDbContext _context;

    public RepositoryBase(ChecklistDbContext context)
    {
        _context = context;
    }

    public virtual IQueryable<T> FindAll(CancellationToken token = default)
    {
        return _set.AsNoTracking();
    }

    public virtual async Task<T?> FindById(int id, CancellationToken token = default)
    {
        return await _set.FindAsync(new object[] { id }, token);
    }

    public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _set.Where(expression).AsNoTracking();
    }

    public virtual async Task<T> Create(T entity, CancellationToken token = default)
    {
        var result = await _set.AddAsync(entity, token);
        return result.Entity;
    }

    public virtual async Task<IEnumerable<T>> Create(IEnumerable<T> entities, CancellationToken token = default)
    {
        await _set.AddRangeAsync(entities, token);
        return entities;
    }

    public virtual Task<T> Update(T entity, CancellationToken token = default)
    {
        var result = _set.Update(entity);
        return Task.FromResult(result.Entity);
    }

    public virtual Task<IEnumerable<T>> Update(IEnumerable<T> entities, CancellationToken token = default)
    {
        _set.UpdateRange(entities);
        return Task.FromResult(entities);
    }

    public virtual Task Delete(T entity, CancellationToken token = default)
    {
        _set.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task Delete(IEnumerable<T> entities, CancellationToken token = default)
    {
        _set.RemoveRange(entities);
        return Task.CompletedTask;
    }
}
