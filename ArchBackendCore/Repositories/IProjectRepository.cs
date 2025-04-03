using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Repository.Models;

namespace ArchBackend.Core.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task DeleteAsync(Project project);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<bool> UpdateAsync(Project project);
    }
}
