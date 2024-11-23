using AutoMapper;
using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.UserMapping
{
    public partial class UserProfile : Profile
    {
        public void AddAddressUserMapping()
        {
            CreateMap<AddAddressDTO, Address>();
        }
    }
}
