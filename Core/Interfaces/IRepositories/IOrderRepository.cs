using Core.Entities;

namespace Core.Interfaces.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetOrderByIntendIdWithDeliveryMethodAndOrderItemsAndPhotos(string intendId);
    }
}
