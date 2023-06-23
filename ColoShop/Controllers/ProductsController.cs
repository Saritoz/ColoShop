using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace ColoShop.Controllers
{
    public class ProductsController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index(int? id)
        {
            CreateSession();

            var items = _context.Products.Include(p => p.IdCategoryProductNavigation).ToList();
            if (id != null)
            {
                items = items.Where(x => x.Id == id).ToList();
            }
            ViewBag.MaxPrice = items.Max(p => p.Price);
            return View(items);
        }

        public IActionResult ProdByCategory(int? id)
        {
            CreateSession();
            var items = _context.Products.Include(p => p.IdCategoryProductNavigation).ToList();
            if (id != null)
            {
                items = items.Where(x => x.IdCategoryProduct == id).ToList();
            }
            var cate = _context.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Name;
            }
            ViewBag.MaxPrice = items.Max(p => p.Price);
            return View(items);
        }

        public IActionResult Partial_ProdByIdCate(int cateid)
        {
            var items = (from _product in _context.Products
                         join _productCategory in _context.ProductCategories on _product.IdCategoryProduct equals _productCategory.Id
                         where (_product.IsHome)
                         select new
                         {
                             product = _product,
                             NameCategory = _productCategory.Name
                         }).ToList();
            //var s = _context.Products.Include(p => p.IdCategoryProductNavigation).Where(x => x.IsHome && x.IdCategoryProductNavigation != null && x.IdCategoryProductNavigation.Id == cateid).Take(12).ToList();

            return PartialView(items);
        }

        public IActionResult Partial_ProductSale()
        {
            var item = _context.Products.Include(p => p.IdCategoryProductNavigation).Where(x => x.IsHome && x.IsSale).ToList();
            return PartialView(item);
        }

        public IActionResult Detail(int id)
        {
            CreateSession();
            var item = _context.Products.Include(p => p.IdCategoryProductNavigation).FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                _context.Products.Attach(item);
                item.ViewCount += 1;
                _context.Entry(item).Property(x => x.ViewCount).IsModified = true;
                _context.SaveChanges();
            }
            return View(item);
        }

        public IActionResult Partial_StatisticTable()
        {
            var data = new ViewStatistic().GetStatisticView();
            ViewBag.OnlineUser = HttpContext.Session.GetInt32("VisitorCount");
            return PartialView(data);
        }

        public void CreateSession()
        {
            string? sessionId = HttpContext.Session.GetString("SessionId");

            if (string.IsNullOrEmpty(sessionId))
            {
                // Generate a new session identifier
                sessionId = Guid.NewGuid().ToString();

                // Store the session identifier in the session
                HttpContext.Session.SetString("SessionId", sessionId);

                // Increment the counter for new sessions
                // You can store the count in the session or any other persistent storage
                int visitorCount = HttpContext.Session.GetInt32("VisitorCount") ?? 0;
                HttpContext.Session.SetInt32("VisitorCount", visitorCount + 1);
            }
        }


    }
}
