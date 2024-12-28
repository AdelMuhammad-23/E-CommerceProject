using E_CommerceProject.Core.DTOs.RolesDTOs;
using E_CommerceProject.Core.Entities.Identity;

namespace E_CommerceProject.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public void CreateRoleMapping()
        {
            CreateMap<CreateRoleDTO, Role>();
        }
    }
}
