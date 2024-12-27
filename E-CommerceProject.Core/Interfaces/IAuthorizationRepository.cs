using E_CommerceProject.Core.DTOs.AuthenticationDTOs;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Responses;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IAuthorizationRepository
    {
        public Task<bool> IsRoleExist(string roleName);
        public Task<bool> IsRoleNameExistExcludeSelf(string roleName, int id);
        public Task<ManageUserRoleResponse> GetManageUserRoleResponse(User user);
        public Task<string> EditUserRoleAsync(EditUserRole editUserRole);
        public Task<string> EditUserClaimsAsync(EditUserClaims editUserClaims);
        public Task<ManageUserClaimsResponse> ManageUserClaims(User user);

    }
}
