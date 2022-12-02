using WebApplication2.Models;

namespace WebApplication2.Data.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book book);
    }
}
