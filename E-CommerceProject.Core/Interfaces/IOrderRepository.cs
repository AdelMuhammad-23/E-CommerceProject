using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.DTOs.Pagination;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<PaginatedResult<OrderDTO>> GetAllOrder(int pageNumber = 1, int pageSize = 10);
        public Task<Order> GetOrderById(int Id);
        public Task<string> AddOrderAsync(Order order);

    }
}
