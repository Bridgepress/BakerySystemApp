using BakerySystem.Core.Installers;
using BakerySystem.Repositories.Contracts;
using BakerySystem.Repositories.Contracts.Repositories;
using BakerySystem.Repositories.Implementation.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BakerySystem.Repositories.Implementation.Installers
{
    public class RepositoryInstaller : IInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            var repositories = typeof(RepositoryManager).Assembly.GetTypes()
                .Where(type => type.BaseType is not null && type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(RepositoryBase<>));

            foreach (var repository in repositories)
            {
                var repositoryInterface = repository.GetInterfaces()
                    .Single(i => !i.IsGenericType);
                services.AddScoped(repositoryInterface, repository);
            }
        }
    }
}
