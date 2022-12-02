using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data.Repositories;
using WebApplication2.Data.ViewModels;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitofWork;
        public CategoryController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories = _unitofWork.Category.GetAll();
            return View(categoryVM);
        }
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Category = _unitofWork.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
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
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Category.Id == 0)
                {
                    _unitofWork.Category.Add(vm.Category);
                    TempData["success"] = "category Created Done!";
                }
                else
                {
                    _unitofWork.Category.Update(vm.Category);
                    TempData["success"] = "category Updated Done!";
                }
                _unitofWork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitofWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var category = _unitofWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitofWork.Category.Delete(category);
            _unitofWork.Save();
            TempData["success"] = "category Deleted Done!";
            return RedirectToAction("Index");
        }
    }
}
