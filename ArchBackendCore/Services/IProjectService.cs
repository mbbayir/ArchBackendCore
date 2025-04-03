    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ArchBackend.Core.Models;
using ArchBackend.Core.Service;
using ArchBackend.Repository.Models;
using Microsoft.AspNetCore.Http;

namespace ArchBackend.Core.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> AddProjectAsync(Project project, IFormFile formFile);
        Task<Project> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(int id);
    }

}

