namespace Bookstore.Application.CommandAndQuery.Baskets.Commands.DeleteFromBasket
{
    public class DeleteFromBasketCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }
    }
}
