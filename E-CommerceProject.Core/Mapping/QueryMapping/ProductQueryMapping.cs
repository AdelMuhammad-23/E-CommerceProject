using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void ProductListMapping()
        {
            CreateMap<Product, ProductsListDTO>();
        }
    }
}
