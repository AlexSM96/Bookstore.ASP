using AutoMapper;
using Bookstore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.AuthorDto
{
    public class AuthorViewModel : Profile
    {
        public AuthorViewModel()
        {
            CreateMap<AuthorViewModel, Author>()
                .ForMember(a => a.Id, opt => opt.MapFrom(authorVm => Guid.NewGuid()))
                .ForMember(a => a.Name, opt => opt.MapFrom(authorVm => authorVm.Name))
                .ReverseMap();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage ="Веведите имя автора")]
        public string Name { get; set; }
    }
}
