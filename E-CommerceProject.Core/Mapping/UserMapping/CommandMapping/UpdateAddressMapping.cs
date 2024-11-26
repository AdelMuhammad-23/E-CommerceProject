using E_CommerceProject.Core.DTOs.AccountDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void UpdateAddressMapping()
        {
            CreateMap<UpdateAddressDtO, Address>();
        }
    }
}
