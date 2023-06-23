using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index()
        {
            var items = _context.ProductCategories.OrderByDescending(x => x.Id).ToList();
            return View(items);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                _context.ProductCategories.Add(model);
                _context.SaveChanges();
                return Redirect("/admin/productCategory");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var item = _context.ProductCategories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                _context.ProductCategories.Update(model);
                _context.SaveChanges();
                return Redirect("/admin/productCategory");
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

        [HttpDelete]
        public IActionResult DeteleSelected(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { success = false });
            }

            string[] idArr = ids.Split(',');
            foreach (string id in idArr)
            {
                var item = _context.News.Find(Int32.Parse(id));
                if (item != null)
                {
                    _context.News.Remove(item);
                }
            }
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}
