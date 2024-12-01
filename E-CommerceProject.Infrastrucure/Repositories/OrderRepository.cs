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
        #endregion
    }
}
