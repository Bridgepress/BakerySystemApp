using BakerySystem.DataAccess;
using BakerySystem.Domain.Entities;
using BakerySystem.Handlers.SignalR;
using BakerySystem.Repositories.Contracts;
using BakerySystem.Services.Contracts.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace BakerySystem.Services.Implementation.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHubContext<OrderHub> _hubContext;
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryManager _repositoryManager;

        public OrderService(IHubContext<OrderHub> hubContext, ApplicationDbContext context,
            IRepositoryManager repositoryManager)
        {
            _hubContext = hubContext;
            _context = context;
            _repositoryManager = repositoryManager;
        }

        public async Task CreateOrderAsync(Order order)
        {
            try
            {
                await _repositoryManager.OrderRepository.CreateOrder(order);
                var updatedOrders = await _context.Orders.Include(o => o.Products).ToListAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveOrderListUpdate", updatedOrders);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the order", ex);
            }
        }
    }
}
