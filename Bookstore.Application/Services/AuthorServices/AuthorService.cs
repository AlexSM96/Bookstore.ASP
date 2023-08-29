using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bookstore.Application.Interfaces;
using Bookstore.Application.Mapping.AuthorDto;
using Bookstore.Application.Services.Base;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Application.Services.AuthorServices
{
    public class AuthorService : IBaseService<AuthorViewModel>
    {
        private readonly IAuthorDbContext _context;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);


        public async Task<IList<AuthorViewModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Authors
                .ProjectTo<AuthorViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task CreateAsync(AuthorViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                model.Id = Guid.NewGuid();
                var author = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Id == model.Id);

                if (author is not null)
                {
                    throw new Exception($"Автор: ID = {author.Id},Name = {author.Name}");
                }

                author = _mapper.Map<Author>(model);
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


        public async Task UpdateAsync(AuthorViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Id == model.Id);

                if (author is null || author.Id != model.Id)
                {
                    throw new ArgumentNullException(nameof(author));
                }

                _context.Authors.Update(author);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}
