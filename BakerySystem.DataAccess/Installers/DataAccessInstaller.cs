using BakerySystem.Core.Installers;
using BakerySystem.DataAccess.InitalDataCreate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BakerySystem.DataAccess.Installers
{
    public class DataAccessInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ApplicationDbContext.ConnectionStringKey),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly("BakerySystem.DataAccess")));
            services.AddHostedService<MigrationsService>();
        }
    }
}
