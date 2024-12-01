using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private DbSet<OrderItem> _orderItems;
        public OrderItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _orderItems = dbContext.Set<OrderItem>();
        }


    }
}
