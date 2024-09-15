using BakerySystem.Domain.Entities;

namespace BakerySystem.Domain.Models
{
    public class UpdateOrder : EntityBase
    {
        public string Status { get; set; } = default!;
        public decimal? Total { get; set; }
    }
}
