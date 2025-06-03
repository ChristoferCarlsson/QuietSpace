using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetById(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(ReviewDto reviewDto)
        {
            // Get user ID from JWT
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
            reviewDto.UserId = int.Parse(userIdString);

            // Optional: Check if user already reviewed this place
            var existingReview = await _reviewRepository.GetByUserAndPlaceAsync(reviewDto.UserId, reviewDto.PlaceId);
            if (existingReview != null)
                return Conflict("User has already reviewed this place.");

            await _reviewRepository.AddAsync(reviewDto);
            return CreatedAtAction(nameof(GetById), new { id = reviewDto.Id }, reviewDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id) return BadRequest();

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
            int userId = int.Parse(userIdString);

            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null || existingReview.UserId != userId)
                return Forbid("You can only edit your own review.");

            reviewDto.UserId = userId;
            await _reviewRepository.UpdateAsync(reviewDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();
            int userId = int.Parse(userIdString);

            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null || existingReview.UserId != userId)
                return Forbid("You can only delete your own review.");

            await _reviewRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
