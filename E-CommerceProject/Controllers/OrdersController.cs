using AutoMapper;
using E_CommerceProject.Base;
using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : AppControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _user;
        public OrdersController(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper, UserManager<User> user)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _user = user;
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddProduct([FromBody] AddOrderDTO orderDTO)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (user == null)
                return Unauthorized("User is Unauthorized");
            var p = orderDTO.orderItem.Select(x => x.ProductId);
            // check order ifo
            if (orderDTO == null) ;
            return BadRequest("Invalid Order Data");

        }


    }
}
