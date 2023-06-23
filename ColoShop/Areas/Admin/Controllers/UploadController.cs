using Microsoft.AspNetCore.Mvc;

namespace ColoShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UploadController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult UploadImg(IFormFile upload)
        {
            Console.WriteLine(upload);
            if (upload != null && upload.Length > 0)
            {
                var extension = upload.FileName.Split('.')[1];
                var fileName = DateTime.Now.ToString("ddMMyyyyhms") + GenerateRandomString(6) + '.' +extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" ,"uploads", "images", fileName);
                Console.WriteLine(path);
                var stream = new FileStream(path, FileMode.Create);
                upload.CopyToAsync(stream);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult UploadExplorer()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "images"));
            ViewBag.allImages = dir.GetFiles();
            return View();
        }

        public IActionResult GetImagesName()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "images"));
            List<FileInfo> imageFiles = new(dir.GetFiles());
            List<string> fileNameList = new();
            foreach (var file in imageFiles)
            {
                fileNameList.Add(file.Name);
            }
            return Json( new { data=fileNameList});
        }

        public static List<FileInfo> getAllImages()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "images"));
            List<FileInfo> imageFiles = new(dir.GetFiles());
            return imageFiles;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }

        public IActionResult UploadIcon(IFormFile upload)
        {
            Console.WriteLine(upload);
            if (upload != null && upload.Length > 0)
            {
                var extension = upload.FileName.Split('.')[1];
                var fileName = DateTime.Now.ToString("ddMMyyyyhms") + GenerateRandomString(6) + '.' + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "icons", fileName);
                
                var stream = new FileStream(path, FileMode.Create);
                upload.CopyToAsync(stream);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult GetIconsName()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "icons"));
            List<FileInfo> imageFiles = new(dir.GetFiles());
            List<string> fileNameList = new();
            foreach (var file in imageFiles)
            {
                fileNameList.Add(file.Name);
            }
            return Json(new { data = fileNameList });
        }
    }
}
