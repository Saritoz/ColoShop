using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Controllers
{
    public class OverviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
