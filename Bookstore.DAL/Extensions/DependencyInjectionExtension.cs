using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.DAL.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MSSQL");

            return services.AddDbContext<BookstoreDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
