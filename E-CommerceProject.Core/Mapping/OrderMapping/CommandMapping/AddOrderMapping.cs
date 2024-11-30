using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.OrderMapping
{
    public partial class OrderProfile
    {
        public void AddOrderMapping()
        {
            CreateMap<AddOrderDTO, Order>();
        }

    }
}
