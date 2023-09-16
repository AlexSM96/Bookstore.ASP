namespace Bookstore.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper) =>
            (_mediator, _mapper) = (mediator, mapper);
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            var ordersVM = _mapper.Map<IList<OrderViewModel>>(orders);
            return PartialView(ordersVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(AddOrderCommand model)
        {
            var order = await _mediator.Send(model);
            return Ok(model);
        }
    }
}
