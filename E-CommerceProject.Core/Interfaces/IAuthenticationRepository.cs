using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Infrastructure.Helper;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task<JwtAuthResult> GetJwtToken(User user);

    }
}
