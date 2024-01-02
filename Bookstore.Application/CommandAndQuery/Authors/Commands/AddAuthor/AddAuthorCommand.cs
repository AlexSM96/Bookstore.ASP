namespace Bookstore.Application.CommandAndQuery.Authors.Commands.AddAuthor
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public string Name { get; set; }
    }
}
