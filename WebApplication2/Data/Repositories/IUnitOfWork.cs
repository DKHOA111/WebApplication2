namespace WebApplication2.Data.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        IBookRepository Book { get; }
        //ICartRepository Cart { get; }
        //IApplicationUser ApplicationUser { get; }
        // IOrderHeaderRepository OrderHearder { get; }
        // IOrderDetailRepository OrderDetail { get; }

        void Save();
    }
}
