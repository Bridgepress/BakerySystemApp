using BakerySystem.Core.Installers;
using BakerySystem.Services.Contracts.Services;
using BakerySystem.Services.Implementation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BakerySystem.Services.Implementation.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
