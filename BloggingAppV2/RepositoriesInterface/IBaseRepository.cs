using System.Linq.Expressions;

namespace BloggingApp.Web.RepositoriesInterface;

public interface IBaseRepository<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, bool trackChanges);
    void Create(T entity);
    void Delete(T entity);
    void Update(T entity);
}