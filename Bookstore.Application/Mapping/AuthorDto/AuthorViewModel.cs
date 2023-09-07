using AutoMapper;
using Bookstore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.AuthorDto
{
    public class AuthorViewModel : Profile
    {
        public AuthorViewModel()
        {
            CreateMap<Author, AuthorViewModel>()
                .ReverseMap();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage ="Веведите имя автора")]
        public string Name { get; set; }
    }
}
