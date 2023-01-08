namespace Core.Entities
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(string productName, string pictureUrl, decimal price, int quantity, int productItemId)
        {
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
            Quantity = quantity;
            ProductItemId = productItemId;
            //OrderId = orderId; <--- wypełni orm
        }

        //ProductItemOrdered skrócone
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductItemId { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
