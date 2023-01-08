using Core.Entities;

namespace Core.Interfaces.IRepositories
{
    public interface ICustomerBasketRepository : IRepository<CustomerBasket>
    {
        void Update(CustomerBasket entity);
    }
}
