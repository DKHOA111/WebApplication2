using WebApplication2.Models;

namespace WebApplication2.Data.ViewModels
{
    public class CategoryVM
    {
        public Category Category { get; set; } = new Category();
        public IEnumerable<Category> categories { get; set; } = new List<Category>();
    }
}
