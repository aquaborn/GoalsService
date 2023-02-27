using Microsoft.EntityFrameworkCore;
using Pet.Common;

namespace Pet.Web.AppData
{
    public static class DatabaseContextExtensions
    {
        public static void AddCustomSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppDbContext");
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString));
            services.BuildServiceProvider().GetService<AppDbContext>().Database.Migrate(); // DB automigration on start enable
        }
    }
}
