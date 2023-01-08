namespace Core.Interfaces.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IProductRepository ProductRepository { get; }
        ICustomerBasketRepository CustomerBasketRepository { get; }
        IDeliveryMethodRepository DeliveryMethodRepository { get; }
        IOrderRepository OrderRepository { get; }
        IBasketItemRepository BasketItemRepository { get; }
        IBrandOfProductRepository BrandOfProductRepository { get; }
        ICategoryOfProductRepository CategoryOfProductRepository  { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
