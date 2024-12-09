using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_CommerceProject.Base;
using E_CommerceProject.Core.DTOs.OrderDTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Enums;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : AppControllerBase
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public OrdersController(IOrderRepository orderRepository,
                                IProductRepository productRepository,
                                IMapper mapper
                               )

        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        #endregion

        #region Endpoints

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders(int pageNumber = 1, int pageSize = 10)
        {
            var orders = await _orderRepository.GetAllOrder(pageNumber, pageSize);
            if (orders == null)
                return BadRequest("Orders Is not Found");
            return Ok(orders);
        }
        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var orderDto = await _orderRepository.GetTableNoTracking()
                                                 .Where(o => o.OrderId == id)
                                                 .Select(o => new OrderDTO
                                                 {
                                                     OrderId = o.OrderId,
                                                     UserId = o.UserId,
                                                     OrderDate = o.OrderDate,
                                                     Status = o.Status,
                                                     TotalPrice = o.TotalPrice,
                                                     OrderItems = o.OrderItems.Select(oi => new ListOrderItem
                                                     {
                                                         ProductId = oi.ProductId,
                                                         Quantity = oi.Quantity
                                                     }).ToList()
                                                 })
                                                 .FirstOrDefaultAsync();

            if (orderDto == null)
                return NotFound("Order is not found");

            return Ok(orderDto);
        }
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetOrderByUser(int userId)
        {
            var orders = await _orderRepository.GetTableNoTracking()
                                               .Where(o => o.UserId == userId)
                                               .OrderByDescending(o => o.OrderDate)
                                               .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                                               .ToListAsync();

            if (!orders.Any())
                return NotFound("No orders found for this user.");

            return Ok(orders);
        }


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
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(AddOrderDTO updateOrder)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return Unauthorized("User is not authorized");
            var UserId = int.Parse(userIdClaim.Value);
            var orders = await _orderRepository.GetTableNoTracking()
                                              .Where(o => o.UserId == UserId)
                                              .OrderByDescending(o => o.OrderDate)
                                              .ToListAsync();

            await _orderRepository.DeleteRangeAsync(orders);

            if (!orders.Any())
                return NotFound("No orders found for this user.");

            var order = new Order
            {
                UserId = int.Parse(userIdClaim.Value),
                TotalPrice = 0,
                OrderItems = new List<OrderItem>()
            };

            foreach (var itemDTO in updateOrder.OrderItems)
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
                UserId = int.Parse(userIdClaim.Value),
                Status = OrderStatus.Pending.ToString(),
                OrderDate = DateTime.UtcNow,
                OrderItems = updateOrder.OrderItems,
                TotalPrice = order.TotalPrice
            };
            return Ok(response);
        }

        [HttpDelete("DeleteOrderById/{Id}")]
        public async Task<IActionResult> DeleteOrderById([FromRoute] int Id)
=
        {
            var order = await _orderRepository.GetOrderById(Id);
            if (order == null)
                return NotFound("Order is not found");

            try
            {
                await _orderRepository.DeleteAsync(order);
                return Ok(new { Message = "Order is deleted successfully", OrderId = Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        #endregion

    }
}
