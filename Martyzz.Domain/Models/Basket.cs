namespace Martyzz.Domain.Models
{
    public class Basket
    {
        public Guid Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
