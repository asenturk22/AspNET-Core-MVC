using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        //Dependency Injection = DI pattern
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index() {
            var model = _manager.ProductService.GetAllProducts(false);
            return View(model);
        } 

        public IActionResult Get([FromRoute(Name="id")] int id)
        {
            var model = _manager.ProductService.GetOneProduct(id, false);
            return View(model); 
        }

        /* 
        public IEnumerable<Product> Index()
        {            
            var context = new RepositoryDbContext(
                new DbContextOptionsBuilder<RepositoryDbContext>()
                .UseSqlite("Data Source = ProductDb.db")
                .Options
            );

            return context.Products; 
            
            array olarak geri dönüş yapar
            new List<Product>() 
            {
                new Product(){ProductId=1, ProductName="Computer", Price=5}
            };
        }
        */ 
    }
}