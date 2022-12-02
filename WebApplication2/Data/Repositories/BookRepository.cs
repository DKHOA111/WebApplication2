using WebApplication2.Models;

namespace WebApplication2.Data.Repositories
{
	public class BookRepository : Repository<Book>, IBookRepository
	{
		private ApplicationDbContext _context;
		public BookRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}
		public void Update(Book book)
		{
			var bookDb = _context.Books.FirstOrDefault(x => x.Id == book.Id);
			if (bookDb != null)
			{
				bookDb.Name = book.Name;
				bookDb.Author = book.Author;
				bookDb.Description = book.Description;
				bookDb.Price = book.Price;
				if (book.ImageUrl != null)
				{
					bookDb.ImageUrl = book.ImageUrl;
				}

			}
		}
	}
}
