namespace Core.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? cutPrice { get; set; }
        public int Quantity { get; set; }
        public ICollection<PhotoDto>? Photos { get; set; }
        public CategoryOfProductDto CategoryOfProduct { get; set; }
        public BrandOfProductDto BrandOfProduct { get; set; }
    }
}
