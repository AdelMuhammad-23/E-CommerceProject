using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    #region Fields
    private readonly DbSet<Address> _addresse;
    private readonly UserManager<User> _userManager;
    #endregion

    #region Constructor
    public UserRepository(UserManager<User> userManager, ApplicationDbContext dbContext) : base(dbContext)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _addresse = dbContext.Set<Address>();
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

        // Assign default role
        var roleResult = await _userManager.AddToRoleAsync(user, "User");
        if (!roleResult.Succeeded)
            return roleResult.Errors.FirstOrDefault()?.Description ?? "FailedToAssignRole";


        return "Success";
    }
    public async Task<string> AddAddressUserAsync(Address address)
    {
        await _addresse.AddAsync(address);
        await _dbContext.SaveChangesAsync();
        return "Success";
    }
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }
    #endregion
    #region Method Helper
    private async Task<string> CheckUserExistenceAsync(User user)
    {
        if (await _userManager.FindByEmailAsync(user.Email) != null)
            return "EmailIsExist";

        if (await _userManager.FindByNameAsync(user.UserName) != null)
            return "UserNameIsExist";

        return null;
    }
    #endregion

}
