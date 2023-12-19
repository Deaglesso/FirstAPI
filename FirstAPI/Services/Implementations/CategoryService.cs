using FirstAPI.DTOs;
using FirstAPI.Entities;
using FirstAPI.Repositories.Interfaces;
using FirstAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCategoryDTO> GetAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("not found");

            return new GetCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<ICollection<GetCategoryDTO>> GetAllAsync(int page, int limit)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip:(page-1)*limit,limit:limit).ToListAsync();
            ICollection<GetCategoryDTO> dtos = new List<GetCategoryDTO>();
            foreach (var category in categories)
            {
                dtos.Add(new GetCategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name,

                });
            }
            return dtos;
        }

        public async Task CreateAsync(CreateCategoryDTO categoryDTO)
        {
            await _repository.AddAsync(new Category
            {
                Name = categoryDTO.Name,
            });
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CreateCategoryDTO categoryDTO)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("not found");

            category.Name = categoryDTO.Name;

            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("not found");

            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }
    }
}
