using AutoMapper;
using E_CommerceProject.Base;
using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Enums;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Responses;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : AppControllerBase
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _user;
        #endregion

        #region Constructor
        public OrdersController(IOrderRepository orderRepository,
                                IProductRepository productRepository,
                                IMapper mapper,
                                UserManager<User> user
                               )

        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _user = user;
        }
        #endregion

        #region Endpoints
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddProduct([FromBody] AddOrderDTO orderDTO)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return Unauthorized("User is not authorized");

            if (!int.TryParse(userIdClaim.Value, out var userId))
                return BadRequest("Invalid user ID");

            var order = new Order
            {
                UserId = userId,
                TotalPrice = 0,
                OrderItems = new List<OrderItem>()
            };

            foreach (var itemDTO in orderDTO.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(itemDTO.ProductId);
                if (product == null)
                    return BadRequest($"Product with ID {itemDTO.ProductId} not found");

                var itemTotalPrice = itemDTO.Quantity * product.Price;
                order.TotalPrice += itemTotalPrice;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity,
                    Price = itemTotalPrice
                });
            }

            var result = await _orderRepository.AddOrderAsync(order);
            if (result != "Success")
                return BadRequest("Failed to add new order");

            var response = new OrderResponse
            {
                UserId = userId,
                Status = OrderStatus.Pending.ToString(),
                OrderDate = DateTime.UtcNow,
                OrderItems = orderDTO.OrderItems,
                TotalPrice = order.TotalPrice
            };

            return Ok(response);
        }
        #endregion

    }
}
