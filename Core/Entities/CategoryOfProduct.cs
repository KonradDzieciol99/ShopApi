namespace Core.Entities
{
    public class CategoryOfProduct
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
