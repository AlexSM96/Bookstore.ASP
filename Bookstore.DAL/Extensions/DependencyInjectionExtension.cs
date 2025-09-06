using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.DAL.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyDb");

            return services.AddDbContext<BookstoreDbContext>(option =>
            {
                option.UseNpgsql(connectionString);
            });
        }
    }
}
