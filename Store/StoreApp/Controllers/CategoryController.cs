using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace StoreApp.Controllers
{
    public class CategoryController : Controller 
    {
        private IRepositorManager _manager;

        public CategoryController(IRepositorManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index() 
        {
            var model = _manager.Category.FindAll(false);
            return View(model);
        }

        // public IEnumerable<Category> Index() 
        // {
        //     return _manager.Category.FindAll(false);
        // }
    }
}