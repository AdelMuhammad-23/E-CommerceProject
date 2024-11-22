using E_CommerceProject.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

public class UserRepository : IUserRepository
{
    #region Fields
    private readonly UserManager<User> _userManager;
    #endregion

    #region Constructor
    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }
    #endregion

    #region Methods
    public async Task<string> AddUserAsync(User user, string password)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrWhiteSpace(password)) return "PasswordCannotBeEmpty";

        // Check if email or username already exists
        var existingUser = await CheckUserExistenceAsync(user);
        if (existingUser != null) return existingUser;

        // Create user
        var createResult = await _userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
            return createResult.Errors.FirstOrDefault()?.Description ?? "UnknownError";

        return "Success";
    }

    private async Task<string> CheckUserExistenceAsync(User user)
    {
        if (await _userManager.FindByEmailAsync(user.Email) != null)
            return "EmailIsExist";

        if (await _userManager.FindByNameAsync(user.UserName) != null)
            return "UserNameIsExist";

        return null; // No conflicts
    }
    #endregion
}
