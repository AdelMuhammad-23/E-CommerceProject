using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<string> AddUserAsync(User user, string Password);
    public Task<string> AddAddressUserAsync(Address address);
    public Task<User> GetUserByIdAsync(int id);
}