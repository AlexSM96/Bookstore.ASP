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
        .AddScoped<IRequestHandler<AddAuthorCommand,Author>, AddAuthorCommandHandler>()
        .AddScoped<IRequestHandler<GetAuthorsQuery, IList<Author>>, GetAuthorsQueryHandler>()
        .AddScoped<IRequestHandler<AddReviewCommand, Review>, AddReviewCommandHandler>()
        .AddScoped<IRequestHandler<GetReviewsQuery, IList<Review>>, GetReviewsQueryHandler>()
        .AddScoped<IRequestHandler<GetReviewsByBookIdQuery, IList<Review>>, GetReviewsByBookIdQueryHandler>()
        .AddScoped<IRequestHandler<AddOrderCommand, Order>, AddOrderCommandHandler>()
        .AddScoped<IRequestHandler<GetOrdersQuery, IList<Order>>, GetOrdersQueryHandler>()
        .AddScoped<IRequestHandler<GetUserQuery<Guid>, User>,GetUserQueryHandler<Guid>>()
        .AddScoped<IRequestHandler<GetUserQuery<string>, User>, GetUserQueryHandler<string>>()
        .AddScoped<IRequestHandler<GetUsersQuery, IList<User>>, GetUsersQueryHandler>();
}