using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class UserRefreshTokenRepository : GenericRepository<UserRefreshToken>, IUserRefreshTokenRepository
    {
        #region Fields
        private readonly DbSet<UserRefreshToken> _userRefreshTokens;
        #endregion

        #region Constructors
        public UserRefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _userRefreshTokens = dbContext.Set<UserRefreshToken>();
        }
        #endregion



    }
}
