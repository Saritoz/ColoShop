using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Controllers
{
    public class NewsController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index()
        {
            var items = _context.News.OrderByDescending(x => x.Id).Where(x => x.IsActive).ToList();
            return View(items);
        }
    }
}
