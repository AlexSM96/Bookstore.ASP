using Bookstore.Application.Interfaces;
using Bookstore.Application.Mapping.AuthorDto;
using Bookstore.Application.Mapping.BookDto;
using Bookstore.Application.Mapping.CategoryDto;
using Bookstore.Application.Services.AuthorServices;
using Bookstore.Application.Services.Base;
using Bookstore.Application.Services.BookServices;
using Bookstore.DAL;
using Microsoft.EntityFrameworkCore;

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
        .AddAutoMapper(typeof(BookViewModel),
            typeof(AuthorViewModel), typeof(CategoryViewModel));

    builder.Services
        .AddScoped<IBookDbContext, BookstoreDbContext>()
        .AddScoped<IAuthorDbContext, BookstoreDbContext>()
        .AddScoped<IBaseService<BookViewModel>, BookService>()
        .AddScoped<IBaseService<AuthorViewModel>, AuthorService>();
}