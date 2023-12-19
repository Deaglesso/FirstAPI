using FirstAPI.DTOs;

namespace FirstAPI.Services.Interfaces
{
    public interface ITagService
    {
        Task<ICollection<GetTagDTO>> GetAllAsync(int page, int limit);
        Task<GetTagDTO> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDTO tagDTO);
        Task UpdateAsync(int id, CreateTagDTO updateTagDTO);
        Task DeleteAsync(int id);
    }
}
