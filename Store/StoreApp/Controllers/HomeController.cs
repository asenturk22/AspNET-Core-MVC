using Microsoft.AspNetCore.Mvc;

namespace StoreApp.controllers 
{
    public class HomeController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
    }
}