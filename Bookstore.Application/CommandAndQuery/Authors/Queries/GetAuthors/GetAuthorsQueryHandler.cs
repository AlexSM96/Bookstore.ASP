using Bookstore.Application.Interfaces;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.CommandAndQuery.Authors.Queries.GetAuthors
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IList<Author>>
    {
        private readonly IBaseDbContext _context;

        public GetAuthorsQueryHandler(IBaseDbContext context) =>
            _context = context;

        public async Task<IList<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Authors
                .ToListAsync(cancellationToken);
        }
    }
}
