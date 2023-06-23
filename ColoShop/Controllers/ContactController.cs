using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
