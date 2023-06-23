using ColoShop.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Areas.Products.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
