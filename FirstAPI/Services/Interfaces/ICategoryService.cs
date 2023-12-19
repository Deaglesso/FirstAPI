using FirstAPI.DTOs;
using FirstAPI.Entities;

namespace FirstAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<GetCategoryDTO>> GetAllAsync(int page,int limit);
        Task<GetCategoryDTO> GetAsync(int id);
        Task CreateAsync(CreateCategoryDTO categoryDTO);
        Task UpdateAsync(int id, CreateCategoryDTO categoryDTO);
        Task DeleteAsync(int id);
    }
}
