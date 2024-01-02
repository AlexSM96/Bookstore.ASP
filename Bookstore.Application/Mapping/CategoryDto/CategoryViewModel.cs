using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.CategoryDto
{
    public class CategoryViewModel : Profile
    {
        public CategoryViewModel()
        {
            CreateMap<Category, CategoryViewModel>()
                .ReverseMap();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите новую категорию")]
        public string Name { get; set; }
    }

}
