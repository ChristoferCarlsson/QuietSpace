using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly AppDbContext _context;

        public BookmarkRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookmarkDto>> GetAllAsync()
        {
            var entities = await _context.Bookmarks.ToListAsync();
            return entities.Select(b => new BookmarkDto
            {
                Id = b.Id,
                UserId = b.UserId,
                PlaceId = b.PlaceId,
                DateAdded = b.DateAdded
            });
        }

        public async Task<BookmarkDto?> GetByIdAsync(int id)
        {
            var b = await _context.Bookmarks.FindAsync(id);
            if (b == null) return null;

            return new BookmarkDto
            {
                Id = b.Id,
                UserId = b.UserId,
                PlaceId = b.PlaceId,
                DateAdded = b.DateAdded
            };
        }

        public async Task AddAsync(int userId, BookmarkDto bookmarkDto)
        {
            try
            {
                var entity = new Bookmark
                {
                    UserId = userId,
                    PlaceId = bookmarkDto.PlaceId,
                    DateAdded = bookmarkDto.DateAdded
                };

                _context.Bookmarks.Add(entity);
                await _context.SaveChangesAsync();

                bookmarkDto.Id = entity.Id;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                throw new Exception($"Error saving bookmark: {innerMessage}", dbEx);
            }
        }


        public async Task UpdateAsync(BookmarkDto bookmarkDto)
        {
            var entity = await _context.Bookmarks.FindAsync(bookmarkDto.Id);
            if (entity == null) return;

            entity.UserId = bookmarkDto.UserId;
            entity.PlaceId = bookmarkDto.PlaceId;
            entity.DateAdded = bookmarkDto.DateAdded;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Bookmarks.FindAsync(id);
            if (entity != null)
            {
                _context.Bookmarks.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookmarkDto>> GetByUserIdAsync(int userId)
        {
            var bookmarks = await _context.Bookmarks
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return bookmarks.Select(b => new BookmarkDto
            {
                Id = b.Id,
                UserId = b.UserId,
                PlaceId = b.PlaceId,
                DateAdded = b.DateAdded
            });
        }

    }
}
