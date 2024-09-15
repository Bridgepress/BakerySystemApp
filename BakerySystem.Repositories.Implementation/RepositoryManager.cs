using BakerySystem.DataAccess;
using BakerySystem.Repositories.Contracts;
using BakerySystem.Repositories.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BakerySystem.Repositories.Implementation
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public RepositoryManager(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IOrderRepository OrderRepository => _serviceProvider.GetRequiredService<IOrderRepository>();

        public IProductRepository ProductRepository => _serviceProvider.GetRequiredService<IProductRepository>();

        public async Task<bool> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token) > 0;
        }
    }
}
