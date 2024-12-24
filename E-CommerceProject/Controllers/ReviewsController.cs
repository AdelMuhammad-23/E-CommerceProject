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


        [HttpPut("Update-Review")]
        public async Task<IActionResult> UpdateReview([FromQuery] UpdateReviewDTO updateReview)
        {

            var product = await _productRepository.GetByIdAsync(updateReview.ProductId);
            if (product == null)
                return NotFound("Product is Not Found");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                return Unauthorized("User is not authorized");

            var review = await _reviewRepository.GetByIdAsync(updateReview.ReviewId);

            var ReviewMapping = _mapper.Map(updateReview, review);
            //comment or rating == null => don't change in database
            if (updateReview.Rating != null || updateReview.Comment != null)
            {
                var UpdateReview = await _reviewRepository.UpdateReviewAsync(ReviewMapping);

                if (UpdateReview == "Success")
                    return Ok("Update Review is Successfully.");
                if (UpdateReview == null)
                    return NotFound("Review is Not Found");

            }


            return BadRequest("Error when update Review");
        }
    }
}
