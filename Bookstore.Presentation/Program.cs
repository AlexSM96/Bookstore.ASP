using Bookstore.Application.Mapping.CategoryDto;
using Bookstore.Application.Mapping.ReviewDto;
using Bookstore.Application.Mapping.UserDto;
using Bookstore.Application.Services.CategoryServices;
using Bookstore.Application.Services.OrderServices;
using Bookstore.Application.Services.ReviewServices;

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
        .AddScoped<IBaseService<Book>, BookService>()
        .AddScoped<IBaseService<Author>, AuthorService>()
        .AddScoped<IBaseService<Category>, CategoryService>()
        .AddScoped<IBaseService<Order>, OrderService>()
        .AddScoped<IBaseService<Review>, ReviewService>()
        .AddScoped<IAccountService, AccountService>();
}