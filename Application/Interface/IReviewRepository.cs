using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ReviewDto>> GetAllAsync();
        Task<ReviewDto> GetByIdAsync(int id);
        Task AddAsync(ReviewDto review);
        Task UpdateAsync(ReviewDto review);
        Task DeleteAsync(int id);

        Task UpdateAverageRatingAsync(int placeId);
        Task AddReviewAndUpdateAverageAsync(ReviewDto reviewDto);

        Task<IEnumerable<ReviewDto>> GetByPlaceIdAsync(int placeId);

        Task<ReviewDto> GetByUserAndPlaceAsync(int userId, int placeId);  // To enforce one review per user per place


    }
}
