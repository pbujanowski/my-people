using System.Linq.Expressions;

namespace MyPeople.Services.Images.Application.Repositories;

public interface IRepository<TEntity>
{
    TEntity Create(TEntity entity);

    IQueryable<TEntity> FindAll();

    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> condition);

    TEntity Update(TEntity entity);

    TEntity Delete(TEntity entity);

    void Detach(TEntity entity);

    Task SaveChangesAsync();
}