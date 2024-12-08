using E_CommerceProject.Core.DTOs.BasketDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.BasketMapping
{
    public partial class BasketProfile
    {
        public void UpdateBasketMapping()
        {
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketListDTO, BasketItem>();

        }
    }
}
