using ColoShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace ColoShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private EcommerceShopContext _context = new();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Partial_CartTable()
        {
            return PartialView(ShoppingCart.Instance.Items);
        }

        [Authorize]
        public IActionResult CheckOut()
        {
            ViewBag.existItem = ShoppingCart.Instance.Items.Count > 0;
            string username = HttpContext.Session.GetString("user") ?? "";
            var existuser = _context.Users.Find(username);
            Order od = new();
            if (existuser != null)
            {
                od.CustomerName = existuser.FullName;
                od.Email = existuser.Email;
            }
            return View(od);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessCheckOut(Order order)
        {
            var code = new { success = false, code = -1 };
            if (ModelState.IsValid)
            {
                string username = HttpContext.Session.GetString("user") ?? "";
                var existuser = _context.Users.Find(username);
                if (existuser != null)
                {
                    order.Username = existuser.Username;
                }
                Random rd = new Random();
                order.CreatedDate = DateTime.Now;
                order.Quantity = ShoppingCart.Instance.Items.Sum(x => x.Quantity);
                order.TotalAmount = ShoppingCart.Instance.Items.Sum(x => x.TotalPrice);
                order.Code = "OD" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                _context.Orders.Add(order);
                _context.SaveChanges();
                int id = order.Id;
                ShoppingCart.Instance.Items.ForEach(x => order.ProductOrders.Add(new ProductOrder
                {
                    IdOrder = id,
                    IdProduct = x.ProductId,
                    Amount = x.Quantity
                }));
                _context.SaveChanges();
                SendEmail(ShoppingCart.Instance.Items, order);
                ShoppingCart.Instance.ClearCart();
                code = new { success = true, code = -1 };
                return Json(code);
            }
            return Json(code);
        }

        public IActionResult Partial_Checkout_Items()
        {
            return PartialView(ShoppingCart.Instance.Items);
        }

        public IActionResult GetCountInCart()
        {
            return Json(new { count = ShoppingCart.Instance.Items.Count });
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var existProduct = _context.Products.Include(p => p.IdCategoryProductNavigation).FirstOrDefault(x => x.Id == id);
            ShoppingCart cart = ShoppingCart.Instance;
            var code = new { success = false, message = "", code = -1, count = 0 };
            if (existProduct != null)
            {
                cart.AddToCart(new ShoppingCartItem
                {
                    ProductId = id,
                    ProductName = existProduct.Name,
                    CategoryName = existProduct.IdCategoryProductNavigation != null ? existProduct.IdCategoryProductNavigation.Name : "",
                    ProductImage = existProduct.Image != null ? existProduct.Image.Split(";")[existProduct.Avatar ??= 0] : "",
                    Quantity = quantity,
                    Price = (decimal)(existProduct.PriceSale > 0 ? existProduct.PriceSale : existProduct.Price ??= 0),
                    TotalPrice = quantity * (decimal)(existProduct.PriceSale > 0 ? existProduct.PriceSale : existProduct.Price ??= 0)
                }, quantity);
                code = new { success = true, message = "Add sucessfully", code = 200, count = cart.Items.Count };
                return Json(code);
            }
            code = new { success = false, message = "Fail to add", code = -1, count = cart.Items.Count };
            return Json(code);
        }

        [HttpDelete]
        public IActionResult RemoveFromCart(int id)
        {
            var code = new { success = false, message = "", code = -1, count = 0, totalSub = (decimal)0 };
            ShoppingCart cart = ShoppingCart.Instance;
            var exist = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (cart.RemoveFromCart(id))
            {
                code = new { success = true, message = "Remove successfully", code = 200, count = cart.Items.Count, totalSub = exist != null ? exist.TotalPrice : (decimal)0 };
            }
            else
            {
                code = new { success = false, message = "Fail to Remove", code = -1, count = cart.Items.Count, totalSub = (decimal)0 };
            }
            return Json(code);
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            ShoppingCart.Instance.ClearCart();
            return Json(new { success = true });

        }

        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {
            ShoppingCart.Instance.UpdateQuantity(id, quantity);
            return Json(new { success = true });
        }

        private void SendEmail(List<ShoppingCartItem> items, Order order)
        {
            var fromAddress = new MailAddress("netgame793@gmail.com", "Colo Shop");
            var toAddress = new MailAddress(order.Email ?? "", order.CustomerName);
            const string fromPassword = "cgckvetfvzzhemiw";
            const string subject = "Thank you for purchasing products in ColoShop";
            //const string body = "Default";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            string templateFilePath = "wwwroot/templates/send1.html";
            string emailTemplate = GetEmailTemplate(templateFilePath, items, order);

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = emailTemplate,
                IsBodyHtml = true
            };

            smtp.Send(message);
        }

        private string GetEmailTemplate(string templateFilePath, List<ShoppingCartItem> items, Order order)
        {
            string templateContent = string.Empty;

            using (var reader = new StreamReader(templateFilePath))
            {
                templateContent = reader.ReadToEnd();
            }

            // Replace placeholders with actual values
            templateContent = templateContent.Replace("{{username}}", order.CustomerName);
            templateContent = templateContent.Replace("{{code}}", order.Code);
            templateContent = templateContent.Replace("{{address}}", order.Address);
            templateContent = templateContent.Replace("{{phone}}", order.Phone);
            templateContent = templateContent.Replace("{{email}}", order.Email);
            templateContent = templateContent.Replace("{{date}}", order.CreatedDate.ToString());
            templateContent = templateContent.Replace("{{totalProduct}}", order.TotalAmount.ToString());
            decimal fee = 0;
            templateContent = templateContent.Replace("{{fee}}", fee.ToString());
            string method = order.PaymentType == 1 ? "COD" : order.PaymentType == 2 ? "Transfer" : "Cash";
            templateContent = templateContent.Replace("{{payment}}", method);
            templateContent = templateContent.Replace("{{total}}", (order.TotalAmount ??= 0 + fee).ToString());
            string tabledata = "";
            items.ForEach(item =>
            {
                tabledata += "<tr>";
                tabledata += $"<td style='color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word'>{item.ProductName} (#{item.ProductId})</td>";
                tabledata += $"<td style='color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif'>{item.Quantity}</td>";
                tabledata += $"<td style='color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif'><span>{item.TotalPrice}&nbsp;<span>USD</span></span></td>";
                tabledata += "</tr>";
            });
            templateContent = templateContent.Replace("{{productlist}}", tabledata);
            return templateContent;
        }
    }
}
