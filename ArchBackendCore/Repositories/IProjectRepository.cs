        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using System.Threading.Tasks;
using ArchBackend.Core.Models;

namespace ArchBackend.Core.Repositories
        {
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<bool> UpdateAsync(Project project);
        Task<Project?> GetProjectWithPicturesByIdAsync(int id);
        Task DeleteAsync(Project project);
    
    }
}
