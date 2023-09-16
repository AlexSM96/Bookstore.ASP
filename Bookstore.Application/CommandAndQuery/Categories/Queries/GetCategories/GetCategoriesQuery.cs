using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery() : IRequest<IList<Category>>;
}
