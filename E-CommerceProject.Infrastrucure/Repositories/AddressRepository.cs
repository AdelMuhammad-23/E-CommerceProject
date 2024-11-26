using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly DbSet<Address> _addresses;
        public AddressRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _addresses = dbContext.Set<Address>();
        }
    }
}
