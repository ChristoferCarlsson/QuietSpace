using Application.DTOs;
using Application.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class QuietPlaceRepository : IQuietPlaceRepository
    {
        private readonly AppDbContext _context;

        public QuietPlaceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuietPlaceDto>> GetAllAsync()
        {
            var entities = await _context.QuietPlaces.ToListAsync();

            return entities.Select(e => new QuietPlaceDto
            {
                Id = e.Id,
                Name = e.Name,
                Address = e.Address,
                AverageRating = e.AverageRating
            });
        }

        public async Task<QuietPlaceDto> GetByIdAsync(int id)
        {
            var e = await _context.QuietPlaces.FindAsync(id);

            if (e == null) return null;

            return new QuietPlaceDto
            {
                Id = e.Id,
                Name = e.Name,
                Address = e.Address,
                AverageRating = e.AverageRating
            };
        }

        public async Task AddAsync(QuietPlaceDto place)
        {
            var entity = new QuietPlace
            {
                Name = place.Name,
                Address = place.Address,
                AverageRating = place.AverageRating
            };

            _context.QuietPlaces.Add(entity);
            await _context.SaveChangesAsync();

            place.Id = entity.Id;
        }

        public async Task UpdateAsync(QuietPlaceDto place)
        {
            var entity = await _context.QuietPlaces.FindAsync(place.Id);

            if (entity == null) return;

            entity.Name = place.Name;
            entity.Address = place.Address;
            entity.AverageRating = place.AverageRating;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.QuietPlaces.FindAsync(id);

            if (entity == null) return;

            _context.QuietPlaces.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<QuietPlaceDto>> GetAllQuietPlacesAsync()
        {
            return await _context.QuietPlaces
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                .Select(p => new QuietPlaceDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    AverageRating = p.Reviews.Any()
                        ? (float?)p.Reviews.Average(r => r.Rating)
                        : null,

                    LatestReviewComment = p.Reviews
                        .Where(r => !string.IsNullOrEmpty(r.Comment))
                        .OrderByDescending(r => r.Date)
                        .Select(r => r.Comment)
                        .FirstOrDefault(),

                    LatestReviewRating = p.Reviews
                        .Where(r => !string.IsNullOrEmpty(r.Comment))
                        .OrderByDescending(r => r.Date)
                        .Select(r => r.Rating)
                        .FirstOrDefault(),

                    LatestReviewerName = p.Reviews
                        .Where(r => !string.IsNullOrEmpty(r.Comment))
                        .OrderByDescending(r => r.Date)
                        .Select(r => r.User.Name)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }


    }
}
