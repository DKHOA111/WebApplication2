using System.Linq.Expressions;

namespace WebApplication2.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        //lambda expression: x=>x.id==id, _context.book.include("categories").tolist();
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);
        T GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}
