using FirstAPI.DAL;
using FirstAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1,int limit =3 )
        {
            List<Category> categories = await _db.Categories.Skip((page-1)*limit).Take(limit).ToListAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK,existed);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created,category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            existed.Name = name;
            await _db.SaveChangesAsync();

            return Accepted(existed);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _db.Categories.Remove(existed);
            await _db.SaveChangesAsync();
            return NoContent();
        }



    }
}
