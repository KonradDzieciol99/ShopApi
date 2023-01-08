namespace Core.Dtos
{
    public class ProductInBasketDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
