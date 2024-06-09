using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;


var builder = WebApplication.CreateBuilder(args);

//Serviceses Added
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryDbContext>(
    options =>   
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
    b => b.MigrationsAssembly("StoreApp")
    
    ));


//IoC  Register
builder.Services.AddScoped<IRepositorManager, RepositoryManager>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


    

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();               //wwwroot klasoru yonetir. 
app.UseHttpsRedirection();          
app.UseRouting();                   

app.MapControllerRoute(
    name:"default", 
    pattern:"{controller=Home}/{action=Index}/{id?}"
);


app.Run();
