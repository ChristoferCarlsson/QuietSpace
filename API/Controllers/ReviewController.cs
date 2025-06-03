using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpPost]
        public async Task<ActionResult> Create(ReviewDto reviewDto)
        {
            // Optional: Check if user already reviewed this place
            var existingReview = await _reviewRepository.GetByUserAndPlaceAsync(reviewDto.UserId, reviewDto.PlaceId);
            if (existingReview != null)
                return Conflict("User has already reviewed this place.");

            await _reviewRepository.AddAsync(reviewDto);
            return CreatedAtAction(nameof(GetById), new { id = reviewDto.Id }, reviewDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id) return BadRequest();

            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null) return NotFound();

            await _reviewRepository.UpdateAsync(reviewDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingReview = await _reviewRepository.GetByIdAsync(id);
            if (existingReview == null) return NotFound();

            await _reviewRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
