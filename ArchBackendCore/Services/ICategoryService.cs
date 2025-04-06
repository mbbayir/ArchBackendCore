using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;

namespace ArchBackend.Core.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryAsync();

        Task<Category> GetCategoryIdAsync(int id);

        Task<Category> AddCategoryAsync(Category category );

        Task<Category> UpdateCategoryAsync(Category category);

        Task<bool> DeleteCategoryAsync(int id);

    }
}
