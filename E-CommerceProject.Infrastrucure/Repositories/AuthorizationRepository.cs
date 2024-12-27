using E_CommerceProject.Core.DTOs.AuthenticationDTOs;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Helper;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Responses;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        #endregion

        #region Constructos
        public AuthorizationRepository(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        #endregion
        public async Task<string> EditUserClaimsAsync(EditUserClaims editUserClaims)
        {
            var trancat = _dbContext.Database.BeginTransaction();
            try
            {
                var user = await _userManager.FindByIdAsync(editUserClaims.UserId.ToString());
                if (user == null) return "User Not Found";
                var oldClaims = await _userManager.GetClaimsAsync(user);

                var removeOldClaims = await _userManager.RemoveClaimsAsync(user, oldClaims);
                if (!removeOldClaims.Succeeded)
                    return "Failed to remove old UserClaims";
                var selectedClaims = editUserClaims.UserClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var newClaims = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!newClaims.Succeeded)
                    return "Failed to Add New UserClaims";


                await trancat.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trancat.RollbackAsync();
                return "Failed to Edit  UserClaims";
            }
        }

        public async Task<string> EditUserRoleAsync(EditUserRole editUserRole)
        {
            var trancat = _dbContext.Database.BeginTransaction();
            try
            {
                var user = await _userManager.FindByIdAsync(editUserRole.UserId.ToString());
                if (user == null) return "User Not Found";
                var oldUserRolesList = await _userManager.GetRolesAsync(user);
                var deleteOldUserRoleList = await _userManager.RemoveFromRolesAsync(user, oldUserRolesList);
                if (!deleteOldUserRoleList.Succeeded)
                    return "Failed to remove old UserRoles";

                var newRoles = editUserRole.Roles.Where(x => x.HasRole == true).Select(x => x.Name);
                var newUserRolesList = await _userManager.AddToRolesAsync(user, newRoles);
                if (!newUserRolesList.Succeeded)
                    return "Failed to Add New UserRoles";
                await trancat.CommitAsync();

                return "Success";
            }
            catch (Exception ex)
            {
                await trancat.RollbackAsync();
                return "Failed to Update UserRoles";
            }
        }

        public async Task<ManageUserRoleResponse> GetManageUserRoleResponse(User user)
        {
            var response = new ManageUserRoleResponse();
            var rolesListModel = new List<UserRoles>();
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var rolesModel = new UserRoles();
                rolesModel.Id = role.Id;
                rolesModel.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    rolesModel.HasRole = true;
                rolesListModel.Add(rolesModel);
            }
            response.Roles = rolesListModel;
            return response;
        }



        public async Task<bool> IsRoleExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> IsRoleNameExistExcludeSelf(string roleName, int id)
        {
            var role = _roleManager.Roles.Where(x => x.Name == roleName && x.Id == id).FirstOrDefault();
            if (role == null) return false;
            return true;

        }

        public async Task<ManageUserClaimsResponse> ManageUserClaims(User user)
        {
            //create Empty response
            var response = new ManageUserClaimsResponse();
            //create empty userClaims list
            var claimsList = new List<UserClaims>();
            //get claims for user
            var userClaims = await _userManager.GetClaimsAsync(user);

            response.UserId = user.Id;

            foreach (var claim in ClaimsStore.claims)
            {
                //create new object from UserClaims 
                var claims = new UserClaims();
                //check user claims is equal any claims in claims store or not 
                if (userClaims.Any(x => x.Type == claim.Type))
                    claims.Value = true;

                claims.Type = claim.Type;
                //add object in claims list
                claimsList.Add(claims);
            }
            //add claims list in userClaims in response
            response.UserClaims = claimsList;

            return response;
        }


    }
}
