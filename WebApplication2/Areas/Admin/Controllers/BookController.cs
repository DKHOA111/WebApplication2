using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Data.Repositories;
using WebApplication2.Data.ViewModels;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BookController : Controller
    {

        private IUnitOfWork _unitofWork;
        private IWebHostEnvironment _hostingEnvironment;

        public BookController(IUnitOfWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }


    

        #region APICALL
        public IActionResult AllBooks()
        {
            var books = _unitofWork.Book.GetAll(includeProperties: "Category");
            return Json(new { data = books });
        }
        #endregion

        public IActionResult Index()
        {
            //BookVM bookVM = new BookVM)
            //bookVM.Books = _unitofWork.Book.GetAll();
            return View();

        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //return View();
        //}
        //[HttpPost]|
        //[validateAntiForgeryToken]
        //public IActionResult create(Category category)
        //{

        //if (ModelState.IsValid) {
        // _unitofWork.Category.Add (category)
        // _unitofWork.Save();

        // TempData["success"]= "Category Created Donel";
        //return RedirectToAction ("Index");
        //}
        //return view(category);

        //}

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            BookVM vm = new BookVM()
            {
                Book = new(),
                Categories = _unitofWork.Category.GetAll().Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })

            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Book = _unitofWork.Book.GetT(x => x.Id == id);
                if (vm.Book == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);

                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(BookVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = String.Empty;
                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "bookImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    if (vm.Book.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {

                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Book.ImageUrl = @"bookImage\" + fileName;
                }
                if (vm.Book.Id == 0)
                {

                    _unitofWork.Book.Add(vm.Book);
                    TempData["success"] = "Product Created Donel";
                }
                else
                {
                    _unitofWork.Book.Update(vm.Book);
                    TempData["success"] = "Product Update Donel";
                }

                _unitofWork.Save();
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");
        }




        [HttpGet]

        //public IActionresult Delete(int? id)
        //{
        //  if (id == null || id == 0)
        //{
        //  return NotFound ();

        //}
        //var category =_unitofWork.Category.GetT(x => x.Id == id);
        //if (Category = null)
        //{
        //  return NotFound ();
        //}
        //  return View(category);
        //}

        #region DeleteAPICALL
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var book = _unitofWork.Book.GetT(x => x.Id == id);
            if (book == null)
            {
                return Json(new { success = false, message = "Error in Fetching Data" });

            }
            else
            {
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, book.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitofWork.Book.Delete(book);
                _unitofWork.Save();
                return Json(new { success = true, message = "Book Deleted" });
            }
        }

        #endregion
    }
}
