using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetByUserId(int userId)
        {
            var bookmarks = await _bookmarkRepository.GetByUserIdAsync(userId);
            return Ok(bookmarks);
        }

        [HttpPost]
        public async Task<ActionResult> Create(BookmarkDto bookmark)
        {
            await _bookmarkRepository.AddAsync(bookmark);
            return CreatedAtAction(nameof(GetById), new { id = bookmark.Id }, bookmark);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BookmarkDto bookmark)
        {
            if (id != bookmark.Id) return BadRequest("ID mismatch");
            await _bookmarkRepository.UpdateAsync(bookmark);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookmarkRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
