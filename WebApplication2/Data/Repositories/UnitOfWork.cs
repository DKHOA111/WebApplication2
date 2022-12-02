namespace WebApplication2.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IBookRepository Book { get; private set; }
        //public ICartRepository Cart { get; private set; } 

        // public IApplicationUser ApplicationUser { get; private set; }

        // public IOrderHeaderRepository OrderHeader { get; private set; }

        // public IOrderDetailRepository OrderDetail { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Book = new BookRepository(context);
            //Cart = new CartRepository(context);
            //ApplicationUser = new ApplicationRepository(context);
            //OrderHearder = new OrderHearderRepository(context);
            //OrderDetail = new OrderDetailRepository(context);


        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
