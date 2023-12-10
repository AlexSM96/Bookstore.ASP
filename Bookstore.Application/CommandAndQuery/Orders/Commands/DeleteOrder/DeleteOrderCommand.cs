namespace Bookstore.Application.CommandAndQuery.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(Guid OrderId) : IRequest<Unit>;  
}
