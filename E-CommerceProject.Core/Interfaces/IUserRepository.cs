using E_CommerceProject.Core.Entities.Identity;

public interface IUserRepository
{
    public Task<string> AddUserAsync(User user, string Password);
}