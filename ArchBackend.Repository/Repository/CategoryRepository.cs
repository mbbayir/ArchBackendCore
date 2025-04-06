using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using ArchBackend.Repository.Repository.Generic;

namespace ArchBackend.Repository.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public Task<bool> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
