using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.OrderMapping
{
    public partial class OrderProfile
    {
        public void AddOrderMapping()
        {
            CreateMap<AddOrderDTO, Order>()
                .ForMember(dest => dest.OrderItems, src => src.MapFrom(o => o.OrderItems));

            CreateMap<OrderItemDTO, OrderItem>()
                .ForMember(dest => dest.ProductId, src => src.MapFrom(o => o.ProductId))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(o => o.Quantity));
        }

    }
}
