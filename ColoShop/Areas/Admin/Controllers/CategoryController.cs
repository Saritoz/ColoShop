using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private EcommerceShopContext _context = new EcommerceShopContext();
        public IActionResult Index()
        {
            var items = _context.Categories;
            return View(items);
        }

        public IActionResult Add()
        {
            Category category = new Category { Position = 1 };
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(model);
                _context.SaveChanges();
                return Redirect("/admin/category");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var item = _context.Categories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(model);
                _context.SaveChanges();
                return Redirect("/admin/category");
            }
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.Categories.Find(id);
            if (item != null)
            {
                _context.Categories.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}