    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ArchBackend.Core.Models;
using ArchBackend.Core.Models.Dto;
using ArchBackend.Core.Service;
using Microsoft.AspNetCore.Http;

namespace ArchBackend.Core.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<Project> AddProjectAsync(Project project, IFormFile formFile);
        Task<Project> UpdateProjectAsync(Project project , IFormFile formFile );
        Task<IEnumerable<ProjectDto>> DetailWithCategory();
        Task<IEnumerable<ProjectDto>> DetailWithOurService();

        Task<bool> DeleteProjectAsync(int id);
    }

}

