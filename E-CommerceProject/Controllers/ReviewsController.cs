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
        [HttpPost("Get-All-Reviews")]
        public async Task<IActionResult> GetAllReviews(int pageNumber = 1, int pageSize = 10)
        {
            var reviews = await _reviewRepository.GetPaginationReviews(pageNumber, pageSize);
            return Ok(reviews);
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

            var existingReview = await _reviewRepository.GetByIdAsync(updateReview.ReviewId);
            if (existingReview == null)
                return NotFound("Review is Not Found");

            var reviewMapping = _mapper.Map(updateReview, existingReview);
            if (updateReview != null)
            {
                if (updateReview.Rating != null)
                    reviewMapping.Rating = (int)updateReview.Rating.Value;

                if (!string.IsNullOrEmpty(updateReview.Comment))
                    reviewMapping.Comment = updateReview.Comment;

                var updateResult = await _reviewRepository.UpdateReviewAsync(reviewMapping);
                if (updateResult == "Success")
                    return Ok("Update Review is Successfully.");
            }
            return BadRequest("No updates were made.");
        }

        [HttpDelete("Delete-Review/{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null)
                return NotFound("Review is Not Found");

            var updateResult = await _reviewRepository.DeleteReviewAsync(existingReview);
            if (updateResult == "Success")
                return Ok("Delete Review is Successfully.");

            return BadRequest("Error when delete review.");
        }
    }
}

