using StoreApp.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//Serviceses Added
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RepositoryDbContext>(
    options =>  
    options.UseSqlite(builder.Configuration.GetConnectionString("sqlconnection")));
    

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseRouting(); 

app.MapControllerRoute(
    name:"default", 
    pattern:"{controller=Home}/{action=Index}/{id?}"
);


app.Run();
