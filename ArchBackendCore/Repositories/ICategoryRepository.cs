using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;

namespace ArchBackend.Core.Repositories
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<bool> UpdateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}
