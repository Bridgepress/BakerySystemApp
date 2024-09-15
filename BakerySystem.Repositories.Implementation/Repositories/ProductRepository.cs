using BakerySystem.DataAccess;
using BakerySystem.Domain.Entities;
using BakerySystem.Repositories.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BakerySystem.Repositories.Implementation.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.Where(x => x.OrderId == null).ToListAsync();
        }
    }
}
