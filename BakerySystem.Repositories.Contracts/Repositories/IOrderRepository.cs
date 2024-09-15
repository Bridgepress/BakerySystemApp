using BakerySystem.Domain.Entities;

namespace BakerySystem.Repositories.Contracts.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task CreateOrder(Order order);
        Task<List<Order>> GetAllOrders();
        Task UpdateOrder(Order order);
    }
}
