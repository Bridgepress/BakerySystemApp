using BakerySystem.Core.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BakerySystem.Api.Installers
{
    public class AutomapperInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApiAssemblyMarker));
        }
    }
}
