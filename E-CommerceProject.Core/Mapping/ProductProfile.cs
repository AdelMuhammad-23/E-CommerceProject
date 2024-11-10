using AutoMapper;

namespace E_CommerceProject.Core.Mapping.ProductMapping
{
    public partial class ProductProfile : Profile
    {
        public ProductProfile()
        {
            ProductListMapping();
            AddProductMapping();
        }
    }
}
