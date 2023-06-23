using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ColoShop.Controllers
{
    public class UserController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("user");
            var item = _context.Users.Find(username);
            if (item != null)
            {
                item.Password = "????????";
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Index(User model)
        {
            if (ModelState.IsValid)
            {
                var username = model.Username;
                var existItem = _context.Users.Find(username);
                if (existItem != null)
                {
                    var password = model.Password;
                    model.Password = BCrypt.Net.BCrypt.HashPassword(password);
                    _context.Users.Update(existItem);
                    _context.SaveChanges();
                    return Redirect("/user");
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var item = _context.Users.SingleOrDefault(x => x.Username == username);
            if (item != null && BCrypt.Net.BCrypt.Verify(password, item.Password))
            {
                HttpContext.Session.SetString("user", item.Username);
                return Redirect("/");
            }
            return View();
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            var existUsername = _context.Users.Find(model.Username);
            if (existUsername != null)
            {
                ViewBag.error = "Already exist username";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var password = model.Password;
                model.Password = BCrypt.Net.BCrypt.HashPassword(password);

                _context.Users.Add(model);
                _context.SaveChanges();

                ViewData["Message"] = "Register successfully. Now you can login";
                return Redirect("/user/login");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return Json(new { success = true });
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.GetString("user") == null)
            {
                context.Result = new RedirectResult("/user/login");
            }
        }
    }
}
