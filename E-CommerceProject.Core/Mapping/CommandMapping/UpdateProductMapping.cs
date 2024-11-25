using E_CommerceProject.Core.DTOs.ProductDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void UpdateProductMapping()
        {
            CreateMap<UpdateProductDTO, Product>()
                                             .ForMember(dest => dest.Image, src => src.Ignore());
        }
    }
}
