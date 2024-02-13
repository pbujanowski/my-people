using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyPeople.Services.Posts.Application.Repositories;
using MyPeople.Services.Posts.Infrastructure.Data;

namespace MyPeople.Services.Posts.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;

    protected RepositoryBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TEntity Create(TEntity entity)
    {
        return _dbContext.Set<TEntity>().Add(entity).Entity;
    }

    public TEntity Delete(TEntity entity)
    {
        return _dbContext.Set<TEntity>().Remove(entity).Entity;
    }

    public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
    {
        var deletedEntities = new List<TEntity>();
        foreach (var entity in entities)
        {
            var deletedEntity = _dbContext.Set<TEntity>().Remove(entity).Entity;
            deletedEntities.Add(deletedEntity);
        }

        return deletedEntities;
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
}