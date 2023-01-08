
using Core.Enums;
using Core.Models;

namespace Core.Entities
{
    public class Order
    {
        public Order()
        {
        }

        public Order(string buyerEmail, OrderAddress shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId, ICollection<OrderItem> orderItems)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
            OrderItems = orderItems;
        }

        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

    }
}
