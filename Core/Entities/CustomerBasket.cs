namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }
         
        public CustomerBasket(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public decimal? ShippingPrice { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }

    }
}
