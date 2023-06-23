using ColoShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EcommerceShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceShop")));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "EcommerceShop.Session";
    /* options.IdleTimeout = TimeSpan.FromMinutes(20);*/
});


//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { })
//                .AddEntityFrameworkStores<EcommerceShopContext>()
//                .AddRoles<IdentityRole>()
//                .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Contact",
      pattern: "contact/{action}",
      defaults: new { controller = "Contact", action = "Index" }
    );

    endpoints.MapControllerRoute(
      name: "Cart",
      pattern: "cart",
      defaults: new { controller = "ShoppingCart", action = "Index" }
    );

    endpoints.MapControllerRoute(
      name: "Payment",
      pattern: "payment",
      defaults: new { controller = "ShoppingCart", action = "CheckOut" }
    );

    endpoints.MapControllerRoute(
      name: "Category",
      pattern: "category-{name}-{id}",
      defaults: new { controller = "Products", action = "ProdByCategory" }
    );

    endpoints.MapControllerRoute(
      name: "ProductDetail",
      pattern: "detail/{name}-p{id}",
      defaults: new { controller = "Products", action = "Detail" }
    );

    endpoints.MapControllerRoute(
      name: "Products",
      pattern: "products/{action}/{name?}",
      defaults: new { controller = "Products", action = "Index" }
    );

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
      defaults: new { area = "Admin" }
    );
});

app.Run();
