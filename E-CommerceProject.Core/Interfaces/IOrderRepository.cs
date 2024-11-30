using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<string> AddOrderAsync(Order order);
    }
}
