using AutoMapper;

namespace E_CommerceProject.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            CreateRoleMapping();
            UpdateRoleMapping();
        }
    }
}
