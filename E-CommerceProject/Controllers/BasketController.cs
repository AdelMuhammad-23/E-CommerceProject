using AutoMapper;
using E_CommerceProject.Core.DTOs.BasketDTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpGet("GetBasketById/{Id}")]
        public async Task<IActionResult> GetBasketById([FromRoute] string Id)
        {
            var basket = await _basketRepository.GetBasketAsync(Id);
            return Ok(basket ?? new CustomerBasket());
        }
        [HttpPost("UpdateBasket")]
        public async Task<IActionResult> UpdateBasketAsync(CustomerBasketDTO basket)
        {
            var basketMapping = _mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basketMapping);
            return Ok(updatedBasket);
        }
        [HttpDelete("DeleteBasket/{Id}")]
        public async Task<IActionResult> DeleteBasketById([FromRoute] string Id)
        {
            var deleteBasket = await _basketRepository.DeleteBasketAsync(Id);
            return Ok("Basket Deleted successfully");
        }
    }
}
