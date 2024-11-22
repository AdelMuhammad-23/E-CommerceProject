using E_CommerceProject.Core.Entities.Identity;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<string> AddUserAsync(User user, string password);
    }
}
