
using Core.Exceptions;
using Stripe;
using System.Diagnostics.CodeAnalysis;

namespace Core.Entities
{
    public class BasketItem 
    {
        public BasketItem()
        {

        }
        public BasketItem(string productName, decimal price, int quantity, int customerBasketId, int productId)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            CustomerBasketId = customerBasketId;
            ProductId = productId;
        }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? PictureUrl { get; set; }
        public int CustomerBasketId { get; set; }
        public CustomerBasket CustomerBasket { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
