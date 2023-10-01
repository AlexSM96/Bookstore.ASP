using Bookstore.Application.CommandAndQuery.Baskets.Commands.DeleteFromBasket;
using Bookstore.Application.CommandAndQuery.Reviews.Commands.DeleteReview;

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
                .AddScoped<IRequestHandler<GetUsersQuery, IList<User>>, GetUsersQueryHandler>()
                .AddScoped<IRequestHandler<GetBooksByIdQuery, IList<Book>>, GetBooksByIdQueryHandler>()
                .AddScoped<IRequestHandler<AddToBasketCommand, Basket>, AddToBasketCommandHandler>()
                .AddScoped<IRequestHandler<GetBasketQuery, Basket>, GetBasketQueryHandler>()
                .AddScoped<IRequestHandler<DeleteFromBasketCommand, Unit>, DeleteFromBasketCommandHandler>()
                .AddScoped<IRequestHandler<DeleteReviewCommand, Unit>, DeleteReviewCommandHandler>()
                .AddScoped<ISmtpClient, SmtpClient>()
                .AddScoped<IEmailService, EmailService>();
        }
    }
}
