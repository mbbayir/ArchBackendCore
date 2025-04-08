using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using ArchBackend.Repository.Repository.Generic;

namespace ArchBackend.Repository.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
            
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await Task.CompletedTask; 
        }
    }

}
