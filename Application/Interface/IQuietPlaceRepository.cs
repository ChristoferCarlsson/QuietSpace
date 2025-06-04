using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IQuietPlaceRepository
    {
        Task<IEnumerable<QuietPlaceDto>> GetAllAsync();
        Task<QuietPlaceDto> GetByIdAsync(int id);
        Task AddAsync(QuietPlaceDto place);
        Task UpdateAsync(QuietPlaceDto place);
        Task DeleteAsync(int id);
        Task<List<QuietPlaceDto>> GetAllQuietPlacesAsync();

    }
}
