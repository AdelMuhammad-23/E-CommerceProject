using AutoMapper;
using E_CommerceProject.Core.DTOs.ReviewsDTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_CommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public ReviewsController(IMapper mapper,
                                 UserManager<User> userManager,
                                 IReviewRepository reviewRepository,
                                 IProductRepository productRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }
        [HttpPost("Add-Review")]
        public async Task<IActionResult> CreateReview([FromQuery] CreateReviewDTO createReview)
        {
            var product = await _productRepository.GetByIdAsync(createReview.ProductId);

            if (product == null)
                return NotFound("Product is Not Found");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return Unauthorized("User is not authorized");

            var ReviewMapping = _mapper.Map<Review>(createReview);
            ReviewMapping.UserId = int.Parse(userIdClaim.Value);

            var addReview = await _reviewRepository.CreateReviewAsync(ReviewMapping);

            if (addReview == "Success")
                return Ok("Add Review is Successfully.");
            return BadRequest("Error when add Review");
        }
    }
}
