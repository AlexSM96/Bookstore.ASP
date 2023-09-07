using AutoMapper;
using Bookstore.Application.Mapping.UserDto;
using Bookstore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.ReviewDto
{
    public class ReviewViewModel : Profile
    {
        public ReviewViewModel() 
        {
            CreateMap<Review, ReviewViewModel>()
                .ReverseMap();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите комментарий")]      
        public string Text { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Today;

        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public UserViewModel User { get; set; }
    }
}
