using E_CommerceProject.Core.DTOs.AccountDTOs;
using E_CommerceProject.Core.Entities.Identity;

namespace E_CommerceProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void AddUserCommandMapping()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(p => p.Phone));
        }
    }
}
