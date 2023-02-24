using System.Linq.Expressions;

namespace Company.Framework.Repository;

public interface IRepository<TEntity>
    where TEntity : class
{
    IQueryable<TEntity> GetAll();
    List<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> expression);
    TEntity? GetFirstByFilter(Expression<Func<TEntity, bool>> expression);
    TEntity? GetById(int id);
    bool Insert(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
    bool DeleteById(int id);
}