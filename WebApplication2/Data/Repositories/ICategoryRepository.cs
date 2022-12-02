using WebApplication2.Models;

namespace WebApplication2.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category book);
    }
}
