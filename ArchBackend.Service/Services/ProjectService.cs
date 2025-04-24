    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ArchBackend.Core.Services;
    using ArchBackend.Core.Repositories;
    using ArchBackend.Core.UnitOfWorks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;
using ArchBackend.Core.Models;
using ArchBackend.Core.Models.Dto;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }

    public async Task<Project> GetProjectByIdAsync(int id)
    {
        return await _projectRepository.GetByIdAsync(id);
    }

    public async Task<Project> AddProjectAsync(Project project, IFormFile formFile)
    {
        if (formFile != null && formFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            project.ImagePath = "/uploads/" + fileName;  
        }
        else
        {
            project.ImagePath = "/uploads/default.jpg"; 
        }

        await _projectRepository.AddAsync(project);
        await _unitOfWork.CommitAsync();
        return project;
    }


    public async Task<Project> UpdateProjectAsync(Project project, IFormFile formFile)
    {

        if (project == null)
        {
            throw new ArgumentNullException(nameof(project), "Invalid Request: Project is null");
        }

        if (formFile != null)
        {
            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var filePath = Path.Combine(uploadsFolderPath, formFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            project.ImagePath = "/uploads/" + formFile.FileName;
        }



        await _projectRepository.UpdateAsync(project);

        await _unitOfWork.CommitAsync();

        return project;
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

      Task<IEnumerable<ProjectDto>> IProjectService.DetailWithCategory()
    {
  
        return  _projectRepository.DetailWithCategory();
    }


    Task<IEnumerable<ProjectDto>> IProjectService.DetailWithOurService()
    {
        return _projectRepository.DetailWithOurService();

    }
}

