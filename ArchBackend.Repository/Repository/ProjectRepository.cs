using ArchBackend.Core.Models;
using ArchBackend.Core.Models.Dto;
using ArchBackend.Core.Repositories;
using ArchBackend.Repository.Repository;
using ArchBackend.Repository.Repository.Generic;
using Microsoft.EntityFrameworkCore;



public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Project?> GetProjectWithPicturesByIdAsync(int id)
    {
        return await _context.Projects
            .Include(p => p.Picture)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Project> AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return project;
    }


    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.ProjectCategories)
            .ThenInclude(pc => pc.Category)
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task DeleteAsync(Project project)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }


    public async Task<IEnumerable<ProjectDto>> DetailWithCategory()
    {

        var projects = await _context.Projects
       .Include(p => p.ProjectCategories)
       .ThenInclude(pc => pc.Category)
       .AsNoTracking()
       .ToListAsync();

        return projects.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Location = p.Location,
            Tag = p.Tag,
            ImagePath = p.ImagePath,
            Categories = p.ProjectCategories
                             .Select(pc => pc.Category.Name) 
                             .ToList()
        });

    }
    public async Task<IEnumerable<ProjectDto>> DetailWithOurService()
    {
        var projects = await _context.Projects
       .Include(p => p.OurServiceProjects)
       .ThenInclude(pc => pc.OurService)
       .AsNoTracking()
       .ToListAsync();
        return projects.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Location = p.Location,
            Tag = p.Tag,
            ImagePath = p.ImagePath,
            OurServices = p.OurServiceProjects
                            .Select(op => op.OurService.Name) 
                             .ToList()
        });


    }

}