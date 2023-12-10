using Bookstore.Application.CommandAndQuery.Orders.Commands.DeleteOrder;
using Bookstore.Application.Services.Email;

namespace Bookstore.Presentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IEmailService _service;

        public OrderController(IMediator mediator, IMapper mapper, IEmailService service) =>
            (_mediator, _mapper, _service) = (mediator, mapper, service);

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            var ordersVM = _mapper.Map<IList<OrderViewModel>>(orders);
            return View(ordersVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(AddOrderCommand model, string bodyHtml)
        {
            var order = await _mediator.Send(model);

            await _service
                .SendEmailAsync(order.User.Email, "Подтверждение заказа", $"""<ul class="list-group">{bodyHtml}<ul>""");

            return Ok();
        }

        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            await _mediator.Send(new  DeleteOrderCommand(orderId));
            return RedirectToAction("GetOrders");
        }
    }
}
