using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index(int? page)
        {
            int pageSize = 15;
            page ??= 1;
            var list = _context.Orders.OrderByDescending(x => x.Id).ToList();
            List<Order>? items;

            int totalPage = list.Count() % pageSize == 0 ? list.Count() / pageSize : list.Count() / pageSize + 1;
            items = list.Skip(((int)page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.currentPage = page;
            ViewBag.pageSize = pageSize;
            ViewBag.TotalPage = totalPage;
            return View(items);
        }

        public IActionResult View(int id)
        {
            var item = _context.Orders.Include(p => p.ProductOrders).ThenInclude(po => po.IdProductNavigation).FirstOrDefault(x => x.Id == id);
            return View(item);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var item = _context.Orders.Find(id);
            if (item != null)
            {
                _context.Orders.Attach(item);
                item.Status = status.ToLower();
                _context.Entry(item).Property(x => x.Status).IsModified = true;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
