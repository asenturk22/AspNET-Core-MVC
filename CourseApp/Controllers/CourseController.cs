using CourseApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //Hangi tarayiciyla iletisim kurdugunu saglar. Sahtecilik icin onlem olabilir. 
        public IActionResult Apply([FromForm] Candidate model)  //[FromForm] verinin formdan geldigini belirtiyoruz. 
        {
            Repository.Add(model);
            return View("Feedback", model);
        }
    }
}