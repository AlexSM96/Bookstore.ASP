using AutoMapper;
using Bookstore.Application.Mapping.OrderDto;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBaseService<Order> _service;
        private readonly IMapper _mapper;

        public OrderController(IBaseService<Order> service, IMapper mapper) =>
            (_service,_mapper) = (service,mapper);
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _service.GetAllAsync(CancellationToken.None);
            return PartialView(_mapper.Map<IList<OrderViewModel>>(orders));
        }
    }
}
