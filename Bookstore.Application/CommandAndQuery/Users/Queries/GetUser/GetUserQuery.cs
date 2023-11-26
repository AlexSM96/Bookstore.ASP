namespace Bookstore.Application.CommandAndQuery.Users.Queries.GetUser
{
    public record GetUserQuery<T>(T Data) : IRequest<User>;
}
