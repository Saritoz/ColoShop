using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenAdminController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ColoShop.Models.Admin model)
        {
            if (ModelState.IsValid)
            {
                var item = _context.Admins.SingleOrDefault(x => x.Username == model.Username);
                if (item != null && BCrypt.Net.BCrypt.Verify(model.Password, item.Password))
                {
                    HttpContext.Session.SetString("admin", item.Username);
                    return Redirect("/admin");
                }
            }
            return Redirect("/admin/authenAdmin");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(ColoShop.Models.Admin model)
        {
            if (ModelState.IsValid)
            {
                var item = new ColoShop.Models.Admin();
                item.Username = model.Username;
                item.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

                _context.Admins.Add(item);
                _context.SaveChanges();
                return Redirect("/admin/authenAdmin");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin");
            return Redirect("/admin");
        }
    }


}
