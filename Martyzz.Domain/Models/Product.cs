namespace Martyzz.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }

        // relations
        public int? CategoryId { get; set; }
        public Category? ProductCategory { get; set; }

        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}
