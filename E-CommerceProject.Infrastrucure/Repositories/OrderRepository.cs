using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.DTOs.Pagination;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        #region Fields
        private readonly DbSet<Order> _orders;
        #endregion
        #region Constructor
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _orders = dbContext.Set<Order>();
        }
        #endregion
        #region Handle Functions
        public async Task<string> AddOrderAsync(Order order)
        {

            try
            {
                await _orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }
        public async Task<PaginatedResult<OrderDTO>> GetAllOrder(int pageNumber = 1, int pageSize = 10)
        {
            var query = _orders.AsQueryable();
            var orders = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(order => new OrderDTO
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    OrderItems = order.OrderItems.Select(orderItems => new ListOrderItem
                    {
                        ProductId = orderItems.ProductId,
                        Quantity = orderItems.Quantity,
                    }).ToList(),
                    Status = order.Status,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice,
                }).ToListAsync();

            var totalItems = query.Count();
            return new PaginatedResult<OrderDTO>
            {
                Items = orders,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }

        public async Task<Order> GetOrderById(int Id)
        {
            return await _orders.SingleOrDefaultAsync(x => x.OrderId.Equals(Id));
        }

        #endregion
    }
}
