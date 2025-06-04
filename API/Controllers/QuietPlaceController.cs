using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuietPlaceController : ControllerBase
    {
        private readonly IQuietPlaceRepository _quietPlaceRepository;

        public QuietPlaceController(IQuietPlaceRepository quietPlaceRepository)
        {
            _quietPlaceRepository = quietPlaceRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<QuietPlaceDto>> GetAll()
        {
            return await _quietPlaceRepository.GetAllQuietPlacesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuietPlaceDto>> GetById(int id)
        {
            var place = await _quietPlaceRepository.GetByIdAsync(id);
            if (place == null)
                return NotFound();
            return place;
        }

        [HttpGet("multiple")]
        public async Task<ActionResult<IEnumerable<QuietPlaceDto>>> GetMultiple([FromQuery] int[] ids)
        {
            var places = await _quietPlaceRepository.GetByIdsAsync(ids);
            return Ok(places);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Add(QuietPlaceDto place)
        {
            await _quietPlaceRepository.AddAsync(place);
            return CreatedAtAction(nameof(GetById), new { id = place.Id }, place);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, QuietPlaceDto place)
        {
            if (id != place.Id)
                return BadRequest();

            await _quietPlaceRepository.UpdateAsync(place);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _quietPlaceRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
