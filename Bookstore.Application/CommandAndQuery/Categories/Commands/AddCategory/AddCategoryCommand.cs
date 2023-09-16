using Bookstore.Domain.Entities;
using MediatR;

namespace Bookstore.Application.CommandAndQuery.Categories.Commands.AddCategory
{
    public class AddCategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
    }
}
