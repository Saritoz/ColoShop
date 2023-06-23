using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Controllers
{
    public class MenuController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MenuTop()
        {
            var items = _context.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }

        public IActionResult MenuLeft(string? currentCate)
        {
            var items = _context.ProductCategories.ToList();
            if (currentCate != null)
            {
                ViewBag.currentCate = currentCate;
            }
            return PartialView("_MenuLeft", items);
        }

        //public JsonResult MenuTopJson()
        //{
        //	var items = _context.Categories.OrderBy(x => x.Position).ToList();
        //	return Json(new { code = 200, data = items });

        //}

        public IActionResult MenuProductCategory()
        {
            var items = _context.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public IActionResult MenuArrivals()
        {
            var items = _context.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }

        //public IActionResult MenuArrivals1()
        //{
        //	var items = _context.ProductCategories.ToList();
        //	return PartialView("_MenuArrivals", items);
        //}
    }
}
