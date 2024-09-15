using BakerySystem.DataAccess;
using BakerySystem.Domain.Entities;
using BakerySystem.Repositories.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BakerySystem.Repositories.Implementation.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateOrder(Order order)
        {
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            if(order.Products == null || !order.Products.Any())
            {
                throw new ArgumentException("Order must contain at least one product", nameof(order));
            }

            Order newOrder = new Order
            {
                Products = new List<Product>(),
                Status = order.Status
            };
            decimal total = 0;

            foreach (var item in order.Products)
            {
                var product = await _context.Products.Where(x => x.Id == item.Id).FirstOrDefaultAsync();
                if (product?.Count < item.Count)
                {
                    throw new InvalidOperationException($"Not enough products in stock. Product: {product.Title}, Count: {product.Count}");
                }
                product.Count -= item.Count;
                newOrder.Products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Count = item.Count,
                    Title = product.Title,
                    Price = product.Price,
                    Type = product.Type,
                    Image = product.Image,
                    Description = product.Description
                });
                total += item.Price * item.Count;
                _context.Products.Update(product);
            }

            newOrder.Total = total;
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(x=>x.Products).ToListAsync();
        }

        public async Task UpdateOrder(Order order)
        {
             _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
