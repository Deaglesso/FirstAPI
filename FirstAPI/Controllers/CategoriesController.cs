using FirstAPI.DAL;
using FirstAPI.DTOs;
using FirstAPI.Entities;
using FirstAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository _repository;

        public CategoriesController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1,int limit =3 )
        {
            return Ok(await _repository.GetAllAsync(x=>x.Id > 2));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _repository.GetByIdAsync(id);

            if (existed is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK,existed);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDTO categoryDTO)
        {
            Category category = new Category
            {
                Name = categoryDTO.Name,
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created,category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _repository.GetByIdAsync(id);

            if (existed is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            existed.Name = name; 
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return Accepted(existed);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _repository.GetByIdAsync(id);

            if (existed is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
            return NoContent();
        }



    }
}
