using Company.Framework.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Company.Framework.Repository;

public abstract class BaseRepository<TContext, TEntity> : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity
{
    private readonly TContext _ctx;
    private readonly DbSet<TEntity> _table;

    public BaseRepository(TContext context)
    {
        _ctx = context;
        _table = _ctx.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _table;
    }

    public List<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> expression)
    {
        return _table.Where(expression).ToList();
    }

    public TEntity? GetFirstByFilter(Expression<Func<TEntity, bool>> expression)
    {
        return _table.FirstOrDefault(expression);
    }

    public TEntity? GetById(int id)
    {
        return _table.FirstOrDefault(e => e.Id == id);
    }

    public bool Insert(TEntity entity)
    {
        _table.Add(entity);
        var affected = _ctx.SaveChanges();
        return affected > 0;
    }

    public bool Update(TEntity entity)
    {
        _table.Update(entity);
        var affected = _ctx.SaveChanges();
        return affected > 0;
    }

    public bool Delete(TEntity entity)
    {
        _table.Remove(entity);
        var affected = _ctx.SaveChanges();
        return affected > 0;
    }

    public bool DeleteById(int id)
    {
        var entity = _table.Find(id);
        if (entity != null) _table.Remove(entity);
        var affected = _ctx.SaveChanges();
        return affected > 0;
    }
}