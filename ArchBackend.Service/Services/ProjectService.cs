using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArchBackend.Core.Services;
using ArchBackend.Core.Repositories;
using ArchBackend.Core.UnitOfWorks;
using ArchBackend.Repository.Models;

namespace ArchBackend.Service.Services
{
    public class ProjectService : Service<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWorks _unitOfWork;

        public ProjectService(IProjectRepository projectRepository, IUnitOfWorks unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
            await _unitOfWork.CommitAsync();
            return project;
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            var existingProject = await _projectRepository.GetByIdAsync(project.Id);
            if (existingProject == null)
                return false;

            await _projectRepository.UpdateAsync(project);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return false;

            await _projectRepository.DeleteAsync(project);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _projectRepository.GetAllAsync();
        }
        public async Task<Project> GetProjectById(int id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }
        public async Task<Project> CreateProject(Project project)
        {
            await _projectRepository.AddAsync(project);
            await _unitOfWork.CommitAsync();
            return project;
        }
        public async Task<Project> UpdateProject(Project project)
        {
            await _projectRepository.UpdateAsync(project);
            await _unitOfWork.CommitAsync();
            return project;
        }
        public async Task<bool> DeleteProject(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                return false;
            await _projectRepository.DeleteAsync(project);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
    
}
