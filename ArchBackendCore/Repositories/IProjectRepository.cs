        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        using System.Threading.Tasks;
using ArchBackend.Core.Models;
using ArchBackend.Core.Models.Dto;

namespace ArchBackend.Core.Repositories
        {
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<bool> UpdateAsync(Project project);
        Task<Project?> GetProjectWithPicturesByIdAsync(int id);
        Task<IEnumerable<ProjectDto>> DetailWithCategory();

        Task<IEnumerable<ProjectDto>> DetailWithOurService();
        Task DeleteAsync(Project project);
    
    }
}
