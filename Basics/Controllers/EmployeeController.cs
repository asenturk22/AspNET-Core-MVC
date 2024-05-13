using Microsoft.AspNetCore.Mvc;
using Basics.Models;

namespace Basics.Controllers 
 {
    public class EmployeeController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult Index1() 
        {
            string message = $"Hello World. {DateTime.Now.ToString()}";
            return View("Index1", message);
        }

        public ViewResult Index2() 
        {
            var names = new String[] {
                "Ahmet", 
                "Abdulla", 
                "Sahra" 
            };
            return View("Index2", names);
        }

        public IActionResult Index3()
        {
            var list = new List<Employee>() {
                new Employee(){Id=1, FirstName="Ahmet", LastName="Demir", Age=20},
                new Employee(){Id=2, FirstName="Mehmet", LastName="Can", Age=20},
                new Employee(){Id=3, FirstName="Hamza", LastName="Dağ", Age=20},

            };
            return View("Index3", list);
        }
    }
 }