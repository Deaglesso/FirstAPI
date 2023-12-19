using FirstAPI.DAL;
using FirstAPI.Entities;
using FirstAPI.Repositories.Interfaces;

namespace FirstAPI.Repositories.Implementations
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {

        public CategoryRepository(AppDbContext db): base(db) { }
        
    }
}
