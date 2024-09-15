namespace BakerySystem.Domain.Entities
{
    public class Order : EntityBase
    {
        public List<Product>? Products { get; set; }
        public string Status { get; set; } = default!;
        public decimal? Total { get; set; }
    }
}
