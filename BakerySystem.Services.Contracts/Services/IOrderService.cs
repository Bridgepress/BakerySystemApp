using BakerySystem.Domain.Entities;

namespace BakerySystem.Services.Contracts.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
    }
}
