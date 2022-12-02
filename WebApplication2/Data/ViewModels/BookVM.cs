using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models;

namespace WebApplication2.Data.ViewModels
{
    public class BookVM
    {
        public Book Book { get; set; } = new Book();
        [ValidateNever]
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
