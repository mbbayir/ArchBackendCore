    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ArchBackend.Core.Models;
using ArchBackend.Core.Service;
using ArchBackend.Repository.Models;

namespace ArchBackend.Core.Services
{
    public interface IProjectService: IService<Project>
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<bool> DeleteProject(int id);
    }
}

