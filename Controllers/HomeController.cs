using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.Controllers

{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // This will load Views/Home/Index.cshtml
        }
    }
}