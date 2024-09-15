using BakerySystem.Core.General;
using BakerySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BakerySystem.DataAccess.InitalDataCreate
{
    public class MigrationsService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public MigrationsService(IServiceProvider serviceProvider, ILogger logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                await context.Database.MigrateAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while migrating the database");
            }

            await SeedProducts(context, stoppingToken);
        }

        private static async Task SeedProducts(ApplicationDbContext context, CancellationToken cancellationToken)
        {
            if (await context.Products.AnyAsync(cancellationToken))
            {
                return;
            }
            context.AddRange(
                new Product
                {
                    Id = Guid.NewGuid(),
                    Title = "Muffin Chocolate",
                    Type = TypeProductEnum.Muffin.ToString(),
                    Count = 100,
                    Price = 200,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCID19_kX1SxsjtNo2ceAfp3O-h1phruHpA&s",
                    Description = "Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Title = "Bread Chocolate",
                    Type = TypeProductEnum.Bread.ToString(),
                    Count = 100,
                    Price = 200,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCID19_kX1SxsjtNo2ceAfp3O-h1phruHpA&s",
                    Description = "Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Title = "Bread None",
                    Type = TypeProductEnum.Bread.ToString(),
                    Count = 100,
                    Price = 200,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCID19_kX1SxsjtNo2ceAfp3O-h1phruHpA&s",
                    Description = "Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Type = TypeProductEnum.Muffin.ToString(),
                    Title = "Muffin Chocolate",
                    Count = 100,
                    Price = 200,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCID19_kX1SxsjtNo2ceAfp3O-h1phruHpA&s",
                    Description = "Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Type = TypeProductEnum.Cookie.ToString(),
                    Title = "Muffin Chocolate",
                    Count = 100,
                    Price = 200,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCID19_kX1SxsjtNo2ceAfp3O-h1phruHpA&s",
                    Description = @"Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them.
                                    Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them.
                                    Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them.
                                    Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them.
                                    Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them.
                                    Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them.
                                    Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them.
                                    Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Title = "Muffin Chocolate",
                    Count = 100,
                    Price = 200,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmCID19_kX1SxsjtNo2ceAfp3O-h1phruHpA&s",
                    Description = "Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them."
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Title = "Muffin Chocolate",
                    Count = 200,
                    Price = 200.50M,
                    Image = "https://images.24ur.com/media/images/953x459/Mar2011/60642848.jpg?v=7607&fop=fp:0.50:0.60",
                    Description = "Chocolate muffins are the perfect dessert and so easy to make, you can trust your children to make them."
                }
            );

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
