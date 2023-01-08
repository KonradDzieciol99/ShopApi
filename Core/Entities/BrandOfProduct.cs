namespace Core.Entities
{
    public class BrandOfProduct
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
