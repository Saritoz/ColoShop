using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index(string searchTxt, int? page)
        {
            var pageSize = 10;
            page ??= 1;
            var list = _context.Products.OrderByDescending(x => x.Id).Include(p => p.IdCategoryProductNavigation);
            List<Product>? items;

            if (!string.IsNullOrEmpty(searchTxt))
            {
                list = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Product, ProductCategory?>)list.Where(x => x.Name.Contains(searchTxt));
                ViewBag.searchTxt = searchTxt;
            }

            int totalPage = list.Count() % pageSize == 0 ? list.Count() / pageSize : list.Count() / pageSize + 1;
            items = list.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.TotalPage = totalPage;
            return View(items);
        }

        public IActionResult Add()
        {
            IEnumerable<ProductCategory> productCategories = _context.ProductCategories.ToList();
            ViewBag.ProductCategory = new SelectList(productCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            //var category = _context.ProductCategories.Find(product.IdCategoryProduct);
            //if (category != null)
            //    product.IdCategoryProductNavigation = category;

            //foreach (var entry in ModelState.Values)
            //{
            //    if (entry.Errors.Any())
            //    {
            //        foreach (var error in entry.Errors)
            //        {
            //            string errorMessage = error.ErrorMessage;
            //        }
            //    }
            //}

            if (ModelState.IsValid)
            {
                product.IsHome = false;
                _context.Products.Add(product);
                _context.SaveChanges();
                return Redirect("/admin/products");
            }
            IEnumerable<ProductCategory> productCategories = _context.ProductCategories.ToList();
            ViewBag.ProductCategory = new SelectList(productCategories, "Id", "Name");
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            IEnumerable<ProductCategory> productCategories = _context.ProductCategories.ToList();
            ViewBag.ProductCategory = new SelectList(productCategories, "Id", "Name");
            var item = _context.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(model);
                _context.SaveChanges();
                return Redirect("/admin/products");
            }
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.Products.Find(id);
            if (item != null)
            {
                _context.Products.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult ToogleActive(int id)
        {
            var item = _context.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.Products.Update(item);
                _context.SaveChanges();
                return Json(new { currentActive = item.IsActive, success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult ToogleHome(int id)
        {
            var item = _context.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                _context.Products.Update(item);
                _context.SaveChanges();
                return Json(new { currentHome = item.IsHome, success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult ToogleSale(int id)
        {
            var item = _context.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                _context.Products.Update(item);
                _context.SaveChanges();
                return Json(new { currentSale = item.IsSale, success = true });
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
                var item = _context.Products.Find(Int32.Parse(id));
                if (item != null)
                {
                    _context.Products.Remove(item);
                }
            }
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}
