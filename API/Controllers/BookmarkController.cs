using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository _bookmarkRepository;

        public BookmarkController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                              ?? User.FindFirst("id")
                              ?? User.FindFirst("sub");

            if (userIdClaim == null)
                return 0;

            return int.TryParse(userIdClaim.Value, out int userId) ? userId : 0;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetAll()
        {
            var bookmarks = await _bookmarkRepository.GetAllAsync();
            return Ok(bookmarks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkDto>> GetById(int id)
        {
            var bookmark = await _bookmarkRepository.GetByIdAsync(id);
            if (bookmark == null) return NotFound();
            return Ok(bookmark);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetUserBookmarks()
        {
            int userId = GetCurrentUserId();
            var bookmarks = await _bookmarkRepository.GetByUserIdAsync(userId);
            return Ok(bookmarks);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BookmarkDto bookmark)
        {
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                              ?? User.FindFirst("id")
                              ?? User.FindFirst("sub");

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId) || userId <= 0)
            {
                return BadRequest("Invalid user ID in token.");
            }

            bookmark.UserId = userId;
            bookmark.DateAdded = DateTime.UtcNow;

            try
            {
                await _bookmarkRepository.AddAsync(userId, bookmark);
                return CreatedAtAction(nameof(GetById), new { id = bookmark.Id }, bookmark);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving bookmark: {ex.Message}");
                return StatusCode(500, "An error occurred while saving the bookmark.");
            }
        }




        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BookmarkDto bookmark)
        {
            if (id != bookmark.Id) return BadRequest("ID mismatch");
            await _bookmarkRepository.UpdateAsync(bookmark);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookmarkRepository.DeleteAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("byPlace/{placeId}")]
        public async Task<ActionResult> DeleteByPlace(int placeId)
        {
            int userId = GetCurrentUserId();
            var bookmarks = await _bookmarkRepository.GetByUserIdAsync(userId);
            var bookmark = bookmarks.FirstOrDefault(b => b.PlaceId == placeId);
            if (bookmark == null) return NotFound();

            await _bookmarkRepository.DeleteAsync(bookmark.Id);
            return NoContent();
        }
    }
}
