using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.OrderMapping
{
    public partial class OrderProfile
    {
        public void GetOrderByIdMapping()
        {

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src =>
                    src.OrderItems.Sum(item => item.Quantity * item.Price)));
        }

    }
}
