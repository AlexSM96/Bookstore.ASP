namespace Bookstore.Application.Mapping.OrderDto
{
    public class CreateOrderViewModel
    {
        public DateTime CteationDate { get; set; } = DateTime.Today;

        public Guid UserId { get; set; }

        public Guid BookId { get; set; }
    }
}
