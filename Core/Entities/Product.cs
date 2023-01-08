namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? cutPrice  { get; set; }
        public int Quantity { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<BasketItem> BasketItem { get; set; }
        public int CategoryOfProductId { get; set; }
        public CategoryOfProduct CategoryOfProduct { get; set; }
        public int BrandOfProductId { get; set; }
        public BrandOfProduct BrandOfProduct { get; set; }
    }
}
