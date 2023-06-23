using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private EcommerceShopContext _context = new EcommerceShopContext();  
        public IActionResult Index()
        {
            var list = _context.News.OrderByDescending(x => x.Id).ToList();
            return View(list);
        }

        public IActionResult Add() {
            ViewBag.allImages = UploadController.getAllImages();
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.allImages = UploadController.getAllImages();
            var data = _context.News.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(New model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 3;
                _context.News.Add(model);
                _context.SaveChanges();
                return Redirect("/admin/news");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(New model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 3;
                _context.News.Update(model);
                _context.SaveChanges();
                return Redirect("/admin/news");
            }

            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _context.News.Find(id);
            if (item != null)
            {
                _context.News.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult ToogleActive(int id)
        {
            var item = _context.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.News.Update(item);
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
                var item = _context.News.Find(Int32.Parse(id));
                if (item != null)
                {
                    _context.News.Remove(item);
                }
            }
            _context.SaveChanges();

            return Json(new { success = true });
        }

        
    }
}
