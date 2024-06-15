using Entities.Models;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            /*
            1. parametre : veritabanindaki Categori alanindaki listeyi item olarak getirir. 
            2. parametre : CategoryId veri alani,
            3. parametre : CategoryName text alani,
            4. parametre : Default olarak secili 1. kayit getirilsin 
            */
            ViewBag.Categories = GetCategoriesSelectList();
            return View();
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(
                    _manager.CategoryService.GetAllCategories(false),
                    "CategoryId",
                    "CategoryName",
                    "1"
                );
        }

        [HttpPost]
        [ValidateAntiForgeryToken ]
        public IActionResult Create([FromForm] ProductDtoForInsertion productDto)
        {
            if (ModelState.IsValid) {
                _manager.ProductService.CreateProduct(productDto); 
                return RedirectToAction("Index");
            }

            return View();
        }

        //Get Method
        public IActionResult Update([FromRoute(Name ="id")] int id)
        {
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        //Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] ProductDtoForUpdate product)
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