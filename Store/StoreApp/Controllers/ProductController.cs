using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        //Dependency Injection = DI pattern
        private readonly RepositoryDbContext _context;

        public ProductController(RepositoryDbContext context) {
            _context = context;
        }

        public IActionResult Index() {
            var model = _context.Products.ToList();
            return View(model);
        } 

        public IActionResult Get(int id)
        {
            Product product = _context.Products.First(p => p.ProductId.Equals(id));
            return View(product);
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