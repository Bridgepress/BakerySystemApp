namespace BakerySystem.Domain.Entities
{
    public class Product : EntityBase
    {
        public string? Title { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Guid? OrderId { get; set; }
    }
}
