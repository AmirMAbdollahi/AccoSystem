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

    public virtual bool Insert(TEntity entity)
    {
        try
        {
            _dbSet.Add(entity);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public virtual bool Update(TEntity entity)
    {
        try
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
        catch 
        {
            return false;
        }
        
    }

    public virtual bool Delete(TEntity entity)
    {
        try
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public virtual bool Delete(object id)
    {
        try
        {
            Delete(GetById(id));
            return true;
        }
        catch 
        {
            return false;
        }
    }
    
}