using Microsoft.AspNetCore.Mvc;  // IActionResult ve Controller için
using Services.Contracts;
using Entities.Models;
using System.Linq; // LINQ metodları için

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager; 

        public OrderController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var orders = _manager.OrderService.Orders; 
            return View(orders); 
        }

        [HttpPost]
        public IActionResult Complete([FromForm] int id) 
        {
            _manager.OrderService.Complete(id);
            return RedirectToAction("Index");
        }

    }
}