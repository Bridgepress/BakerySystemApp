using BakerySystem.Repositories.Contracts.Repositories;

namespace BakerySystem.Repositories.Contracts
{
    public interface IRepositoryManager
    {

        IOrderRepository OrderRepository { get; }

        IProductRepository ProductRepository { get; }

        Task<bool> SaveChangesAsync(CancellationToken token = default);
    }
}
