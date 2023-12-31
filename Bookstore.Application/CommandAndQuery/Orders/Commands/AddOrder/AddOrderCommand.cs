﻿namespace Bookstore.Application.CommandAndQuery.Orders.Commands.AddOrder
{
    public class AddOrderCommand : IRequest<Order>
    {
        public DateTime CreationDate { get; set; } = DateTime.Today;

        public Guid UserId { get; set; }

        public List<Book>? Books { get; set; }
    }
}
