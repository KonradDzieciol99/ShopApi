using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class CustomerBasketDto
    {
        public int? Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public decimal? ShippingPrice { get; set; }
        //public int AppUserId { get; set; }??????
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
        //public int? CustomerBasketId { get; set; } 
        
    }
}
