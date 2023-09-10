using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}