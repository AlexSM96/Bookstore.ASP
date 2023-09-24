namespace Bookstore.Application.CommandAndQuery.Baskets.Queries.GetBasket
{
    public record GetBasketQuery(Guid UserId) : IRequest<Basket>; 
}
