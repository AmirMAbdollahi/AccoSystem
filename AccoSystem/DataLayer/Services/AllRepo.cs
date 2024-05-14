using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AccoSystem.DataLayer.Services;

public class AllRepo<TEntity> where TEntity:class
{

    private AccoSystemDbContext _context;
    private DbSet<TEntity> _dbSet;
    
    public AllRepo(AccoSystemDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? where=null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (where != null)
        {
            query = query.Where(where);
        }

        return query.ToList();
    }
    public virtual TEntity GetById(object id)
    {
        return _dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void Delete(TEntity entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
    }

    public virtual void Delete(object id)
    {
        Delete(GetById(id));
    }
    
}