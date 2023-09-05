using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBaseService<Book> _service;
        private readonly IMapper _mapper;

        public HomeController(IBaseService<Book> service, IMapper mapper) =>
            (_service,_mapper) =(service, mapper);
        
        public async Task<IActionResult> Index()
        {
            var response = await _service.GetAllAsync(CancellationToken.None);
            var books = response.TakeLast(4);
            return View(_mapper.Map<IList<BookViewModel>>(books));
        }
    }
}