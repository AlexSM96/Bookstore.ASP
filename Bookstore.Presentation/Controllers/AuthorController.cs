using Bookstore.Application.Mapping.AuthorDto;
using Bookstore.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBaseService<AuthorViewModel> _service;

        public AuthorController(IBaseService<AuthorViewModel> service) => 
            _service = service;
        
        public async Task<IActionResult> Index()
        {
            var authors = await _service.GetAllAsync(CancellationToken.None);
            return View(authors);
        }

        public async Task<IActionResult> GetJsonListAuthors()
        {
            var authors = await _service.GetAllAsync(CancellationToken.None);
            return Json(authors);
        }
    }
}
