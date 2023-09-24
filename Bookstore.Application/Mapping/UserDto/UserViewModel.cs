using AutoMapper;
using Bookstore.Application.Mapping.OrderDto;
using Bookstore.Application.Mapping.ReviewDto;

namespace Bookstore.Application.Mapping.UserDto
{
    public class UserViewModel : Profile
    {
        public UserViewModel()
        {
            CreateMap<User, UserViewModel>()
                .ReverseMap();
        }

        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IList<ReviewViewModel>? Reviews { get; set; }

        public IList<OrderViewModel>? Orders { get; set; }
    }
}
