using FirstAPI.DAL;
using FirstAPI.Entities;
using FirstAPI.Repositories.Interfaces;
using System;
namespace FirstAPI.Repositories.Implementations
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext db) : base(db)
        {
        }
    }
}
