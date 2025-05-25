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
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                Category = e.Category,
                AverageRating = e.AverageRating,
                Tags = e.Tags
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
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                Category = e.Category,
                AverageRating = e.AverageRating,
                Tags = e.Tags
            };
        }

        public async Task AddAsync(QuietPlaceDto place)
        {
            var entity = new QuietPlace
            {
                Name = place.Name,
                Address = place.Address,
                Latitude = place.Latitude,
                Longitude = place.Longitude,
                Category = place.Category,
                AverageRating = place.AverageRating,
                Tags = place.Tags
            };

            _context.QuietPlaces.Add(entity);
            await _context.SaveChangesAsync();

            place.Id = entity.Id; // return generated ID if needed
        }

        public async Task UpdateAsync(QuietPlaceDto place)
        {
            var entity = await _context.QuietPlaces.FindAsync(place.Id);

            if (entity == null) return;

            entity.Name = place.Name;
            entity.Address = place.Address;
            entity.Latitude = place.Latitude;
            entity.Longitude = place.Longitude;
            entity.Category = place.Category;
            entity.AverageRating = place.AverageRating;
            entity.Tags = place.Tags;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.QuietPlaces.FindAsync(id);

            if (entity == null) return;

            _context.QuietPlaces.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
