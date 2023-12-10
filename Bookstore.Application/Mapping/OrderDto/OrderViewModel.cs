using AutoMapper;
using Bookstore.Application.Mapping.BookDto;
using Bookstore.Application.Mapping.UserDto;

namespace Bookstore.Application.Mapping.OrderDto
{
    public class OrderViewModel : Profile
    {
        public OrderViewModel()
        {
            CreateMap<Order, OrderViewModel>()
                .ReverseMap();
        }

        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Today;

        public Guid UserId { get; set; }

        public UserViewModel User { get; set; }

        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>(); 
    }
}
