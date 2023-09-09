using AutoMapper;
using Bookstore.Application.Mapping.OrderDto;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBaseService<Order> _service;
        private readonly IMapper _mapper;
        private readonly IBaseDbContext _context;

        public OrderController(IBaseService<Order> service, IMapper mapper, 
            IBaseDbContext context) =>
            (_service, _mapper, _context) = (service, mapper, context);
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _service.GetAllAsync(CancellationToken.None);
            return PartialView(_mapper.Map<IList<OrderViewModel>>(orders));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            var book = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == model.BookId);
            var vm = new OrderViewModel();
            vm.UserId = model.UserId;
            vm.CreationDate = model.CteationDate;
            vm.Books.Add(_mapper.Map<BookViewModel>(book));

            var order = _mapper.Map<Order>(vm);
            await _service.CreateAsync(order, CancellationToken.None);
            return Ok(model);
        }
    }
}
