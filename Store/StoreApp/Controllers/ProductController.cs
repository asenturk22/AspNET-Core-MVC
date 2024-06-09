using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        //Dependency Injection = DI pattern
        private readonly IRepositorManager _manager;

        public ProductController(IRepositorManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index() {
            var model = _manager.Product.GetAllProducts(false);
            return View(model);
        } 

        public IActionResult Get(int id)
        {
            var model = _manager.Product.GetOneProduct(id, false);
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