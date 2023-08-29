using AutoMapper;
using Bookstore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.BookDto
{
    public class BookViewModel : Profile
    {
        public BookViewModel()
        {
            CreateMap<BookViewModel, Book>()
                .ForMember(b => b.Id, opt => opt.MapFrom(bookVm => Guid.NewGuid()))
                .ForMember(b => b.Title, opt => opt.MapFrom(bookVm => bookVm.Title))
                .ForMember(b => b.PublicationDate, opt => opt.MapFrom(bookVm => bookVm.PublicationDate))
                .ForMember(b => b.Description, opt => opt.MapFrom(bookVm => bookVm.Description))
                .ForMember(b => b.Price, opt => opt.MapFrom(bookVm => bookVm.Price))
                .ForMember(b => b.ImagePath, opt => opt.MapFrom(bookVm => bookVm.ImagePath))
                .ForMember(b => b.Authors, opt => opt.MapFrom(bookVm => bookVm.Authors))
                .ForMember(b => b.Categories, opt => opt.MapFrom(bookVm => bookVm.Categories))
                .ReverseMap();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите назвние книги")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите дату публикации книги")]

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        [MaxLength(500, ErrorMessage = "Превышено макисмальное количество символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Введите цену книги")]
        public decimal Price { get; set; }

        [MaxLength(100, ErrorMessage = "Превышено макисмальное количество символов")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Выберете авторов книги")]
        public IList<Author> Authors { get; set; }

        [Required(ErrorMessage = "Выберете категории для книги")]
        public IList<Category> Categories { get; set; }
    }
}
