namespace Bookstore.Application.CommandAndQuery.Baskets.Commands.AddToBasket
{
    public class AddToBasketCommand : IRequest<Basket>
    {
        public Guid UserId { get; set; }

        public List<Book> Books { get; set; }
    }
}
