﻿using AutoMapper;
using Bookstore.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Application.Mapping.BookDto
{
    public class BookViewModel : Profile
    {
        public BookViewModel()
        {
            CreateMap<BookViewModel, Book>()
                .ReverseMap();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите назвние книги")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите дату публикации книги")]
        public DateTime PublicationDate { get; set; }

        [MaxLength(500, ErrorMessage = "Превышено макисмальное количество символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Введите цену книги")]
        public decimal Price { get; set; }

        [MaxLength(100, ErrorMessage = "Превышено макисмальное количество символов")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Выберете авторов книги")]
        public IList<Author> Authors { get; set; }

        [Required(ErrorMessage = "Выберете жанры к которым относится книга")]
        public IList<Category> Categories { get; set; }
    }

}
