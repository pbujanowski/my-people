using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Images.Application.Repositories;
using MyPeople.Services.Images.Infrastructure.Data;

namespace MyPeople.Services.Images.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity>(ApplicationDbContext dbContext) : IRepository<TEntity>
    where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext = dbContext;

    public TEntity Create(TEntity entity)
    {
        return _dbContext.Set<TEntity>().Add(entity).Entity;
    }

    public TEntity Delete(TEntity entity)
    {
        return _dbContext.Set<TEntity>().Remove(entity).Entity;
    }

    public IQueryable<TEntity> FindAll()
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }

    public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> condition)
    {
        return _dbContext.Set<TEntity>().Where(condition).AsNoTracking();
    }

    public TEntity Update(TEntity entity)
    {
        return _dbContext.Set<TEntity>().Update(entity).Entity;
    }

    public void Detach(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
