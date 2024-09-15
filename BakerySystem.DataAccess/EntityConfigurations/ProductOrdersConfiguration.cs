using BakerySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakerySystem.DataAccess.EntityConfigurations
{
    public class ProductOrdersConfiguration : IEntityTypeConfiguration<ProductOrders>
    {
        public void Configure(EntityTypeBuilder<ProductOrders> builder)
        {
            builder.HasOne<Order>()
                   .WithMany()
                   .HasForeignKey(po => po.OrderId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
