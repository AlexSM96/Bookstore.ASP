using Bookstore.Application.Books.Commands.AddBook;
using Bookstore.Application.Books.Commands.DeleteBook;
using Bookstore.Application.Books.Commands.UpdateBook;
using Bookstore.Application.Books.Queries.GetBook;
using Bookstore.Application.Books.Queries.GetBooks;
using Bookstore.Application.Books.Queries.GetBooksByInput;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
context.Database.EnsureCreated();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddMediatR(option => option
       .RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

    builder.Services
        .AddControllersWithViews()
        .AddRazorRuntimeCompilation();

    string connetionString = builder.Configuration.GetConnectionString("MSSQL")!;
    builder.Services.AddDbContext<BookstoreDbContext>(option =>
    {
        option.UseSqlServer(connetionString);
    });

    builder.Services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option =>
        {
            option.LoginPath = new PathString("/Account/SignIn");
            option.AccessDeniedPath = new PathString("/Account/SignIn");
        });

    builder.Services
        .AddAutoMapper(typeof(BookViewModel), typeof(AuthorViewModel), 
            typeof(CategoryViewModel), typeof(ReviewViewModel),
            typeof(UserViewModel));

    builder.Services
        .AddScoped<IBaseDbContext, BookstoreDbContext>()
        .AddScoped<IBaseService<Author>, AuthorService>()
        .AddScoped<IBaseService<Category>, CategoryService>()
        .AddScoped<IBaseService<Order>, OrderService>()
        .AddScoped<IBaseService<Review>, ReviewService>()
        .AddScoped<IBaseService<User>, UserService>()
        .AddScoped<IAccountService, AccountService>()
        .AddScoped<IRequestHandler<AddBookCommand, Book>, AddBookCommandHandler>()
        .AddScoped<IRequestHandler<UpdateBookCommand, Book>, UpdateBookCommandHandler>()
        .AddScoped<IRequestHandler<DeleteBookCommand, Unit>, DeleteBookCommandHandler>()
        .AddScoped<IRequestHandler<GetBooksQuery, IList<Book>>, GetBooksQueryHandler>()
        .AddScoped<IRequestHandler<GetBooksByInputQuery, IList<Book>>, GetBooksByInputQueryHandler>()
        .AddScoped<IRequestHandler<GetBookByIdQuery, Book>, GetBookByIdQueryHandler>();
}