using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Data.Repositories;
using WebApplication2.Models;

namespace WebApplication2.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Book> books = _unitOfWork.Book.GetAll(includeProperties: "Category");
            return View(books);
        }




        [HttpGet]
        public IActionResult Details(int? bookId)
        {
            Cart cart = new Cart()
            {
                Book = _unitOfWork.Book.GetT(x => x.Id == bookId, includeProperties: "Category"),
                Count = 1,
                BookId = (int)bookId
            };
            return View(cart);
        }




        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public IActionResult Details(Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var claimsIdentity = (ClaimsIdentity)User.Identity;
        //        var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //        cart.ApplicationUserId = claims.Value;

        //        var cartItem = _unitOfWork.Cart.GetT(x => x.ProductId == cart.ProductId && x.ApplicationUserId == claims.Value);

        //        if (cartItem == null)
        //        {
        //            _unitOfWork.Cart.Add(cart);
        //            _unitOfWork.Save();
        //            HttpContext.Session.SetInt32("SessionCart", _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value).ToList().Count);
        //        }
        //        else
        //        {
        //            _unitOfWork.Cart.IncrementCartItem(cartItem, cart.Count);
        //            _unitOfWork.Save();
        //        }

        //    }
        //    return RedirectToAction("Index");

        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}