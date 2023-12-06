using System.Linq.Expressions;
using BloggingApp.Web.DataBase;
using BloggingApp.Web.RepositoriesInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BloggingApp.Web.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges
            ? _context.Set<T>()
            : _context.Set<T>().AsNoTracking();
    }


    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges)
    {
        return trackChanges
            ? _context.Set<T>().Where(predicate)
            : _context.Set<T>().Where(predicate).AsNoTracking();
    }


    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}