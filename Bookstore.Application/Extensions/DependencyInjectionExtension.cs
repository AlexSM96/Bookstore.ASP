using Bookstore.Application.CommandAndQuery.Accounts.Commands.LogIn;
using Bookstore.Application.CommandAndQuery.Accounts.Commands.Registration;
using Bookstore.Application.CommandAndQuery.Authors.Commands.AddAuthor;
using Bookstore.Application.CommandAndQuery.Authors.Queries.GetAuthors;
using Bookstore.Application.CommandAndQuery.Books.Commands.AddBook;
using Bookstore.Application.CommandAndQuery.Books.Commands.DeleteBook;
using Bookstore.Application.CommandAndQuery.Books.Commands.UpdateBook;
using Bookstore.Application.CommandAndQuery.Books.Queries.GetBook;
using Bookstore.Application.CommandAndQuery.Books.Queries.GetBooks;
using Bookstore.Application.CommandAndQuery.Books.Queries.GetBooksByInput;
using Bookstore.Application.CommandAndQuery.Categories.Commands.AddCategory;
using Bookstore.Application.CommandAndQuery.Categories.Queries.GetCategories;
using Bookstore.Application.CommandAndQuery.Orders.Commands.AddOrder;
using Bookstore.Application.CommandAndQuery.Orders.Queries.GetOrders;
using Bookstore.Application.CommandAndQuery.Reviews.Commands.AddReview;
using Bookstore.Application.CommandAndQuery.Reviews.Queries.GetReviews;
using Bookstore.Application.CommandAndQuery.Reviews.Queries.GetReviewsByBookId;
using Bookstore.Application.CommandAndQuery.Users.Queries.GetUser;
using Bookstore.Application.CommandAndQuery.Users.Queries.GetUsers;
using Bookstore.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Bookstore.Application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddScoped<IRequestHandler<AddBookCommand, Book>, AddBookCommandHandler>()
                .AddScoped<IRequestHandler<UpdateBookCommand, Book>, UpdateBookCommandHandler>()
                .AddScoped<IRequestHandler<DeleteBookCommand, Unit>, DeleteBookCommandHandler>()
                .AddScoped<IRequestHandler<GetBooksQuery, IList<Book>>, GetBooksQueryHandler>()
                .AddScoped<IRequestHandler<GetBooksByInputQuery, IList<Book>>, GetBooksByInputQueryHandler>()
                .AddScoped<IRequestHandler<GetBookByIdQuery, Book>, GetBookByIdQueryHandler>()
                .AddScoped<IRequestHandler<GetCategoriesQuery, IList<Category>>, GetCategoriesQueryHandler>()
                .AddScoped<IRequestHandler<AddCategoryCommand, Category>, AddCategoryCommandHandler>()
                .AddScoped<IRequestHandler<RegisterAccountCommand, ClaimsIdentity>, RegisterAccountCommandHandler>()
                .AddScoped<IRequestHandler<LogInCommand, ClaimsIdentity>, LogInCommandHandler>()
                .AddScoped<IRequestHandler<AddAuthorCommand, Author>, AddAuthorCommandHandler>()
                .AddScoped<IRequestHandler<GetAuthorsQuery, IList<Author>>, GetAuthorsQueryHandler>()
                .AddScoped<IRequestHandler<AddReviewCommand, Review>, AddReviewCommandHandler>()
                .AddScoped<IRequestHandler<GetReviewsQuery, IList<Review>>, GetReviewsQueryHandler>()
                .AddScoped<IRequestHandler<GetReviewsByBookIdQuery, IList<Review>>, GetReviewsByBookIdQueryHandler>()
                .AddScoped<IRequestHandler<AddOrderCommand, Order>, AddOrderCommandHandler>()
                .AddScoped<IRequestHandler<GetOrdersQuery, IList<Order>>, GetOrdersQueryHandler>()
                .AddScoped<IRequestHandler<GetUserQuery<Guid>, User>, GetUserQueryHandler<Guid>>()
                .AddScoped<IRequestHandler<GetUserQuery<string>, User>, GetUserQueryHandler<string>>()
                .AddScoped<IRequestHandler<GetUsersQuery, IList<User>>, GetUsersQueryHandler>();
        }
    }
}
