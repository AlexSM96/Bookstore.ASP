namespace Bookstore.Application.CommandAndQuery.Baskets.Commands.DeleteAll
{
    public record DeleteAllFromBasketCommand(Guid UserId) : IRequest<Unit>; 
}
