

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

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
        .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
        .AddRazorRuntimeCompilation();

    builder.Services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option =>
        {
            option.LoginPath = new PathString("/Account/Login");
            option.AccessDeniedPath = new PathString("/Account/Login");
        });
       
    builder.Services
        .AddAutoMapper(typeof(BookViewModel), typeof(AuthorViewModel), 
            typeof(CategoryViewModel), typeof(ReviewViewModel),
            typeof(UserViewModel));

    builder.Services
           .AddDataAccessLayer(builder.Configuration)
           .AddScoped<IBaseDbContext, BookstoreDbContext>()
           .AddApplication();
}