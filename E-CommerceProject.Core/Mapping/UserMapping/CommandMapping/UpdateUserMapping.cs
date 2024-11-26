using E_CommerceProject.Core.DTOs.AccountDTOs;
using E_CommerceProject.Core.Entities.Identity;

namespace E_CommerceProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateProfileDTO, User>();
        }
    }
}
