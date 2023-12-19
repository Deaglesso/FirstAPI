using FirstAPI.DTOs;
using FirstAPI.Entities;
using FirstAPI.Repositories.Interfaces;
using FirstAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }


        public async Task<ICollection<GetTagDTO>> GetAllAsync(int page, int limit)
        {
            ICollection<Tag> tags = await _repository.GetAllAsync(skip: (page - 1) * limit, limit: limit, isTracked: false).ToListAsync();

            ICollection<GetTagDTO> tagDtos = new List<GetTagDTO>();
            foreach (Tag tag in tags)
            {
                tagDtos.Add(new GetTagDTO
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }

            return tagDtos;
        }

        public async Task<GetTagDTO> GetByIdAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Not found");
            return new GetTagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public async Task CreateAsync(CreateTagDTO createTagDto)
        {
            await _repository.AddAsync(new Tag
            {
                Name = createTagDto.Name
            });

            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CreateTagDTO updateTagDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not Found");

            tag.Name = updateTagDto.Name;

            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag == null) throw new Exception("Not found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }

    }
}
