using BakerySystem.Core.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BakerySystem.Api.Installers
{
    public class ControllersInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
            => services.AddControllers()
                .AddApplicationPart(typeof(ApiAssemblyMarker).Assembly)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
    }
}
