using E_CommerceProject.Core.DTOs.ProductDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void AddProductMapping()
        {
            CreateMap<AddProductsDTO, Product>()
                .ForMember(dest => dest.Image, src => src.Ignore());

        }
    }
}
