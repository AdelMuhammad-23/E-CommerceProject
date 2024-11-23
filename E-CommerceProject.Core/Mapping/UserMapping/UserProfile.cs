using AutoMapper;

namespace E_CommerceProject.Core.Mapping.UserMapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserCommandMapping();
            AddAddressUserMapping();
        }
    }
}
