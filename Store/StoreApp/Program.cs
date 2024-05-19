var builder = WebApplication.CreateBuilder(args);


//MVC controller kullanilacak ve Views nesnelerinin de projeye dahil et
builder.Services.AddControllersWithViews();

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseRouting(); 

app.MapControllerRoute(
    name:"default", 
    pattern:"{controller=Home}/{action=Index}/{id?}"
);


app.Run();
