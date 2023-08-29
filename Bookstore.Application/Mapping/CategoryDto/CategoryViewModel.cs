using AutoMapper;
using Bookstore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.CategoryDto
{
    public class CategoryViewModel : Profile
    {
        public CategoryViewModel()
        {
            CreateMap<CategoryViewModel, Category>()
                .ForMember(c => c.Id, opt => opt.MapFrom(categoryVm => Guid.NewGuid()))
                .ForMember(c => c.Title, opt => opt.MapFrom(categoryVm => categoryVm.Title))
                .ForMember(c => c.BookId, opt => opt.MapFrom(categoryVm => categoryVm.BookId))
                .ReverseMap();
        }

        [Required(ErrorMessage = "Введите название категории")]
        public string Title { get; set; }

        public Guid BookId { get; set; }
    }
}
