using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var model = _manager.ProductService.GetAllProducts(false);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken ]
        public IActionResult Create([FromForm] Product product)
        {
            if (ModelState.IsValid) {
                _manager.ProductService.CreateProduct(product); 
                return RedirectToAction("Index");
            }

            return View();
        }

        //Get Method
        public IActionResult Update([FromRoute(Name ="id")] int id)
        {
            var model = _manager.ProductService.GetOneProduct(id, false);
            return View(model);
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid) 
            {
                _manager.ProductService.UpdateOneProduct(product);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            if (ModelState.IsValid) 
            {
                _manager.ProductService.DeleteOneProduct(id);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}