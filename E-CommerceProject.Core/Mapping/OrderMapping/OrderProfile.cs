using AutoMapper;

namespace E_CommerceProject.Core.Mapping.OrderMapping
{
    public partial class OrderProfile : Profile
    {
        public OrderProfile()
        {
            AddOrderMapping();
        }
    }
}
