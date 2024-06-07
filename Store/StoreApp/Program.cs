using Microsoft.EntityFrameworkCore;
using Repositories;


var builder = WebApplication.CreateBuilder(args);

//Serviceses Added
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryDbContext>(
    options =>   
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection"),
    b => b.MigrationsAssembly("StoreApp")
    
    ));
    

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
