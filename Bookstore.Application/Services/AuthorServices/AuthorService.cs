using AutoMapper;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.AuthorServices
{
    public class AuthorService : IBaseService<Author>
    {
        private readonly IBaseDbContext _context;

        public AuthorService(IBaseDbContext context, IMapper mapper) =>
            _context = context;


        public async Task<IList<Author>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Authors
                .ToListAsync(cancellationToken)
                 ?? throw new ArgumentNullException();
        }

        public async Task CreateAsync(Author author, CancellationToken cancellationToken)
        {
            try
            {
                author.Id = Guid.NewGuid();
                var newAuthor = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Id == author.Id);

                if (newAuthor is not null)
                {
                    throw new Exception($"Автор: ID = {author.Id}, Name = {author.Name}");
                }

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id, cancellationToken);
                if (author is null || author.Id != id)
                {
                    throw new ArgumentNullException(nameof(author));
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }


        public async Task UpdateAsync(Author author, CancellationToken cancellationToken)
        {
            try
            {
                var authorFromDb = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Id == author.Id);

                if (authorFromDb is null || authorFromDb.Id != author.Id)
                {
                    throw new ArgumentNullException(nameof(authorFromDb));
                }

                authorFromDb.Name = author.Name;

                _context.Authors.Update(authorFromDb);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}
