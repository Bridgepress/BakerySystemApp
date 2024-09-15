namespace BakerySystem.Domain.Entities
{
    public class ProductOrders : EntityBase
    {
        public List<Product> Products { get; set; }
        public Guid OrderId { get; set; }
    }
}
