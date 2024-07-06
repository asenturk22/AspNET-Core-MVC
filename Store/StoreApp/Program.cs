using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using Entities.Models;
using StoreApp.Models;

var builder = WebApplication.CreateBuilder(args);


//Serviceses Added
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<RepositoryDbContext>(
    options =>   
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
    b => b.MigrationsAssembly("StoreApp")
    
    ));

//Sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => 
{
    options.Cookie.Name = "StoreApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
}); 
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 


//IoC  Register
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IOrderService, OrderManager>(); 

builder.Services.AddScoped<Cart>(c => SessionCart.GetCart(c)); 

builder.Services.AddAutoMapper(typeof(Program));    

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();               //wwwroot klasoru yonetir. 
app.UseHttpsRedirection();     
app.UseSession();
app.UseRouting();                   

//End Points
app.UseEndpoints(endpoints => 
{
    endpoints.MapAreaControllerRoute(
        name:"Admin", 
        areaName:"Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );
    
    endpoints.MapControllerRoute(
        name:"default", 
        pattern:"{controller=Home}/{action=Index}/{id?}"

    );

    endpoints.MapRazorPages();
});

/*
One endpoints
app.MapControllerRoute(
        name:"default", 
        pattern:"{controller=Home}/{action=Index}/{id?}"
);
*/

app.Run();
