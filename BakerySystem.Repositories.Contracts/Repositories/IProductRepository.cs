using BakerySystem.Domain.Entities;

namespace BakerySystem.Repositories.Contracts.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<List<Product>> GetAllProducts();
    }
}
