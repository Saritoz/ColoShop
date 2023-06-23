using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index(string searchTxt, int? page)
        {
            var pageSize = 10;
            page ??= 1;
            var list = _context.Posts.OrderByDescending(x => x.Id);
            List<Post>? items;

            if (!string.IsNullOrEmpty(searchTxt))
            {
                list = (IOrderedQueryable<Post>)list.Where(x => x.Title.Contains(searchTxt));
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
            return View();
        }

        public IActionResult Edit(int id)
        {
            var data = _context.Posts.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 3;
                _context.Posts.Add(model);
                _context.SaveChanges();
                return Redirect("/admin/post");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 3;
                _context.Posts.Update(model);
                _context.SaveChanges();
                return Redirect("/admin/news");
            }

            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.Posts.Find(id);
            if (item != null)
            {
                _context.Posts.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult ToogleActive(int id)
        {
            var item = _context.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.Posts.Update(item);
                _context.SaveChanges();
                return Json(new { currentActive = item.IsActive, success = true });
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
                var item = _context.Posts.Find(Int32.Parse(id));
                if (item != null)
                {
                    _context.Posts.Remove(item);
                }
            }
            _context.SaveChanges();

            return Json(new { success = true });
        }


    }
}
