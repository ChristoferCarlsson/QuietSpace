using Application.DTOs;
using Application.Interface;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var reviews = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Place)
            .ToListAsync();

        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            UserId = r.UserId,
            PlaceId = r.PlaceId,
            Rating = r.Rating,
            Date = r.Date,
            Comment = r.Comment
        });
    }

    public async Task<ReviewDto> GetByIdAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return null;

        return new ReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            PlaceId = review.PlaceId,
            Rating = review.Rating,
            Date = review.Date,
            Comment = review.Comment
        };
    }

    public async Task AddAsync(ReviewDto reviewDto)
    {
        var review = new Review
        {
            UserId = reviewDto.UserId,
            PlaceId = reviewDto.PlaceId,
            Rating = reviewDto.Rating,
            Date = reviewDto.Date,
            Comment = reviewDto.Comment
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        await UpdateAverageRatingAsync(review.PlaceId);

        reviewDto.Id = review.Id;
    }

    public async Task UpdateAsync(ReviewDto reviewDto)
    {
        var review = await _context.Reviews.FindAsync(reviewDto.Id);
        if (review == null) return;

        review.Rating = reviewDto.Rating;
        review.Comment = reviewDto.Comment;
        review.Date = reviewDto.Date;

        await _context.SaveChangesAsync();

        await UpdateAverageRatingAsync(review.PlaceId);
    }

    public async Task DeleteAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null) return;

        int placeId = review.PlaceId;

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        await UpdateAverageRatingAsync(placeId);
    }

    public async Task<ReviewDto> GetByUserAndPlaceAsync(int userId, int placeId)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.UserId == userId && r.PlaceId == placeId);

        if (review == null) return null;

        return new ReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            PlaceId = review.PlaceId,
            Rating = review.Rating,
            Date = review.Date,
            Comment = review.Comment
        };
    }

    public async Task UpdateAverageRatingAsync(int placeId)
    {
        var place = await _context.QuietPlaces
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == placeId);

        if (place == null) return;

        if (place.Reviews.Any())
        {
            place.AverageRating = (float)Math.Round(place.Reviews.Average(r => r.Rating), 2);
        }
        else
        {
            place.AverageRating = null;
        }

        await _context.SaveChangesAsync();
    }

    public async Task AddReviewAndUpdateAverageAsync(ReviewDto reviewDto)
    {
        await AddAsync(reviewDto);  // AddAsync already calls UpdateAverageRatingAsync internally
    }

    public async Task<IEnumerable<ReviewDto>> GetByPlaceIdAsync(int placeId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.PlaceId == placeId)
            .ToListAsync();

        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            UserId = r.UserId,
            PlaceId = r.PlaceId,
            Rating = r.Rating,
            Date = r.Date,
            Comment = r.Comment
        });
    }

}
