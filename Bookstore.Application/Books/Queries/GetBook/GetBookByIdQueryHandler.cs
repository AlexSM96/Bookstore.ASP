﻿using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.Books.Queries.GetBook
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBaseDbContext _context;

        public GetBookByIdQueryHandler(IBaseDbContext context) =>
            _context = context;
        

        public async Task<Book> Handle(GetBookByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(request.Id);

            if (book is null) 
            {
                throw new ArgumentNullException(nameof(Book));
            }

            return book;
        }
    }
}
