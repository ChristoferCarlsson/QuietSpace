using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IBookmarkRepository
    {
        Task<IEnumerable<BookmarkDto>> GetAllAsync();
        Task<BookmarkDto> GetByIdAsync(int id);
        Task AddAsync(int userId, BookmarkDto bookmarkDto);

        Task UpdateAsync(BookmarkDto bookmark);
        Task DeleteAsync(int id);

        Task<IEnumerable<BookmarkDto>> GetByUserIdAsync(int userId);
    }
}
