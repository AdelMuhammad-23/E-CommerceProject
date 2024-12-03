using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.Entities;


namespace E_CommerceProject.Core.Mapping.OrderMapping
{
    public partial class OrderProfile
    {
        public void GetAllOrderMapping()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Status, src => src.MapFrom(o => o.Status));

            CreateMap<OrderItem, ListOrderItem>()
                .ForMember(dest => dest.ProductId, src => src.MapFrom(o => o.ProductId))
                .ForMember(dest => dest.Quantity, src => src.MapFrom(o => o.Quantity));
        }
    }
}
