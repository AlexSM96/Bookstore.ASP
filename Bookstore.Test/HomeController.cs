using Bookstore.Test.Interfaces;
using Bookstore.Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;


namespace Bookstore.Test
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View(repository.GetUsers());
        }


        public IActionResult Create() => View();

        [HttpPost] 
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                repository.Create(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult GetUser(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var user = repository.GetUser(id.Value);
            if(user == null)
            {
                return NotFound();
            }

            return View(id);
        }
    }
}
